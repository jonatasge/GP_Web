using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VirtualPlay.Api
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "Login/{accessToken}/{email}",
                defaults: new { controller = "Login", action = "getJsonLogin" }
            );

            routes.MapRoute(
                name: "AcessToken",
                url: "AccessToken/{secret}/{email}/{date}",
                defaults: new { controller = "Token", action = "getJsonAccessToken" }
            );

            routes.MapRoute(
                name: "Session",
                url: "Session/Check/{session}/{email}",
                defaults: new { controller = "Login", action = "generateNewSession" }
            );

            routes.MapRoute(
                name: "TransNew",
                url: "Transaction/New/{session}/{email}/{merchant}",
                defaults: new { controller = "Transaction", action = "New" }
            );

            routes.MapRoute(
                name: "TransUpdate",
                url: "Transaction/Update/{session}/{email}/{merchant}",
                defaults: new { controller = "Transaction", action = "Update" }
            );

            routes.MapRoute(
                name: "TransSendReceipt",
                url: "Transaction/SendReceipt/{session}/{email}/{merchant}",
                defaults: new { controller = "Transaction", action = "SendReceipt" }
            );

            routes.MapRoute(
                name: "NewSignature",
                url: "Transaction/NewSignature/{session}/{email}",
                defaults: new { controller = "Transaction", action = "NewSignature" }
            );

            routes.MapRoute(
                name: "DashboardOperation",
                url: "Dashboard/Operation/{session}/{email}/{merchant}/{year}/{month}/{day}",
                defaults: new { controller = "Dashboard", action = "Operation" }
            );

            routes.MapRoute(
                name: "DashboardInstallment",
                url: "Dashboard/Installment/{session}/{email}/{merchant}/{year}/{month}/{day}",
                defaults: new { controller = "Dashboard", action = "Installment" }
            );

            routes.MapRoute(
                name: "DashboardCardBrand",
                url: "Dashboard/CardBrand/{session}/{email}/{merchant}/{year}/{month}/{day}",
                defaults: new { controller = "Dashboard", action = "CardBrand" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}