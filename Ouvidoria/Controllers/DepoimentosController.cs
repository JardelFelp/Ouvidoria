using Ouvidoria.Filters;
using Ouvidoria.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    [AutorizacaoFiltro("2")]
    public class DepoimentosController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();
        
        public ActionResult Index()
        {
            var departamentoDepoimento = db.DepartamentoDepoimento.Include(d => d.Departamento).Include(d => d.Usuario);
            return View(departamentoDepoimento.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartamentoDepoimento departamentoDepoimento = db.DepartamentoDepoimento.Find(id);
            if (departamentoDepoimento == null)
            {
                return HttpNotFound();
            }
            return View(departamentoDepoimento);
        }
        
        public ActionResult Create()
        {
            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome");
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Avaliacao,Comentario,idUsuario,idDepartamento")] DepartamentoDepoimento departamentoDepoimento)
        {
            if (ModelState.IsValid)
            {
                db.DepartamentoDepoimento.Add(departamentoDepoimento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome", departamentoDepoimento.idDepartamento);
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome", departamentoDepoimento.idUsuario);
            return View(departamentoDepoimento);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartamentoDepoimento departamentoDepoimento = db.DepartamentoDepoimento.Find(id);
            if (departamentoDepoimento == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome", departamentoDepoimento.idDepartamento);
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome", departamentoDepoimento.idUsuario);
            return View(departamentoDepoimento);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Avaliacao,Comentario,idUsuario,idDepartamento")] DepartamentoDepoimento departamentoDepoimento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departamentoDepoimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDepartamento = new SelectList(db.Departamento, "id", "Nome", departamentoDepoimento.idDepartamento);
            ViewBag.idUsuario = new SelectList(db.Usuario, "id", "Nome", departamentoDepoimento.idUsuario);
            return View(departamentoDepoimento);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartamentoDepoimento departamentoDepoimento = db.DepartamentoDepoimento.Find(id);
            if (departamentoDepoimento == null)
            {
                return HttpNotFound();
            }
            return View(departamentoDepoimento);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DepartamentoDepoimento departamentoDepoimento = db.DepartamentoDepoimento.Find(id);
            db.DepartamentoDepoimento.Remove(departamentoDepoimento);
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
