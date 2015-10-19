using System.Web.Mvc;

namespace VirtualPlay.Admin.Areas.MyAccount
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
                "AdmBilling",
                "Billing",
                new { controller = "Billing", action = "Index" },
                namespaces: new[] { "VirtualPlay.Admin.Areas.MyAccount.Controllers" }
            );

            context.MapRoute(
                "AdmCallCenter",
                "CallCenter",
                new { controller = "CallCenter", action = "Index" },
                namespaces: new[] { "VirtualPlay.Admin.Areas.MyAccount.Controllers" }
            );

            context.MapRoute(
                "AdmMyAccount",
                "MyAccount/Profile/{controller}/{action}/{id}",
                new { controller = "Profile", action = "Details", id = UrlParameter.Optional },
                namespaces: new[] { "VirtualPlay.Admin.Areas.MyAccount.Controllers" }
            );
        }
    }
}