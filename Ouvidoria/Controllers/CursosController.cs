using Ouvidoria.Filters;
using Ouvidoria.Models;
using Ouvidoria.Service;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    [AutorizacaoFiltro("2")]
    public class CursosController : Controller
    {
        public ActionResult Index()
        {
            return View(CursoService.RetornaCursos());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var retorno = CursoService.ValidaCurso(id);
            if (retorno != "")
            {
                TempData["Error"] = retorno;
                return RedirectToAction("Index");
            }
            return View(CursoService.RetornaCurso(id));
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Nome")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                CursoService.CadastrarCurso(curso);
                return RedirectToAction("Index");
            }

            return View(curso);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var retorno = CursoService.ValidaCurso(id);
            if (retorno != "")
            {
                TempData["Error"] = retorno;
                return RedirectToAction("Index");
            }
            return View(CursoService.RetornaCurso(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Nome")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                CursoService.EditarCurso(curso);
                return RedirectToAction("Index");
            }
            return View(curso);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var retorno = CursoService.ValidaCurso(id);
            if (retorno != "")
            {
                TempData["Error"] = retorno;
                return RedirectToAction("Index");
            }
            return View(CursoService.RetornaCurso(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CursoService.DeletarCurso(id);
            return RedirectToAction("Index");
        }

    }
}
