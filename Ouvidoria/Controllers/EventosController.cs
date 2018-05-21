using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ouvidoria.Models;

namespace Ouvidoria.Controllers
{
    public class EventosController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();

        // GET: Eventoes
        public ActionResult Index()
        {
            var evento = db.Evento.Include(e => e.EventoTipo).Include(e => e.Usuario);
            return View(evento.ToList());
        }

        // GET: Eventoes/Details/5
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

        // GET: Eventoes/Create
        public ActionResult Create()
        {
            ViewBag.idEventoTipo = new SelectList(db.EventoTipo, "id", "Tipo");
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome");
            return View();
        }

        // POST: Eventoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Eventoes/Edit/5
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

        // POST: Eventoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Eventoes/Delete/5
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

        // POST: Eventoes/Delete/5
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
