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
using VirtualPlay.Admin.Managers;
using System.Text;
using System.IO;
using System.Drawing;

using PagedList;
using VirtualPlay.Business;

namespace VirtualPlay.Admin.Areas.Sales.Controllers
{
    public class PaymentController : Controller
    {
        private Entities db = new Entities();

        // GET: /Sales/Payment/
        public async Task<ActionResult> Sale(int? year, int? month, int? day, string sortOrder, string operation, string status, string cardBrand, int? page, string marchantName)
        {
            UserManager.IsAuthenticated();
            SelectList selectListYear = new SelectList(DDLHelper.GetYears(), "Value", "Text", 2015);
            SelectList selectListMonth = new SelectList(DDLHelper.GetMonths(), "Value", "Text", 9);
            SelectList selectListDay = new SelectList(DDLHelper.GetDays(), "Value", "Text", 29);

            ViewBag.Operation = new SelectList(DDLHelper.GetOperation(), "Value", "Text");
            ViewBag.Status = new SelectList(DDLHelper.GetStatus(), "Value", "Text");
            ViewBag.CardBrand = new SelectList(DDLHelper.GetCardBrand(), "Value", "Text");

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "operation_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

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

            return SaleResult(-1, year.Value, month.Value, day.Value, sortOrder, operation, status, cardBrand, page ?? 1, marchantName);
        }

        // GET: /Sales/Payment/Merchant
        public async Task<ActionResult> Merchant(int? year, int? month, int? day, string sortOrder, string operation, string status, string cardBrand, int? page, string merchantName)
        {
            UserManager.IsAuthenticated();
            SelectList selectListYear = new SelectList(DDLHelper.GetYears(), "Value", "Text");
            SelectList selectListMonth = new SelectList(DDLHelper.GetMonths(), "Value", "Text");
            SelectList selectListDay = new SelectList(DDLHelper.GetDays(), "Value", "Text");

            ViewBag.Operation = new SelectList(DDLHelper.GetOperation(), "Value", "Text");
            ViewBag.Status = new SelectList(DDLHelper.GetStatus(), "Value", "Text");
            ViewBag.CardBrand = new SelectList(DDLHelper.GetCardBrand(), "Value", "Text");

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "operation_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

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

            return SaleResult(UserManager.User.idMerchant, year.Value, month.Value, day.Value, sortOrder, operation, status, cardBrand, page ?? 1, merchantName);
        }

