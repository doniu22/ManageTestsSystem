using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestsSystem.Startup))]
namespace TestsSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
