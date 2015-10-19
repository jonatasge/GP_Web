using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualPlay.Admin.Managers;

namespace VirtualPlay.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (UserManager.User != null && UserManager.User.id > 0)
            {
                return RedirectToAction("", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}