using Ouvidoria.Filters;
using System.Web.Mvc;

namespace Ouvidoria.Controllers
{
    [AutorizacaoFiltro("2")]
    public class AdministracaoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}