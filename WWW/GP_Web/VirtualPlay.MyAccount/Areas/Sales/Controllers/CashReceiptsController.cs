using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualPlay.MyAccount.Managers;

namespace VirtualPlay.MyAccount.Areas.Sales.Controllers
{
    public class CashReceiptsController : Controller
    {
        //
        // GET: /Sales/CashReceipts/
        public ActionResult Index()
        {
            UserManager.IsAuthenticated();
            return View();
        }
	}
}