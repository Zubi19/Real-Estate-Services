using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FYP2.Startup))]
namespace FYP2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
