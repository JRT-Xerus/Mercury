using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mercury.Startup))]
namespace Mercury
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
