using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VirtualPlay.Admin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AdmLogin",
                url: "Login/",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "VirtualPlay.Admin.Controllers" }
            );

            routes.MapRoute(
                name: "AdmLogOff",
                url: "LogOff/",
                defaults: new { controller = "Account", action = "LogOff", id = UrlParameter.Optional },
                namespaces: new[] { "VirtualPlay.Admin.Controllers" }
            );

            routes.MapRoute(
                name: "AdmDefault",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "VirtualPlay.Admin.Controllers" }
            );

        }
    }
}
