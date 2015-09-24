using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualPlay.MyAccount.Areas.Sales.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Sales/Dashboard/
        public ActionResult Index()
        {
            return View();
        }
	}
}