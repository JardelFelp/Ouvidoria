using Ouvidoria.Models;
using Ouvidoria.Service;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getDepoimentosRespondidos()
        {
            var respondidos = DepoimentoService.GetDepoimentosRespondidos();
            var naoRespondidos = DepoimentoService.GetDepoimentosNaoRespondidos();

            return Json(new { respondidos, naoRespondidos }, JsonRequestBehavior.AllowGet);
        }
    }
}