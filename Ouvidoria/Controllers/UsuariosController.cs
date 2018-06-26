using Ouvidoria.Filters;
using Ouvidoria.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    [AutorizacaoFiltro("2")]
    public class UsuariosController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();
        
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.Curso).Include(u => u.UsuarioPerfil);
            return View(usuario.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario
                              .Include(u => u.Curso)
                              .Include(u => u.UsuarioPerfil)
                              .SingleOrDefault(x => x.id == id);
                              //.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }
        
        public ActionResult Create()
        {
            ViewBag.idCurso = new SelectList(db.Curso, "id", "Nome");
            ViewBag.idUsuarioPerfil = new SelectList(db.UsuarioPerfil, "id", "Perfil");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Nome,Email,Telefone,idCurso,idUsuarioPerfil")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCurso = new SelectList(db.Curso, "id", "Nome", usuario.idCurso);
            ViewBag.idUsuarioPerfil = new SelectList(db.UsuarioPerfil, "id", "Perfil", usuario.idUsuarioPerfil);
            return View(usuario);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCurso = new SelectList(db.Curso, "id", "Nome", usuario.idCurso);
            ViewBag.idUsuarioPerfil = new SelectList(db.UsuarioPerfil, "id", "Perfil", usuario.idUsuarioPerfil);
            return View(usuario);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Nome,Email,Telefone,Senha, Ativo, idCurso,idUsuarioPerfil")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCurso = new SelectList(db.Curso, "id", "Nome", usuario.idCurso);
            ViewBag.idUsuarioPerfil = new SelectList(db.UsuarioPerfil, "id", "Perfil", usuario.idUsuarioPerfil);
            return View(usuario);
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
