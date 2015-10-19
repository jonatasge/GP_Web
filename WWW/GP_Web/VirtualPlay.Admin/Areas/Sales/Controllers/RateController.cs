using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VirtualPlay.Business.Models;
using VirtualPlay.Admin.Controllers;
using VirtualPlay.Admin.Managers;
using System.Configuration;
using VirtualPlay.Business;

namespace VirtualPlay.Admin.Areas.Sales.Controllers
{
    public class RateController : Controller
    {
        private Entities db = new Entities();

        // GET: /Sales/Rate/
        public async Task<ActionResult> Index()
        {
            UserManager.IsAuthenticated();

            var pay_rate = db.Pay_Rate.Include(p => p.Sys_User).Include(p => p.Sys_User1);
            return View(await pay_rate.ToListAsync());
        }

        // GET: /Sales/Rate/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            UserManager.IsAuthenticated();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_Rate pay_rate = await db.Pay_Rate.FindAsync(id);
            if (pay_rate == null)
            {
                return HttpNotFound();
            }
            return View(pay_rate);
        }

        // GET: /Sales/Rate/Create
        public ActionResult Create()
        {
            UserManager.IsAuthenticated();
            ViewBag.RateType = DDLHelper.GetRateType();
            ViewBag.RateAnticipated = DDLHelper.GetYesNo();
            ViewBag.RateStatus = DDLHelper.GetStatusDefault();
            return View();
        }

