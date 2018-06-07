using Ouvidoria.Models;
using Ouvidoria.Utils;
using Ouvidoria.ViewModels;
using System;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    public class AutenticacaoController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();

        // GET: Autenticacao
        public ActionResult Cadastrar()
        {
            ViewBag.idCurso = new SelectList(db.Curso, "id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(CadastroUsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.idCurso = new SelectList(db.Curso, "id", "Nome");
                return View(viewModel);
            }

            Usuario novoUsuario = new Usuario
            {
                Nome = viewModel.Nome,
                Senha = Hash.GerarHashMd5(viewModel.Senha),
                Email = viewModel.Email,
                Telefone = viewModel.Telefone,
                idCurso = Convert.ToInt32(viewModel.idCurso)
            };

            db.Usuario.Add(novoUsuario);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}