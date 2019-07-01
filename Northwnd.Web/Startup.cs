using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Northwnd.Web.Startup))]
namespace Northwnd.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
