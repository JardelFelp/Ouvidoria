using Ouvidoria.Filters;
using Ouvidoria.Models;
using System.Collections.Generic;
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
            ViewBag.registros = this.GetFeedbacks();
            return View();
        }

        public List<Feedback> GetFeedbacks()
        {
            return db.Feedback.Include(e => e.Departamento).Include(e => e.Usuario).ToList();
        }
    }
}