using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sermo.UI.Web.Startup))]
namespace Sermo.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
