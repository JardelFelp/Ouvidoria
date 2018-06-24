using Ouvidoria.Models;
using Ouvidoria.Utils;
using Ouvidoria.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    [Authorize]
    public class PainelUsuarioController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();

        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identity = User.Identity as ClaimsIdentity;
            var email = identity.Claims.FirstOrDefault(c => c.Type == "Email").Value;

            var usuario = db.Usuario.FirstOrDefault(c => c.Email == email);

            if (Hash.GerarHashMd5(viewModel.Senha) != usuario.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha incorreta");
                return View();
            }

            usuario.Senha = Hash.GerarHashMd5(viewModel.NovaSenha);
            db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            TempData["Mensagem"] = "Senha alterada com sucesso";

            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}