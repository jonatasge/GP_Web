using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VirtualPlay.Admin.Startup))]
namespace VirtualPlay.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
