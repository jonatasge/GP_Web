using System.Web.Mvc;

namespace VirtualPlay.MyAccount.Areas.MyAccount
{
    public class MyAccountAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MyAccount";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Billing",
                "Billing",
                new { controller = "Billing", action = "Index" },
                namespaces: new[] { "VirtualPlay.MyAccount.Areas.MyAccount.Controllers" }
            );

            context.MapRoute(
                "CallCenter",
                "CallCenter",
                new { controller = "CallCenter", action = "Index" },
                namespaces: new[] { "VirtualPlay.MyAccount.Areas.MyAccount.Controllers" }
            );

            context.MapRoute(
                "MyAccount",
                "MyAccount/Profile/{controller}/{action}/{id}",
                new { controller = "Profile", action = "Details", id = UrlParameter.Optional },
                namespaces: new[] { "VirtualPlay.MyAccount.Areas.MyAccount.Controllers" }
            );
        }
    }
}