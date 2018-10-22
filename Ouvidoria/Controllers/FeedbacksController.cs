using Ouvidoria.Filters;
using Ouvidoria.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    [AutorizacaoFiltro("2")]
    public class FeedbacksController : Controller
    {
        private OuvidoriaContext db = new OuvidoriaContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetFeedbacks()
        {
            var feedbacks = db.Feedback.Include(e => e.Departamento).Include(e => e.Usuario).ToList();
            return Json(new { data = feedbacks }, JsonRequestBehavior.AllowGet);
        }
    }
}