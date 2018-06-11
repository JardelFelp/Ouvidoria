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
    [Authorize]
    public class PerfisController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();

        // GET: Perfis
        public ActionResult Index()
        {
            return View(db.UsuarioPerfil.ToList());
        }

        // GET: Perfis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioPerfil usuarioPerfil = db.UsuarioPerfil.Find(id);
            if (usuarioPerfil == null)
            {
                return HttpNotFound();
            }
            return View(usuarioPerfil);
        }

        // GET: Perfis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Perfis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Perfil")] UsuarioPerfil usuarioPerfil)
        {
            if (ModelState.IsValid)
            {
                db.UsuarioPerfil.Add(usuarioPerfil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuarioPerfil);
        }

        // GET: Perfis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioPerfil usuarioPerfil = db.UsuarioPerfil.Find(id);
            if (usuarioPerfil == null)
            {
                return HttpNotFound();
            }
            return View(usuarioPerfil);
        }

        // POST: Perfis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Perfil")] UsuarioPerfil usuarioPerfil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioPerfil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarioPerfil);
        }

        // GET: Perfis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioPerfil usuarioPerfil = db.UsuarioPerfil.Find(id);
            if (usuarioPerfil == null)
            {
                return HttpNotFound();
            }
            return View(usuarioPerfil);
        }

        // POST: Perfis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuarioPerfil usuarioPerfil = db.UsuarioPerfil.Find(id);
            db.UsuarioPerfil.Remove(usuarioPerfil);
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
