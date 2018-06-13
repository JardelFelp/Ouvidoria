using Ouvidoria.Filters;
using Ouvidoria.Models;
using Ouvidoria.Service;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    [AutorizacaoFiltro("1")]
    public class OuvidoriaController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();

        [Authorize]
        public ActionResult Index()
        {
           // string nome = User.Identity.Name;
            EventoService.VerificaEventos();
            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo");
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "id,Titulo,Descricao,idEventoTipo,idUsuario")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Evento.Add(evento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo", evento.idEventoTipo);
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome", evento.idUsuario);
            return View(evento);
        }
    }
}