using Ouvidoria.Filters;
using Ouvidoria.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    [AutorizacaoFiltro("2")]
    public class PerfisUsuariosController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();
        
        public ActionResult Index()
        {
            return View(db.UsuarioPerfil.ToList());
        }
        
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
        
        public ActionResult Create()
        {
            return View();
        }
        
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
