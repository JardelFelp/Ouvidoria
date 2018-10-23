using Ouvidoria.Filters;
using Ouvidoria.Models;
using Ouvidoria.Service;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    [AutorizacaoFiltro("2")]
    public class DepartamentosController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();
        
        [Authorize]
        public ActionResult Index()
        {
            return View(DepartamentoService.RetornaTodosDepartamentos());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                RedirectToAction("Index");
            }
            var retorno = DepartamentoService.ValidaDepartamento(id);
            if (retorno == "")
            {
                RedirectToAction("Index");
            }
            return View(DepartamentoService.RetornaDepartamento(id));
        }
        
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Nome")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                DepartamentoService.CadastraDepartamento(departamento);
                return RedirectToAction("Index");
            }

            return View(departamento);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                RedirectToAction("Index");
            }
            var retorno = DepartamentoService.ValidaDepartamento(id);
            if (retorno == "")
            {
                RedirectToAction("Index");
            }
            return View(DepartamentoService.RetornaDepartamento(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "id,Nome")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                DepartamentoService.EditaDepartamento(departamento);
                return RedirectToAction("Index");
            }
            return View(departamento);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                RedirectToAction("Index");
            }
            var retorno = DepartamentoService.ValidaDepartamento(id);
            if (retorno == "")
            {
                RedirectToAction("Index");
            }
            return View(DepartamentoService.RetornaDepartamento(id));
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DepartamentoService.ExcluiDepartamento(id);
            return RedirectToAction("Index");
        }
    }
}
