using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ouvidoria.Startup))]
namespace Ouvidoria
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
