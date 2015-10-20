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
using VirtualPlay.Admin.Managers;

namespace VirtualPlay.Admin.Areas.Sales.Controllers
{
    public class RateItemsController : Controller
    {
        private Entities db = new Entities();

        // GET: /Sales/RateItems/
        public async Task<ActionResult> Index()
        {
            UserManager.IsAuthenticated();
            var pay_rateitems = db.Pay_RateItems.Include(p => p.Pay_Rate).Include(p => p.Sys_User).Include(p => p.Sys_User1);
            return View(await pay_rateitems.ToListAsync());
        }

        // GET: /Sales/RateItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            UserManager.IsAuthenticated();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_RateItems pay_rateitems = await db.Pay_RateItems.FindAsync(id);
            if (pay_rateitems == null)
            {
                return HttpNotFound();
            }
            return View(pay_rateitems);
        }

        // GET: /Sales/RateItems/Create
        public ActionResult Create()
        {
            UserManager.IsAuthenticated();
            ViewBag.idRate = new SelectList(db.Pay_Rate, "idRate", "cdRate");
            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser");
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser");
            return View();
        }

        // POST: /Sales/RateItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="idRate,idRateItem,nmRateItem,value,nbDays,flStatus,idUserCreate,idUserLastUpdate,dtCreate,dtLastUpdate")] Pay_RateItems pay_rateitems)
        {
            UserManager.IsAuthenticated();
            if (ModelState.IsValid)
            {
                db.Pay_RateItems.Add(pay_rateitems);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idRate = new SelectList(db.Pay_Rate, "idRate", "cdRate", pay_rateitems.idRate);
            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rateitems.idUserCreate);
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rateitems.idUserLastUpdate);
            return View(pay_rateitems);
        }

        // GET: /Sales/RateItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            UserManager.IsAuthenticated();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_RateItems pay_rateitems = await db.Pay_RateItems.FindAsync(id);
            if (pay_rateitems == null)
            {
                return HttpNotFound();
            }
            ViewBag.idRate = new SelectList(db.Pay_Rate, "idRate", "cdRate", pay_rateitems.idRate);
            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rateitems.idUserCreate);
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rateitems.idUserLastUpdate);
            return View(pay_rateitems);
        }

        // POST: /Sales/RateItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="idRate,idRateItem,nmRateItem,value,nbDays,flStatus,idUserCreate,idUserLastUpdate,dtCreate,dtLastUpdate")] Pay_RateItems pay_rateitems)
        {
            UserManager.IsAuthenticated();
            if (ModelState.IsValid)
            {
                db.Entry(pay_rateitems).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idRate = new SelectList(db.Pay_Rate, "idRate", "cdRate", pay_rateitems.idRate);
            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rateitems.idUserCreate);
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser", pay_rateitems.idUserLastUpdate);
            return View(pay_rateitems);
        }

        // GET: /Sales/RateItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            UserManager.IsAuthenticated();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_RateItems pay_rateitems = await db.Pay_RateItems.FindAsync(id);
            if (pay_rateitems == null)
            {
                return HttpNotFound();
            }
            return View(pay_rateitems);
        }

        // POST: /Sales/RateItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserManager.IsAuthenticated();
            Pay_RateItems pay_rateitems = await db.Pay_RateItems.FindAsync(id);
            db.Pay_RateItems.Remove(pay_rateitems);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
