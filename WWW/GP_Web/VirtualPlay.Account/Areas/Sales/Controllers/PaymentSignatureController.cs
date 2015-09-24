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

namespace VirtualPlay.MyAccount.Areas.Sales.Controllers
{
    public class PaymentSignatureController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: /Sales/PaymentSignature/
        public async Task<ActionResult> Index()
        {
            var pay_transactionsignature = db.Pay_TransactionSignature.Include(p => p.Pay_Transaction);
            return View(await pay_transactionsignature.ToListAsync());
        }

        // GET: /Sales/PaymentSignature/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_TransactionSignature pay_transactionsignature = await db.Pay_TransactionSignature.FindAsync(id);
            if (pay_transactionsignature == null)
            {
                return HttpNotFound();
            }
            return View(pay_transactionsignature);
        }

        // GET: /Sales/PaymentSignature/Create
        public ActionResult Create()
        {
            ViewBag.idTransaction = new SelectList(db.Pay_Transaction, "idTransaction", "flStatus");
            return View();
        }

        // POST: /Sales/PaymentSignature/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="idSignature,idTransaction,bnSignature,dtCreate")] Pay_TransactionSignature pay_transactionsignature)
        {
            if (ModelState.IsValid)
            {
                db.Pay_TransactionSignature.Add(pay_transactionsignature);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idTransaction = new SelectList(db.Pay_Transaction, "idTransaction", "flStatus", pay_transactionsignature.idTransaction);
            return View(pay_transactionsignature);
        }

        // GET: /Sales/PaymentSignature/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_TransactionSignature pay_transactionsignature = await db.Pay_TransactionSignature.FindAsync(id);
            if (pay_transactionsignature == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTransaction = new SelectList(db.Pay_Transaction, "idTransaction", "flStatus", pay_transactionsignature.idTransaction);
            return View(pay_transactionsignature);
        }

        // POST: /Sales/PaymentSignature/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="idSignature,idTransaction,bnSignature,dtCreate")] Pay_TransactionSignature pay_transactionsignature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pay_transactionsignature).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idTransaction = new SelectList(db.Pay_Transaction, "idTransaction", "flStatus", pay_transactionsignature.idTransaction);
            return View(pay_transactionsignature);
        }

        // GET: /Sales/PaymentSignature/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_TransactionSignature pay_transactionsignature = await db.Pay_TransactionSignature.FindAsync(id);
            if (pay_transactionsignature == null)
            {
                return HttpNotFound();
            }
            return View(pay_transactionsignature);
        }

        // POST: /Sales/PaymentSignature/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Pay_TransactionSignature pay_transactionsignature = await db.Pay_TransactionSignature.FindAsync(id);
            db.Pay_TransactionSignature.Remove(pay_transactionsignature);
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
