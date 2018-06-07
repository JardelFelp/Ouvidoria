using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartupAttribute(typeof(Ouvidoria.Startup))]
namespace Ouvidoria
{
    //Classe de configuração para autenticação
    public partial class Startup
    {
        //Método que define as configurações de autenticação
        public void Configuration(IAppBuilder app)
        {
            //Define as opções de atenticação
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplocationCookie",
                LoginPath = new PathString("/Autenticacao/Login")
            });
            //ConfigureAuth(app);
        }
    }
}
