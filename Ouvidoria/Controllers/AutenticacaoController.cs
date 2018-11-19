using Ouvidoria.Models;
using Ouvidoria.Service;
using Ouvidoria.Utils;
using Ouvidoria.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    public class AutenticacaoController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();

        public ActionResult Cadastrar()
        {
            UsuarioService.VerificaPerfis();
            CursoService.VerificaCurso();
            TipoDepoimentoService.TemTipos();
            ViewBag.idCurso = new SelectList(db.Curso, "id", "Nome");
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastroUsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.idCurso = new SelectList(db.Curso, "id", "Nome");
                return View(viewModel);
            }

            if (!UsuarioService.VerificaEmailValido(viewModel.Email))
            {
                ModelState.AddModelError("Email", "Esse email já está em uso");
                ViewBag.idCurso = new SelectList(db.Curso, "id", "Nome");
                return View(viewModel);
            }

            UsuarioService.CadastraUsuario(viewModel);

            TempData["Mensagem"] = "Cadastro realizado com sucesso. Por favor, efetue o login.";

            return RedirectToAction("Login");
        }

        public ActionResult Login(string ReturnUrl)
        {
            UsuarioService.VerificaPerfis();
            CursoService.VerificaCurso();
            TipoDepoimentoService.TemTipos();
            var viewModel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            var usuario = db.Usuario.FirstOrDefault(u => u.Email == viewModel.Email);

            if (usuario == null || usuario.Senha != Hash.GerarHashMd5(viewModel.Senha))
            {
                ModelState.AddModelError("Email", "Login ou senha incorreta");
                return View(viewModel);
            }

            if(usuario.Ativo == false)
            {
                ModelState.AddModelError("Email", "Usuário inativo, favor entrar em contato com algum administrador ou enviar email para ouvidoria@faculdadeam.edu.br");
                return View(viewModel);
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.NameIdentifier, usuario.id.ToString()),
                new Claim("Email", usuario.Email),
                new Claim(ClaimTypes.Role, usuario.idUsuarioPerfil.ToString())
            }, "ApplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(identity);

            if (!String.IsNullOrWhiteSpace(viewModel.UrlRetorno) || Url.IsLocalUrl(viewModel.UrlRetorno))
                return Redirect(viewModel.UrlRetorno);
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}