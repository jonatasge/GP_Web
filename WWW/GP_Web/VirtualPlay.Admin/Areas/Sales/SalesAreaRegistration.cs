using System.Web.Mvc;

namespace VirtualPlay.Admin.Areas.Sales
{
    public class SalesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Sales";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdmSales",
                "Sales",
                new { controller = "Payment", action = "Sale" },
                namespaces: new[] { "VirtualPlay.Admin.Areas.Sales.Controllers" }
            );

            context.MapRoute(
                "AdmSalesDetails",
                "Sales/{id}",
                new { controller = "Payment", action = "Details", id = UrlParameter.Optional },
                namespaces: new[] { "VirtualPlay.Admin.Areas.Sales.Controllers" }
            );

            context.MapRoute(
                "AdmSalesMerchant",
                "Sales/Merchant/{id}",
                new { controller = "Payment", action = "Merchant", id = UrlParameter.Optional },
                namespaces: new[] { "VirtualPlay.Admin.Areas.Sales.Controllers" }
            );

            context.MapRoute(
                "AdmCashReceipts",
                "CashReceipts",
                new { controller = "CashReceipts", action = "Index" },
                namespaces: new[] { "VirtualPlay.Admin.Areas.Sales.Controllers" }
            );

            context.MapRoute(
                "AdmDashboardSale",
                "Dashboard/SalesData",
                new { controller = "Dashboard", action = "SalesData" },
                namespaces: new[] { "VirtualPlay.Admin.Areas.Sales.Controllers" }
            );

            context.MapRoute(
                "AdmDashboardRefund",
                "Dashboard/SalesDataRefund",
                new { controller = "Dashboard", action = "SalesDataRefund" },
                namespaces: new[] { "VirtualPlay.Admin.Areas.Sales.Controllers" }
            );

            context.MapRoute(
                "AdmDashboardInstallment",
                "Dashboard/SalesDataInstallment",
                new { controller = "Dashboard", action = "SalesDataInstallment" },
                namespaces: new[] { "VirtualPlay.Admin.Areas.Sales.Controllers" }
            );

            context.MapRoute(
                "AdmDashboardCardBrand",
                "Dashboard/SalesDataCardBrand",
                new { controller = "Dashboard", action = "SalesDataCardBrand" },
                namespaces: new[] { "VirtualPlay.Admin.Areas.Sales.Controllers" }
            );

            context.MapRoute(
                "AdmDashboard",
                "Dashboard",
                new { controller = "Dashboard", action = "Index" },
                namespaces: new[] { "VirtualPlay.Admin.Areas.Sales.Controllers" }
            );

            context.MapRoute(
                "AdmRate",
                "Rates",
                new { controller = "Rate", action = "Index" },
                namespaces: new[] { "VirtualPlay.Admin.Areas.Sales.Controllers" }
            );

            context.MapRoute(
                "AdmRateItems",
                "RateItems",
                new { controller = "RateItems", action = "Index" },
                namespaces: new[] { "VirtualPlay.Admin.Areas.Sales.Controllers" }
            );

            context.MapRoute(
                "Admsales_default",
                "Sales/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}