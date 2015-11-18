using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarDumpApp.Startup))]
namespace CarDumpApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
