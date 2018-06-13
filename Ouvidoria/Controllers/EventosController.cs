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
            var evento = db.Evento.Include(e => e.EventoTipo).Include(e => e.Usuario);
            return View(evento.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }
        
        public ActionResult Create()
        {
            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo");
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Titulo,Descricao,idEventoTipo,idUsuario")] Evento evento)
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
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo", evento.idEventoTipo);
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome", evento.idUsuario);
            return View(evento);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Titulo,Descricao,idEventoTipo,idUsuario")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo", evento.idEventoTipo);
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome", evento.idUsuario);
            return View(evento);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evento evento = db.Evento.Find(id);
            db.Evento.Remove(evento);
            db.SaveChanges();
            return RedirectToAction("Index");
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
