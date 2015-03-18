using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_3DMapleSystem.Web.Startup))]
namespace _3DMapleSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
