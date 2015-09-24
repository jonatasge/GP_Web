using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VirtualPlay.MyAccount.Startup))]
namespace VirtualPlay.MyAccount
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
