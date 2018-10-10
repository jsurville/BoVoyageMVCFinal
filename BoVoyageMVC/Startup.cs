using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoVoyageMVC.Startup))]
namespace BoVoyageMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
