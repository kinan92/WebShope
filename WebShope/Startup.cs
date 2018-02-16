using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebShope.Startup))]
namespace WebShope
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
