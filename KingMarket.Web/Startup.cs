using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KingMarket.Web.Startup))]
namespace KingMarket.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
