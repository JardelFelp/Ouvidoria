using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ouvidoria.Filters
{
    public class AutorizacaoFiltro : AuthorizeAttribute
    {
        private string PerfilAutorizado;

        public AutorizacaoFiltro(string perfil)
        {
            PerfilAutorizado = perfil;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool autorizado = filterContext.HttpContext.User.IsInRole(PerfilAutorizado);

            if (!autorizado)
            {
                filterContext.Controller.TempData["ErroAutorizacao"] = "Voce nao tem permissao para acessar essa pagina.";
                filterContext.Result = new RedirectResult("~/Home");
            }

        }
    }
}