        // GET: /Sales/Payment/Merchant
        public ActionResult SaleResult(int merchant, int year, int month, int day, string sortOrder, string operation, string status, string cardBrand, int page, string merchantName)
        {
            UserManager.IsAuthenticated();
            if (UserManager.User != null)
            {
                var pay_transaction = db.Pay_Transaction.Include(p => p.Sys_Merchant);

                if (pay_transaction != null)
                {
                    IQueryable<Pay_Transaction> sales = null;

                    if (month == -1)
                    {
                        if (merchant == -1)
                            sales = pay_transaction.Where(p => p.date.Value.Year == year);
                        else
                            sales = pay_transaction.Where(p => p.idMerchant == merchant && p.date.Value.Year == year);
                    }
                    else if (day == -1)
                    {
                        if (merchant == -1)
                            sales = pay_transaction.Where(p => p.date.Value.Year == year && p.date.Value.Month == month);
                        else
                            sales = pay_transaction.Where(p => p.idMerchant == merchant && p.date.Value.Year == year && p.date.Value.Month == month);
                    }
                    else
                    {
                        if (merchant == -1)
                            sales = pay_transaction.Where(p => p.date.Value.Year == year && p.date.Value.Month == month && p.date.Value.Day == day);
                        else
                            sales = pay_transaction.Where(p => p.idMerchant == merchant && p.date.Value.Year == year && p.date.Value.Month == month);
                    }
                    
                    if (String.IsNullOrEmpty(operation))
                        operation = "1";

                    if (String.IsNullOrEmpty(status))
                        status = "0";

                    if (String.IsNullOrEmpty(cardBrand))
                        cardBrand = "0";

                    switch (operation)
                    {
                        case "1"://"Crédito, Débito, Estorno"
                            sales = sales.Where(p => (p.operation.Value == (int)Business.Enums.Operation.CREDIT || p.operation.Value == (int)Business.Enums.Operation.DEBIT || p.operation.Value == (int)Business.Enums.Operation.REFUND));
                            break;
                        case "2"://"Crédito e Débito"
                            sales = sales.Where(p => (p.operation.Value == (int)Business.Enums.Operation.CREDIT || p.operation.Value == (int)Business.Enums.Operation.DEBIT));
                            break;
                        case "3"://"Crédito"
                            sales = sales.Where(p => p.operation.Value == (int)Business.Enums.Operation.CREDIT);
                            break;
                        case "4"://"Débito"
                            sales = sales.Where(p => p.operation.Value == (int)Business.Enums.Operation.DEBIT);
                            break;
                        case "5"://"Estorno"
                            sales = sales.Where(p => p.operation.Value == (int)Business.Enums.Operation.REFUND);
                            break;
                        case "6"://"Outros"
                            sales = sales.Where(p => (p.operation.Value == (int)Business.Enums.Operation.CONNECTION_TEST || p.operation.Value == (int)Business.Enums.Operation.LOAD_BIN_TABLES));
                            break;
                    }

                    switch (status)
                    {
                        case "1"://"Autorizada"
                            sales = sales.Where(p => p.flStatus == "A");
                            break;
                        case "2"://"Não Autorizada"
                            sales = sales.Where(p => p.flStatus == "D");
                            break;
                        case "3"://"Cancelada"
                            sales = sales.Where(p => p.flStatus == "C");
                            break;
                        case "4"://"Falha/Erro"
                            sales = sales.Where(p => p.flStatus == "F");
                            break;
                        case "5"://"Pendente"
                            sales = sales.Where(p => p.flStatus == "P");
                            break;
                    }

                    if (!cardBrand.Equals("0"))
                    {
                        string cardBrandFilter = cardBrand.PadLeft(5, '0');
                        sales = sales.Where(p => p.cardBrand == cardBrandFilter);
                    }

                    if (!string.IsNullOrEmpty(merchantName))
                    {
                        sales = sales.Where(p => p.Sys_Merchant.dsSoftDescriptor.ToUpper().Contains(merchantName.ToUpper()) || p.Sys_Merchant.dsEmail.ToUpper().Contains(merchantName.ToUpper()));
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

                        int pageSize = 8;
                        int pageNumber = page;

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

        // GET: /Sales/Payment/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            UserManager.IsAuthenticated();
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
        public async Task<ActionResult> Create([Bind(Include = "idTransaction,idMobile,idMerchant,dtCreate,dtLastUpdate,flStatus,acquirer,acquirerNSU,acquirerResponseCode,authorizationNumber,cardBIN,cardBrand,cardBrandCode,clisitefConfirmationData,clisitefRequestNumber,customerEmail,customerPhone,customerReceipt,date,fiscalDate,fiscalHour,installmentAmount,isTest,issuerInstallmentAllowed,maxIssuerInstallments,maxMerchantInstallments,merchantEmail,merchantInstallmentAllowed,merchantName,merchantReceipt,operation,paymentFunction,paymentFunctionDescription,paymentType,pinpadInfo,pinpadSerialNumber,refundDate,refundDocumentNumber,sitefNSU,sitefVersion,state,statusCode,timestamp,token,type,value,latitude,longitude")] Pay_Transaction pay_transaction)
        {
            UserManager.IsAuthenticated();
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
            UserManager.IsAuthenticated();
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
        public async Task<ActionResult> Edit([Bind(Include = "idTransaction,idMobile,idMerchant,dtCreate,dtLastUpdate,flStatus,acquirer,acquirerNSU,acquirerResponseCode,authorizationNumber,cardBIN,cardBrand,cardBrandCode,clisitefConfirmationData,clisitefRequestNumber,customerEmail,customerPhone,customerReceipt,date,fiscalDate,fiscalHour,installmentAmount,isTest,issuerInstallmentAllowed,maxIssuerInstallments,maxMerchantInstallments,merchantEmail,merchantInstallmentAllowed,merchantName,merchantReceipt,operation,paymentFunction,paymentFunctionDescription,paymentType,pinpadInfo,pinpadSerialNumber,refundDate,refundDocumentNumber,sitefNSU,sitefVersion,state,statusCode,timestamp,token,type,value,latitude,longitude")] Pay_Transaction pay_transaction)
        {
            UserManager.IsAuthenticated();
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
            UserManager.IsAuthenticated();
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
            UserManager.IsAuthenticated();
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
