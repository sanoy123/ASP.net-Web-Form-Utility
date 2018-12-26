using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Presenation.Startup))]
namespace Presenation
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
