using Ouvidoria.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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