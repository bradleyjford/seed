using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Seed.Web.Startup))]
namespace Seed.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