        // POST: /Sales/Rate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="idRate,cdRate,nmRate,flMerchant,flAnticipated,flStatus,idUserCreate,idUserLastUpdate,dtCreate,dtLastUpdate")] Pay_Rate pay_rate)
        {
            UserManager.IsAuthenticated();
            if (ModelState.IsValid)
            {
                pay_rate.flMerchant = Request["rateType"];
                pay_rate.flAnticipated = Request["rateAnticipated"];
                pay_rate.flStatus = Request["rateStatus"];

                pay_rate.idUserCreate = Managers.UserManager.User.id;
                pay_rate.dtCreate = DateTime.Now;

                db.Pay_Rate.Add(pay_rate);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RateType = DDLHelper.GetRateType();
            ViewBag.RateAnticipated = DDLHelper.GetYesNo();
            ViewBag.RateStatus = DDLHelper.GetStatusDefault();
            return View(pay_rate);
        }

        // GET: /Sales/Rate/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            UserManager.IsAuthenticated();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_Rate pay_rate = await db.Pay_Rate.FindAsync(id);
            if (pay_rate == null)
            {
                return HttpNotFound();
            }
            ViewBag.RateTypeList = DDLHelper.GetRateType();
            ViewBag.RateAnticipatedList = DDLHelper.GetYesNo();
            ViewBag.RateStatusList = DDLHelper.GetStatusDefault();
            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rate.idUserCreate);
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rate.idUserLastUpdate);

            ViewBag.RateType = pay_rate.flMerchant;
            ViewBag.RateAnticipated = pay_rate.flAnticipated;
            ViewBag.RateStatus = pay_rate.flStatus;

            return View(pay_rate);
        }

        // POST: /Sales/Rate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="idRate,cdRate,nmRate,flMerchant,flAnticipated,flStatus,idUserCreate,idUserLastUpdate,dtCreate,dtLastUpdate")] Pay_Rate pay_rate)
        {
            UserManager.IsAuthenticated();
            if (ModelState.IsValid)
            {
                pay_rate.cdRate = Request["cdRate"];

                pay_rate.flMerchant = Request["rateType"];
                pay_rate.flAnticipated = Request["rateAnticipated"];
                pay_rate.flStatus = Request["rateStatus"];

                pay_rate.idUserLastUpdate = Managers.UserManager.User.id;
                pay_rate.dtLastUpdate = DateTime.Now;

                db.Entry(pay_rate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RateType = DDLHelper.GetRateType();
            ViewBag.RateAnticipated = DDLHelper.GetYesNo();
            ViewBag.RateStatus = DDLHelper.GetStatusDefault();
            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rate.idUserCreate);
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rate.idUserLastUpdate);
            return View(pay_rate);
        }

        // GET: /Sales/Rate/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            UserManager.IsAuthenticated();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_Rate pay_rate = await db.Pay_Rate.FindAsync(id);
            if (pay_rate == null)
            {
                return HttpNotFound();
            }
            return View(pay_rate);
        }

        // POST: /Sales/Rate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserManager.IsAuthenticated();
            Pay_Rate pay_rate = await db.Pay_Rate.FindAsync(id);
            db.Pay_Rate.Remove(pay_rate);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: /Sales/RateItems/
        public PartialViewResult IndexItems(int id)
        {
            UserManager.IsAuthenticated();
            var pay_rateitems = db.Pay_RateItems.Where(p => p.idRate == id).Include(p => p.Pay_Rate).Include(p => p.Sys_User).Include(p => p.Sys_User1);
            return PartialView(pay_rateitems.ToList());
        }

        // GET: /Sales/RateItems/Details/5
        public async Task<PartialViewResult> DetailsItems(int? id)
        {
            UserManager.IsAuthenticated();
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_RateItems pay_rateitems = await db.Pay_RateItems.FindAsync(id);
            if (pay_rateitems == null)
            {
                //return HttpNotFound();
            }
            return PartialView(pay_rateitems);
        }

        // GET: /Sales/RateItems/Create
        public PartialViewResult CreateItems(int? id)
        {
            UserManager.IsAuthenticated();

            ViewBag.idRate = new SelectList(db.Pay_Rate, "idRate", "cdRate", id);
            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser", UserManager.User.id);
            ViewBag.dtCreate = DateTime.Now;
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser");

            ViewBag.RateItemStatus = DDLHelper.GetStatusDefault();
            ViewBag.RateItemId = DDLHelper.GetDays(100);
            ViewBag.RateItemDays = DDLHelper.GetDays(31);
            
            return PartialView();
        }

        // POST: /Sales/RateItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult CreateItems([Bind(Include = "idRate,idRateItem,nmRateItem,value,nbDays,flStatus,idUserCreate,idUserLastUpdate,dtCreate,dtLastUpdate")] Pay_RateItems pay_rateitems)
        {
            UserManager.IsAuthenticated();
            if (ModelState.IsValid)
            {
                pay_rateitems.idUserCreate = Managers.UserManager.User.id;
                pay_rateitems.dtCreate = DateTime.Now;

                db.Pay_RateItems.Add(pay_rateitems);
                db.SaveChanges();
                HttpContext.Response.Redirect(string.Concat(ConfigurationManager.AppSettings["UrlHost"], "/Sales/Rate/Details/", pay_rateitems.idRate), true);
                //return RedirectToAction("Index");
            }

            ViewBag.idRate = new SelectList(db.Pay_Rate, "idRate", "cdRate", pay_rateitems.idRate);
            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rateitems.idUserCreate);
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rateitems.idUserLastUpdate);

            ViewBag.RateItemStatus = DDLHelper.GetStatusDefault();
            ViewBag.RateItemId = DDLHelper.GetDays(100);
            ViewBag.RateItemDays = DDLHelper.GetDays(31);
            return PartialView(pay_rateitems);
        }

        // GET: /Sales/RateItems/Edit/5
        public async Task<ActionResult> EditItems(int? id, int item)
        {
            UserManager.IsAuthenticated();
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_RateItems pay_rateitems = db.Pay_RateItems.Find(id, item);
            if (pay_rateitems == null)
            {
                //return HttpNotFound();
            }

            ViewBag.idRate = new SelectList(db.Pay_Rate, "idRate", "cdRate", pay_rateitems.idRate);

            ViewBag.RateItemStatusList = DDLHelper.GetStatusDefault();
            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rateitems.idUserCreate);
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rateitems.idUserLastUpdate);

            ViewBag.RateItemStatus = pay_rateitems.flStatus;

            return PartialView(pay_rateitems);
        }

        // POST: /Sales/RateItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditItems([Bind(Include = "idRate,idRateItem,nmRateItem,value,nbDays,flStatus,idUserCreate,idUserLastUpdate,dtCreate,dtLastUpdate")] Pay_RateItems pay_rateitems)
        {
            UserManager.IsAuthenticated();
            if (ModelState.IsValid)
            {
                pay_rateitems.flStatus = Request["rateItemStatus"];

                pay_rateitems.idUserLastUpdate = Managers.UserManager.User.id;
                pay_rateitems.dtLastUpdate = DateTime.Now;

                db.Entry(pay_rateitems).State = EntityState.Modified;
                await db.SaveChangesAsync();

                HttpContext.Response.Redirect(string.Concat(ConfigurationManager.AppSettings["UrlHost"], "/Sales/Rate/Details/", pay_rateitems.idRate), true);
                //return RedirectToAction("Rates");
            }

            ViewBag.idRate = new SelectList(db.Pay_Rate, "idRate", "cdRate", pay_rateitems.idRate);

            ViewBag.RateItemStatusList = DDLHelper.GetStatusDefault();

            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rateitems.idUserCreate);
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rateitems.idUserLastUpdate);
            return PartialView(pay_rateitems);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
