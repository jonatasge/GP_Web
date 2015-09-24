using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using VirtualPlay.Business.Models;
using VirtualPlay.MyAccount.Managers;
using System.Text;
using System.IO;
using System.Drawing;

using PagedList;

namespace VirtualPlay.MyAccount.Areas.Sales.Controllers
{
    public class PaymentController : Controller
    {
        private Entities db = new Entities();

        // GET: /Sales/Payment/
        public async Task<ActionResult> Sale(string sortOrder, string operation, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "operation_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (UserManager.User != null)
            {
                var pay_transaction = db.Pay_Transaction.Include(p => p.Sys_Merchant);

                if (pay_transaction != null)
                {
                    var sales = pay_transaction.Where(p => p.idMerchant == UserManager.User.idMerchant);

                    if (!String.IsNullOrEmpty(operation))
                    {
                        sales = pay_transaction.Where(p => p.idMerchant == UserManager.User.idMerchant);
                    }
                    else
                    {
                        sales = pay_transaction.Where(p => p.idMerchant == UserManager.User.idMerchant);
                    }

                    if (sales != null)
                    {
                        switch (sortOrder)
                        {
                            case "operation_desc":
                                sales = sales.OrderByDescending(s => s.operation);
                                break;
                            case "Date":
                                sales = sales.OrderBy(s => s.date);
                                break;
                            case "date_desc":
                                sales = sales.OrderByDescending(s => s.date);
                                break;
                            default:
                                sales = sales.OrderByDescending(s => s.date);
                                break;
                        }

                        int pageSize = 7;
                        int pageNumber = (page ?? 1);

                        return View(sales.ToPagedList(pageNumber, pageSize));
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        // GET: /Sales/Dashboard/
        public async Task<ActionResult> Dashboard()
        {
            return View();
        }

        // GET: /Sales/Payment/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_Transaction pay_transaction = await db.Pay_Transaction.FindAsync(id);

            if (pay_transaction == null)
            {
                return HttpNotFound();
            }

            return View(pay_transaction);
        }
        
        // GET: /Sales/Payment/Create
        public ActionResult Create()
        {
            ViewBag.idMerchant = new SelectList(db.Sys_Merchant, "idMerchant", "dsEmail");
            return View();
        }

        // POST: /Sales/Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="idTransaction,idMobile,idMerchant,dtCreate,dtLastUpdate,flStatus,acquirer,acquirerNSU,acquirerResponseCode,authorizationNumber,cardBIN,cardBrand,cardBrandCode,clisitefConfirmationData,clisitefRequestNumber,customerEmail,customerPhone,customerReceipt,date,fiscalDate,fiscalHour,installmentAmount,isTest,issuerInstallmentAllowed,maxIssuerInstallments,maxMerchantInstallments,merchantEmail,merchantInstallmentAllowed,merchantName,merchantReceipt,operation,paymentFunction,paymentFunctionDescription,paymentType,pinpadInfo,pinpadSerialNumber,refundDate,refundDocumentNumber,sitefNSU,sitefVersion,state,statusCode,timestamp,token,type,value,latitude,longitude")] Pay_Transaction pay_transaction)
        {
            if (ModelState.IsValid)
            {
                pay_transaction.idTransaction = Guid.NewGuid();
                db.Pay_Transaction.Add(pay_transaction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idMerchant = new SelectList(db.Sys_Merchant, "idMerchant", "dsEmail", pay_transaction.idMerchant);
            return View(pay_transaction);
        }

        // GET: /Sales/Payment/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_Transaction pay_transaction = await db.Pay_Transaction.FindAsync(id);
            if (pay_transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.idMerchant = new SelectList(db.Sys_Merchant, "idMerchant", "dsEmail", pay_transaction.idMerchant);
            return View(pay_transaction);
        }

        // POST: /Sales/Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="idTransaction,idMobile,idMerchant,dtCreate,dtLastUpdate,flStatus,acquirer,acquirerNSU,acquirerResponseCode,authorizationNumber,cardBIN,cardBrand,cardBrandCode,clisitefConfirmationData,clisitefRequestNumber,customerEmail,customerPhone,customerReceipt,date,fiscalDate,fiscalHour,installmentAmount,isTest,issuerInstallmentAllowed,maxIssuerInstallments,maxMerchantInstallments,merchantEmail,merchantInstallmentAllowed,merchantName,merchantReceipt,operation,paymentFunction,paymentFunctionDescription,paymentType,pinpadInfo,pinpadSerialNumber,refundDate,refundDocumentNumber,sitefNSU,sitefVersion,state,statusCode,timestamp,token,type,value,latitude,longitude")] Pay_Transaction pay_transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pay_transaction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idMerchant = new SelectList(db.Sys_Merchant, "idMerchant", "dsEmail", pay_transaction.idMerchant);
            return View(pay_transaction);
        }

        // GET: /Sales/Payment/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay_Transaction pay_transaction = await db.Pay_Transaction.FindAsync(id);
            if (pay_transaction == null)
            {
                return HttpNotFound();
            }
            return View(pay_transaction);
        }

        // POST: /Sales/Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Pay_Transaction pay_transaction = await db.Pay_Transaction.FindAsync(id);
            db.Pay_Transaction.Remove(pay_transaction);
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
        
        //public Image Base64ToImage(string base64String)
        //{
        //    // Convert Base64 String to byte[]
        //    byte[] imageBytes = Convert.FromBase64String(base64String);
        //    MemoryStream ms = new MemoryStream(imageBytes, 0,
        //      imageBytes.Length);

        //    // Convert byte[] to Image
        //    ms.Write(imageBytes, 0, imageBytes.Length);
        //    Image image = Image.FromStream(ms, true);
        //    return image;
        //}
    }
}
