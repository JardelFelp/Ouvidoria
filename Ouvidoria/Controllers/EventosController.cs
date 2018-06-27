using Ouvidoria.Filters;
using Ouvidoria.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    [AutorizacaoFiltro("2")]
    public class EventosController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEventos()
        {
            var evento = db.Evento.Include(e => e.EventoTipo).Include(e => e.Usuario).ToList();
            return Json(new { data = evento }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Responder(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Evento evento = db.Evento
                              .Include(e => e.EventoTipo)
                              .Include(e => e.Usuario)
                              .SingleOrDefault(x => x.id == id);
            if (evento == null)
            {
                TempData["Error"] = "Depoimento nao encontrado";
                return RedirectToAction("Index");
            }
            if (evento.Respondido == true)
            {
                TempData["Error"] = "Depoimento ja respondido";
                return RedirectToAction("Index");
            }
            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo", evento.idEventoTipo);
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome", evento.idUsuario);
            return View(evento);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Responder([Bind(Include = "id,Titulo,Descricao,idEventoTipo,idUsuario, Resposta")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                if(evento.Resposta == "" || evento.Resposta == null)
                {
                    var evento2 = db.Evento.Include(e => e.EventoTipo)
                              .Include(e => e.Usuario)
                              .SingleOrDefault(x => x.id == evento.id);
                    ModelState.AddModelError("Resposta", "Preencha a resposta");
                    ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo", evento.idEventoTipo);
                    ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome", evento.idUsuario);
                    return View(evento2);
                }
                evento.Respondido = true;
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo", evento.idEventoTipo);
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome", evento.idUsuario);
            return View(evento);
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
