using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualPlay.Admin.Managers;
using VirtualPlay.Business;

namespace VirtualPlay.Admin.Areas.Sales.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Sales/Dashboard/
        public ActionResult Index(int? year, int? month, int? day)
        {
            UserManager.IsAuthenticated();
            SelectList selectListYear = new SelectList(DDLHelper.GetYears(), "Value", "Text", 2015);
            SelectList selectListMonth = new SelectList(DDLHelper.GetMonths(), "Value", "Text", 9);
            SelectList selectListDay = new SelectList(DDLHelper.GetDays(), "Value", "Text", 29);

            ViewBag.Years = selectListYear;
            ViewBag.Months = selectListMonth;
            ViewBag.Days = selectListDay;

            if (!year.HasValue)
            {
                year = DateTime.Now.Year;
                ViewBag.Year = year;
            }

            if (!month.HasValue)
            {
                month = DateTime.Now.Month;
                ViewBag.Month = month;
            }

            if (!day.HasValue)
            {
                day = DateTime.Now.Day;
                ViewBag.Day = day;
            }

            return View();
        }

        public JsonResult SalesData(int? year, int? month, int? day)
        {
            UserManager.IsAuthenticated();
            if (!year.HasValue)
                year = DateTime.Now.Year;

            if (!month.HasValue)
                month = DateTime.Now.Month;

            if (!day.HasValue)
                day = DateTime.Now.Day;

            if (UserManager.User != null)
            {
                Api.Controllers.DashboardController apiDashboard = new Api.Controllers.DashboardController();

                var result = apiDashboard.Sale(-1, year.Value, month.Value, day.Value);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SalesDataRefund(int? year, int? month, int? day)
        {
            UserManager.IsAuthenticated();
            if (!year.HasValue)
                year = DateTime.Now.Year;

            if (!month.HasValue)
                month = DateTime.Now.Month;

            if (!day.HasValue)
                day = DateTime.Now.Day;

            if (UserManager.User != null)
            {
                Api.Controllers.DashboardController apiDashboard = new Api.Controllers.DashboardController();

                var result = apiDashboard.Refund(-1, year.Value, month.Value, day.Value);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SalesDataInstallment(int? year, int? month, int? day)
        {
            UserManager.IsAuthenticated();
            if (!year.HasValue)
                year = DateTime.Now.Year;

            if (!month.HasValue)
                month = DateTime.Now.Month;

            if (!day.HasValue)
                day = DateTime.Now.Day;

            if (UserManager.User != null)
            {
                Api.Controllers.DashboardController apiDashboard = new Api.Controllers.DashboardController();

                var result = apiDashboard.Installment(-1, year.Value, month.Value, day.Value);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SalesDataCardBrand(int? year, int? month, int? day)
        {
            UserManager.IsAuthenticated();
            if (!year.HasValue)
                year = DateTime.Now.Year;

            if (!month.HasValue)
                month = DateTime.Now.Month;

            if (!day.HasValue)
                day = DateTime.Now.Day;

            if (UserManager.User != null)
            {
                Api.Controllers.DashboardController apiDashboard = new Api.Controllers.DashboardController();

                var result = apiDashboard.CardBrand(-1, year.Value, month.Value, day.Value);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
	}
}