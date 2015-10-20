using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using VirtualPlay.Api.Infraestructure;
using VirtualPlay.Api.Return;
using VirtualPlay.Business.Models;
using Newtonsoft.Json;
using System.Data.Entity.Core.Objects;
using System.Text.RegularExpressions;
using System.Data.Entity;
using VirtualPlay.SMTP.Email;

namespace VirtualPlay.Api.Controllers
{
    public class TransactionController : Controller
    {
        // POST: /New/
        [HttpPost]
        public JsonResult New(string session, string email, int merchant, int system)
        {
            using (var db = new Entities())
            {
                Response response = null;

                if (!string.IsNullOrEmpty(email))
                {
                    if (!string.IsNullOrEmpty(session))
                    {
                        var participants = db.Sys_User.Where(z => z.dsEmail == email);

                        List<Sys_User> listParticipant = participants.ToList();

                        if (listParticipant != null && listParticipant.Count > 0)
                        {
                            int active_session = 0;

                            Sys_User participant = db.Sys_User.Find(listParticipant[0].idUser);

                            if (participant.dtLastSession != null)
                            {
                                DateTime dateNow = DateTime.Now;

                                TimeSpan timeSpan = dateNow.Subtract((DateTime)participant.dtLastSession);
                                active_session = timeSpan.Minutes;
                            }

                            if (active_session <= 60 && participant.dsSession.Equals(session))
                            {
                                Stream req = Request.InputStream;
                                req.Seek(0, System.IO.SeekOrigin.Begin);
                                string json = new StreamReader(req).ReadToEnd();

                                dynamic myObj;
                                try
                                {
                                    myObj = JsonConvert.DeserializeObject(json);

                                    string newSession = SessionController.New(email);
                                    SessionController.Write(newSession, participant.idUser, system);

                                    var payTransaction = new Pay_Transaction();
                                    payTransaction.idMerchant = merchant;
                                    payTransaction.dtCreate = DateTime.Now;
                                    payTransaction.dtLastUpdate = DateTime.Now;

                                    payTransaction.date = payTransaction.dtCreate;

                                    if (myObj.merchantEmail != null)
                                        payTransaction.merchantEmail = myObj.merchantEmail; //required
                                    if (myObj.merchantName != null)
                                        payTransaction.merchantName = myObj.merchantName;
                                    if (myObj.merchantInstallmentAllowed != null)
                                        payTransaction.merchantInstallmentAllowed = myObj.merchantInstallmentAllowed;

                                    if (myObj.isTest != null)
                                        payTransaction.isTest = myObj.isTest;
                                    if (myObj.operation != null)
                                        payTransaction.operation = myObj.operation;
                                    if (myObj.pinpadInfo != null)
                                        payTransaction.pinpadInfo = myObj.pinpadInfo;
                                    if (myObj.serial_number != null)
                                        payTransaction.pinpadSerialNumber = myObj.serial_number;
                                    if (myObj.state != null)
                                        payTransaction.state = myObj.state;
                                    if (myObj.statusCode != null)
                                        payTransaction.statusCode = myObj.statusCode;
                                    if (myObj.type != null)
                                        payTransaction.type = myObj.type;
                                    if (myObj.value != null)
                                        payTransaction.value = myObj.value;

                                    if (myObj.latitude != null)
                                        payTransaction.latitude = myObj.latitude;
                                    if (myObj.longitude != null)
                                        payTransaction.longitude = myObj.longitude;

                                    if (myObj.flStatus != null && !((string)myObj.flStatus).Equals("null"))
                                        payTransaction.flStatus = myObj.flStatus;

                                    db.Pay_Transaction.Add(payTransaction);
                                    db.SaveChanges();

                                    response = new PayRequest(newSession, payTransaction);
                                }
                                catch (Exception ex)
                                {
                                    response = new ResponseFailure("invalid-data");
                                }
                            }
                            else
                            {
                                response = new ResponseFailure("invalid-session");
                            }
                        }
                        else
                        {
                            response = new ResponseFailure("invalid-email");
                        }
                    }
                    else
                    {
                        response = new ResponseFailure("invalid-session");
                    }
                }
                else
                {
                    response = new ResponseFailure("invalid-email");
                }

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: /Update/
        [HttpPost]
        public JsonResult Update(string session, string email, int merchant, int system)
        {
            using (var db = new Entities())
            {
                Response response = null;

                if (!string.IsNullOrEmpty(email))
                {
                    if (!string.IsNullOrEmpty(session))
                    {
                        var participants = db.Sys_User.Where(z => z.dsEmail == email);

                        List<Sys_User> listParticipant = participants.ToList();

                        if (listParticipant != null && listParticipant.Count > 0)
                        {
                            int active_session = 0;

                            Sys_User participant = db.Sys_User.Find(listParticipant[0].idUser);

                            if (participant.dtLastSession != null)
                            {
                                DateTime dateNow = DateTime.Now;

                                TimeSpan timeSpan = dateNow.Subtract((DateTime)participant.dtLastSession);
                                active_session = timeSpan.Minutes;
                            }

                            if (active_session <= 60 && participant.dsSession.Equals(session))
                            {
                                Stream req = Request.InputStream;
                                req.Seek(0, System.IO.SeekOrigin.Begin);
                                string json = new StreamReader(req).ReadToEnd();

                                dynamic myObj;
                                try
                                {
                                    myObj = JsonConvert.DeserializeObject(json);

                                    string newSession = SessionController.New(email);
                                    SessionController.Write(newSession, participant.idUser, system);

                                    if (myObj.id != null & myObj.id > 0)
                                    {
                                        Pay_Transaction payTransaction = null;
                                        long idMobile = myObj.id;
                                        Guid idServer = Guid.Empty;
                                        string strIdServer = null;

                                        if (myObj.idServer != null)
                                            strIdServer = myObj.idServer;

                                        if (!string.IsNullOrEmpty(strIdServer) && Guid.TryParse(strIdServer, out idServer))
                                        {
                                            payTransaction = db.Pay_Transaction.Where(z => z.idTransaction == idServer).FirstOrDefault();
                                        }
                                        else
                                        {
                                            payTransaction = db.Pay_Transaction.Where(z => z.idMobile == idMobile).FirstOrDefault();
                                        }

                                        if (payTransaction != null)
                                        {
                                            if (myObj.token != null && !((string)myObj.token).Equals("null"))
                                                payTransaction.token = myObj.token;
                                            if (myObj.serial_number != null && !((string)myObj.serial_number).Equals("null"))
                                                payTransaction.pinpadSerialNumber = myObj.serial_number;

                                            //if (myObj.techonology != null)

                                            if (myObj.type != null)
                                                payTransaction.type = myObj.type;
                                            if (myObj.operation != null)
                                                payTransaction.operation = myObj.operation;
                                            if (myObj.state != null)
                                                payTransaction.state = myObj.state;

                                            if (myObj.fiscalDate != null && !((string)myObj.fiscalDate).Equals("null"))
                                                payTransaction.fiscalDate = myObj.fiscalDate;
                                            if (myObj.fiscalHour != null && !((string)myObj.fiscalHour).Equals("null"))
                                                payTransaction.fiscalHour = myObj.fiscalHour;

                                            if (myObj.cs_pinpad_info != null && !((string)myObj.cs_pinpad_info).Equals("null"))
                                                payTransaction.pinpadInfo = myObj.cs_pinpad_info;
                                            if (myObj.value != null && !((string)myObj.value).Equals("null"))
                                                payTransaction.value = myObj.value;
                                            if (myObj.status != null)
                                                payTransaction.statusCode = myObj.status;

                                            //if (myObj.message != null && !((string)myObj.message).Equals("null"))
                                            //payTransaction.statusMessage = myObj.message;

                                            if (myObj.type != null)
                                                payTransaction.type = myObj.type;
                                            if (myObj.creditcard != null && !((string)myObj.creditcard).Equals("null"))
                                                payTransaction.cardBIN = myObj.creditcard;
                                            if (myObj.card_brand != null && !((string)myObj.card_brand).Equals("null"))
                                                payTransaction.cardBrand = myObj.card_brand;
                                            if (myObj.card_type != null && !((string)myObj.card_type).Equals("null"))
                                                payTransaction.cardType = myObj.card_type;
                                            if (myObj.installments != null && !((string)myObj.installments).Equals("null"))
                                                payTransaction.installmentAmount = myObj.installments;
                                            if (myObj.nsu != null && !((string)myObj.nsu).Equals("null"))
                                                payTransaction.acquirerNSU = myObj.nsu;
                                            if (myObj.auth_code != null && !((string)myObj.auth_code).Equals("null"))
                                                payTransaction.authorizationNumber = myObj.auth_code;
                                            if (myObj.return_code != null && !((string)myObj.return_code).Equals("null"))
                                                payTransaction.acquirerResponseCode = myObj.return_code;
                                            if (myObj.cs_payment_type != null && !((string)myObj.cs_payment_type).Equals("null"))
                                                payTransaction.paymentType = myObj.cs_payment_type;
                                            if (myObj.cs_payment_function != null && !((string)myObj.cs_payment_function).Equals("null"))
                                                payTransaction.paymentFunction = myObj.cs_payment_function;
                                            if (myObj.cs_payment_function_description != null && !((string)myObj.cs_payment_function_description).Equals("null"))
                                                payTransaction.paymentFunctionDescription = myObj.cs_payment_function_description;
                                            if (myObj.cs_card_brand_number != null && !((string)myObj.cs_card_brand_number).Equals("null"))
                                                payTransaction.cardBrandCode = myObj.cs_card_brand_number;
                                            if (myObj.cs_sitef_nsu != null && !((string)myObj.cs_sitef_nsu).Equals("null"))
                                                payTransaction.sitefNSU = myObj.cs_sitef_nsu;
                                            if (myObj.cs_sitef_request_number != null && !((string)myObj.cs_sitef_request_number).Equals("null"))
                                                payTransaction.clisitefRequestNumber = myObj.cs_sitef_request_number;
                                            if (myObj.cs_sitef_confirmation_data != null && !((string)myObj.cs_sitef_confirmation_data).Equals("null"))
                                                payTransaction.clisitefConfirmationData = myObj.cs_sitef_confirmation_data;
                                            if (myObj.cs_sitef_refund_date != null && !((string)myObj.cs_sitef_refund_date).Equals("null"))
                                                payTransaction.refundDate = myObj.cs_sitef_refund_date;
                                            if (myObj.cs_sitef_refund_number != null && !((string)myObj.cs_sitef_refund_number).Equals("null"))
                                                payTransaction.refundDocumentNumber = myObj.cs_sitef_refund_number;
                                            if (myObj.cs_pinpad_info != null && !((string)myObj.cs_pinpad_info).Equals("null"))
                                                payTransaction.pinpadInfo = myObj.cs_pinpad_info;
                                            if (myObj.cs_sitef_version != null && !((string)myObj.cs_sitef_version).Equals("null"))
                                                payTransaction.sitefVersion = myObj.cs_sitef_version;
                                            if (myObj.cs_merchant_installments_allowed != null && !((string)myObj.cs_merchant_installments_allowed).Equals("null"))
                                                payTransaction.merchantInstallmentAllowed = myObj.cs_merchant_installments_allowed;
                                            if (myObj.cs_issuer_installments_allowed != null && !((string)myObj.cs_issuer_installments_allowed).Equals("null"))
                                                payTransaction.issuerInstallmentAllowed = myObj.cs_issuer_installments_allowed;
                                            if (myObj.cs_max_merchant_installments != null && !((string)myObj.cs_max_merchant_installments).Equals("null"))
                                                payTransaction.maxMerchantInstallments = myObj.cs_max_merchant_installments;
                                            if (myObj.cs_max_issuer_installments != null && !((string)myObj.cs_max_issuer_installments).Equals("null"))
                                                payTransaction.maxIssuerInstallments = myObj.cs_max_issuer_installments;
                                            if (myObj.cs_customer_receipt != null && !((string)myObj.cs_customer_receipt).Equals("null"))
                                                payTransaction.customerReceipt = myObj.cs_customer_receipt;
                                            if (myObj.cs_merchant_receipt != null && !((string)myObj.cs_merchant_receipt).Equals("null"))
                                                payTransaction.merchantReceipt = myObj.cs_merchant_receipt;

                                            if (myObj.flStatus != null && !((string)myObj.flStatus).Equals("null"))
                                                payTransaction.flStatus = myObj.flStatus;

                                            payTransaction.dtLastUpdate = DateTime.Now;

                                            db.Entry(payTransaction).State = EntityState.Modified;
                                            db.SaveChanges();

                                            response = new PayRequest(newSession, payTransaction);
                                        }
                                        else
                                        {
                                            response = new ResponseFailure("invalid-data");
                                        }
                                    }
                                    else
                                    {
                                        response = new ResponseFailure("invalid-data");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    response = new ResponseFailure("invalid-data");
                                }
                            }
                            else
                            {
                                response = new ResponseFailure("invalid-session");
                            }
                        }
                        else
                        {
                            response = new ResponseFailure("invalid-email");
                        }
                    }
                    else
                    {
                        response = new ResponseFailure("invalid-session");
                    }
                }
                else
                {
                    response = new ResponseFailure("invalid-email");
                }

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: /Update/
        [HttpPost]
        public JsonResult Refund(string session, string email, int merchant, int system)
        {
            using (var db = new Entities())
            {
                Response response = null;

                if (!string.IsNullOrEmpty(email))
                {
                    if (!string.IsNullOrEmpty(session))
                    {
                        var participants = db.Sys_User.Where(z => z.dsEmail == email);

                        List<Sys_User> listParticipant = participants.ToList();

                        if (listParticipant != null && listParticipant.Count > 0)
                        {
                            int active_session = 0;

                            Sys_User participant = db.Sys_User.Find(listParticipant[0].idUser);

                            if (participant.dtLastSession != null)
                            {
                                DateTime dateNow = DateTime.Now;

                                TimeSpan timeSpan = dateNow.Subtract((DateTime)participant.dtLastSession);
                                active_session = timeSpan.Minutes;
                            }

                            if (active_session <= 60 && participant.dsSession.Equals(session))
                            {
                                Stream req = Request.InputStream;
                                req.Seek(0, System.IO.SeekOrigin.Begin);
                                string json = new StreamReader(req).ReadToEnd();

                                dynamic myObj;
                                try
                                {
                                    myObj = JsonConvert.DeserializeObject(json);

                                    string newSession = SessionController.New(email);
                                    SessionController.Write(newSession, participant.idUser, system);

                                    if (myObj.id != null & myObj.id > 0)
                                    {
                                        Pay_Transaction payTransaction = new Pay_Transaction();
                                        payTransaction.idMerchant = merchant;
                                        payTransaction.dtCreate = DateTime.Now;
                                        payTransaction.dtLastUpdate = DateTime.Now;
                                        
                                        long idMobile = myObj.id;
                                        Guid idServer = Guid.Empty;
                                        string strIdServer = null;

                                        if (myObj.idServer != null)
                                            strIdServer = myObj.idServer;

                                        if (myObj.token != null && !((string)myObj.token).Equals("null"))
                                            payTransaction.token = myObj.token;
                                        if (myObj.serial_number != null && !((string)myObj.serial_number).Equals("null"))
                                            payTransaction.pinpadSerialNumber = myObj.serial_number;

                                        //if (myObj.techonology != null)

                                        if (myObj.type != null)
                                            payTransaction.type = myObj.type;
                                        if (myObj.operation != null)
                                            payTransaction.operation = myObj.operation;
                                        if (myObj.state != null)
                                            payTransaction.state = myObj.state;

                                        if (myObj.fiscalDate != null && !((string)myObj.fiscalDate).Equals("null"))
                                            payTransaction.fiscalDate = myObj.fiscalDate;
                                        if (myObj.fiscalHour != null && !((string)myObj.fiscalHour).Equals("null"))
                                            payTransaction.fiscalHour = myObj.fiscalHour;

                                        if (myObj.cs_pinpad_info != null && !((string)myObj.cs_pinpad_info).Equals("null"))
                                            payTransaction.pinpadInfo = myObj.cs_pinpad_info;
                                        if (myObj.value != null && !((string)myObj.value).Equals("null"))
                                            payTransaction.value = myObj.value;
                                        if (myObj.status != null)
                                            payTransaction.statusCode = myObj.status;

                                        //if (myObj.message != null && !((string)myObj.message).Equals("null"))
                                        //payTransaction.statusMessage = myObj.message;

                                        if (myObj.type != null)
                                            payTransaction.type = myObj.type;
                                        if (myObj.creditcard != null && !((string)myObj.creditcard).Equals("null"))
                                            payTransaction.cardBIN = myObj.creditcard;
                                        if (myObj.card_brand != null && !((string)myObj.card_brand).Equals("null"))
                                            payTransaction.cardBrand = myObj.card_brand;
                                        if (myObj.card_type != null && !((string)myObj.card_type).Equals("null"))
                                            payTransaction.cardType = myObj.card_type;
                                        if (myObj.installments != null && !((string)myObj.installments).Equals("null"))
                                            payTransaction.installmentAmount = myObj.installments;
                                        if (myObj.nsu != null && !((string)myObj.nsu).Equals("null"))
                                            payTransaction.acquirerNSU = myObj.nsu;
                                        if (myObj.auth_code != null && !((string)myObj.auth_code).Equals("null"))
                                            payTransaction.authorizationNumber = myObj.auth_code;
                                        if (myObj.return_code != null && !((string)myObj.return_code).Equals("null"))
                                            payTransaction.acquirerResponseCode = myObj.return_code;
                                        if (myObj.cs_payment_type != null && !((string)myObj.cs_payment_type).Equals("null"))
                                            payTransaction.paymentType = myObj.cs_payment_type;
                                        if (myObj.cs_payment_function != null && !((string)myObj.cs_payment_function).Equals("null"))
                                            payTransaction.paymentFunction = myObj.cs_payment_function;
                                        if (myObj.cs_payment_function_description != null && !((string)myObj.cs_payment_function_description).Equals("null"))
                                            payTransaction.paymentFunctionDescription = myObj.cs_payment_function_description;
                                        if (myObj.cs_card_brand_number != null && !((string)myObj.cs_card_brand_number).Equals("null"))
                                            payTransaction.cardBrandCode = myObj.cs_card_brand_number;
                                        if (myObj.cs_sitef_nsu != null && !((string)myObj.cs_sitef_nsu).Equals("null"))
                                            payTransaction.sitefNSU = myObj.cs_sitef_nsu;
                                        if (myObj.cs_sitef_request_number != null && !((string)myObj.cs_sitef_request_number).Equals("null"))
                                            payTransaction.clisitefRequestNumber = myObj.cs_sitef_request_number;
                                        if (myObj.cs_sitef_confirmation_data != null && !((string)myObj.cs_sitef_confirmation_data).Equals("null"))
                                            payTransaction.clisitefConfirmationData = myObj.cs_sitef_confirmation_data;
                                        if (myObj.cs_sitef_refund_date != null && !((string)myObj.cs_sitef_refund_date).Equals("null"))
                                            payTransaction.refundDate = myObj.cs_sitef_refund_date;
                                        if (myObj.cs_sitef_refund_number != null && !((string)myObj.cs_sitef_refund_number).Equals("null"))
                                            payTransaction.refundDocumentNumber = myObj.cs_sitef_refund_number;
                                        if (myObj.cs_pinpad_info != null && !((string)myObj.cs_pinpad_info).Equals("null"))
                                            payTransaction.pinpadInfo = myObj.cs_pinpad_info;
                                        if (myObj.cs_sitef_version != null && !((string)myObj.cs_sitef_version).Equals("null"))
                                            payTransaction.sitefVersion = myObj.cs_sitef_version;
                                        if (myObj.cs_merchant_installments_allowed != null && !((string)myObj.cs_merchant_installments_allowed).Equals("null"))
                                            payTransaction.merchantInstallmentAllowed = myObj.cs_merchant_installments_allowed;
                                        if (myObj.cs_issuer_installments_allowed != null && !((string)myObj.cs_issuer_installments_allowed).Equals("null"))
                                            payTransaction.issuerInstallmentAllowed = myObj.cs_issuer_installments_allowed;
                                        if (myObj.cs_max_merchant_installments != null && !((string)myObj.cs_max_merchant_installments).Equals("null"))
                                            payTransaction.maxMerchantInstallments = myObj.cs_max_merchant_installments;
                                        if (myObj.cs_max_issuer_installments != null && !((string)myObj.cs_max_issuer_installments).Equals("null"))
                                            payTransaction.maxIssuerInstallments = myObj.cs_max_issuer_installments;
                                        if (myObj.cs_customer_receipt != null && !((string)myObj.cs_customer_receipt).Equals("null"))
                                            payTransaction.customerReceipt = myObj.cs_customer_receipt;
                                        if (myObj.cs_merchant_receipt != null && !((string)myObj.cs_merchant_receipt).Equals("null"))
                                            payTransaction.merchantReceipt = myObj.cs_merchant_receipt;

                                        if (myObj.flStatus != null && !((string)myObj.flStatus).Equals("null"))
                                            payTransaction.flStatus = myObj.flStatus;

                                        payTransaction.dtLastUpdate = DateTime.Now;

                                        Pay_Transaction payTransactionUpd = null;
                                        if (!string.IsNullOrEmpty(strIdServer) && Guid.TryParse(strIdServer, out idServer))
                                        {
                                            payTransactionUpd = db.Pay_Transaction.Where(z => z.idTransaction == idServer).FirstOrDefault();
                                        }
                                        else
                                        {
                                            payTransactionUpd = db.Pay_Transaction.Where(z => z.idMobile == idMobile).FirstOrDefault();
                                        }

                                        if (payTransaction != null)
                                        {
                                            payTransaction.idTransactionParent = payTransactionUpd.idTransaction;

                                            payTransactionUpd.flStatus = "R";
                                            payTransactionUpd.dtLastUpdate = DateTime.Now;

                                            db.Entry(payTransactionUpd).State = EntityState.Modified;
                                            db.SaveChanges();
                                        }
                                    
                                        db.Entry(payTransaction).State = EntityState.Added;
                                        db.SaveChanges();

                                        response = new PayRequest(newSession, payTransaction);
                                    }
                                    else
                                    {
                                        response = new ResponseFailure("invalid-data");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    response = new ResponseFailure("invalid-data");
                                }
                            }
                            else
                            {
                                response = new ResponseFailure("invalid-session");
                            }
                        }
                        else
                        {
                            response = new ResponseFailure("invalid-email");
                        }
                    }
                    else
                    {
                        response = new ResponseFailure("invalid-session");
                    }
                }
                else
                {
                    response = new ResponseFailure("invalid-email");
                }

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: /SendReceipt/
        [HttpPost]
        public JsonResult SendReceipt(string session, string email, int merchant, int system)
        {
            using (var db = new Entities())
            {
                Response response = null;

                if (!string.IsNullOrEmpty(email))
                {
                    if (!string.IsNullOrEmpty(session))
                    {
                        var participants = db.Sys_User.Where(z => z.dsEmail == email);

                        List<Sys_User> listParticipant = participants.ToList();

                        if (listParticipant != null && listParticipant.Count > 0)
                        {
                            int active_session = 0;

                            Sys_User participant = db.Sys_User.Find(listParticipant[0].idUser);

                            if (participant.dtLastSession != null)
                            {
                                DateTime dateNow = DateTime.Now;

                                TimeSpan timeSpan = dateNow.Subtract((DateTime)participant.dtLastSession);
                                active_session = timeSpan.Minutes;
                            }

                            if (active_session <= 60 && participant.dsSession.Equals(session))
                            {
                                Stream req = Request.InputStream;
                                req.Seek(0, System.IO.SeekOrigin.Begin);
                                string json = new StreamReader(req).ReadToEnd();

                                dynamic myObj;
                                try
                                {
                                    myObj = JsonConvert.DeserializeObject(json);

                                    string newSession = SessionController.New(email);
                                    SessionController.Write(newSession, participant.idUser, system);

                                    if (myObj.id != null & myObj.id > 0)
                                    {
                                        Pay_Transaction payTransaction = null;
                                        int idSystem = 0;
                                        bool isMerchantReceiptSend = false;
                                        bool isCustomerReceiptSend = false;
                                        bool isCustomerReceiptPhoneSend = false;

                                        long idMobile = myObj.id;
                                        Guid idServer = Guid.Empty;
                                        string strIdServer = null;

                                        if (myObj.idServer != null)
                                            strIdServer = myObj.idServer;

                                        if (!string.IsNullOrEmpty(strIdServer) && Guid.TryParse(strIdServer, out idServer))
                                        {
                                            payTransaction = db.Pay_Transaction.Where(z => z.idTransaction == idServer).FirstOrDefault();
                                        }
                                        else
                                        {
                                            payTransaction = db.Pay_Transaction.Where(z => z.idMobile == idMobile).FirstOrDefault();
                                        }

                                        if (myObj.idSystem != null)
                                            idSystem = myObj.idSystem;

                                        if (payTransaction != null)
                                        {
                                            if (myObj.cs_customer_receipt != null && !((string)myObj.cs_customer_receipt).Equals("null"))
                                            {
                                                payTransaction.customerReceipt = myObj.cs_customer_receipt;
                                            }
                                            if (myObj.cs_merchant_receipt != null && !((string)myObj.cs_merchant_receipt).Equals("null"))
                                            {
                                                payTransaction.merchantReceipt = myObj.cs_merchant_receipt;
                                                isMerchantReceiptSend = true;
                                            }
                                            if (myObj.cardholder_email != null && !((string)myObj.cardholder_email).Equals("null") && !((string)myObj.cardholder_email).Equals(""))
                                            {
                                                payTransaction.customerEmail = myObj.cardholder_email;
                                                isCustomerReceiptSend = true;
                                            }
                                            if (myObj.cardholder_phone != null && !((string)myObj.cardholder_phone).Equals("null") && !((string)myObj.cardholder_phone).Equals(""))
                                            {
                                                payTransaction.customerPhone = myObj.cardholder_phone;
                                                if (payTransaction.customerPhone.Length >= 10)
                                                    isCustomerReceiptPhoneSend = true;
                                            }

                                            db.Entry(payTransaction).State = EntityState.Modified;
                                            db.SaveChanges();

                                            Message msgEmail = new Message(int.Parse(ConfigurationManager.AppSettings["CustomerIdWebMail"]));
                                            msgEmail.ConnectionStrings = ConfigurationManager.AppSettings["ConnectionString"];
                                            msgEmail.Schema = ConfigurationManager.AppSettings["CustomerMailSchema"];
                                            if (payTransaction.operation.Value != (int)Business.Enums.Operation.REFUND)
                                            {
                                                msgEmail.Subject = ConfigurationManager.AppSettings["CustomerMailSubject"];
                                            }
                                            else
                                            {
                                                msgEmail.Subject = ConfigurationManager.AppSettings["CustomerMailSubjectRefund"];
                                            }
                                            msgEmail.cdIdentification1 = payTransaction.idTransaction.ToString();

                                            msgEmail.IdSystem = idSystem;
                                            msgEmail.IdUserCreate = participant.idUser;

                                            if (isCustomerReceiptSend)
                                            {
                                                msgEmail.MailTo = payTransaction.customerEmail;
                                                msgEmail.Body = payTransaction.customerReceipt;
                                                msgEmail.Send();
                                            }

                                            if (isMerchantReceiptSend)
                                            {
                                                msgEmail.MailTo = payTransaction.merchantEmail;
                                                msgEmail.Body = payTransaction.merchantReceipt;
                                                msgEmail.Send();
                                            }

                                            if (isCustomerReceiptPhoneSend)
                                            {
                                                try
                                                {
                                                    VirtualPlay.Direct100.SMS.Authentication authSMS =
                                                        new VirtualPlay.Direct100.SMS.Authentication(ConfigurationManager.AppSettings["Direct100_User"]
                                                                         , ConfigurationManager.AppSettings["Direct100_Password"]);

                                                    if (authSMS.IsAuthenticate())
                                                    {
                                                        VirtualPlay.Direct100.SMS.Message.Result msgResult;
                                                        VirtualPlay.Direct100.SMS.Message sendMessage = new VirtualPlay.Direct100.SMS.Message(authSMS.User);
                                                        sendMessage.Schema = ConfigurationManager.AppSettings["CustomerMailSchema"];
                                                        sendMessage.IdSystem = idSystem;
                                                        sendMessage.IdUser = participant.idUser;
                                                        sendMessage.Extra = payTransaction.idTransaction.ToString();
                                                        sendMessage.ConnectionStrings =
                                                                ConfigurationManager.AppSettings["ConnectionString"];

                                                        string numberPhone = string.Concat("55", payTransaction.customerPhone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", ""));
                                                        string messageString = string.Empty;

                                                        if (payTransaction.operation.Value != (int)Business.Enums.Operation.REFUND)
                                                        {
                                                            messageString =
                                                                ConfigurationManager.AppSettings["CustomerPaymentSMS"]
                                                                                     .Replace("#CARTAO#"
                                                                                            , payTransaction.paymentFunctionDescription)
                                                                                     .Replace("#VALOR#"
                                                                                            , payTransaction.value)
                                                                                     .Replace("#ESTABELECIMENTO#"
                                                                                            , payTransaction.merchantName)
                                                                                     .Replace("#dd-MM-yy HH:mm#"
                                                                                            , payTransaction.date.Value.ToString("dd-MM-yy HH:mm"));
                                                        }
                                                        else
                                                        {
                                                            messageString =
                                                                ConfigurationManager.AppSettings["CustomerPaymentSMSRefund"]
                                                                                     .Replace("#CARTAO#"
                                                                                            , payTransaction.paymentFunctionDescription)
                                                                                     .Replace("#VALOR#"
                                                                                            , payTransaction.value)
                                                                                     .Replace("#ESTABELECIMENTO#"
                                                                                            , payTransaction.merchantName)
                                                                                     .Replace("#dd-MM-yy HH:mm#"
                                                                                            , payTransaction.date.Value.ToString("dd-MM-yy HH:mm"));
                                                        }
                                                        msgResult = sendMessage.Send(authSMS.Token, numberPhone, messageString);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    //Erro ou não foi possível enviar o SMS
                                                }
                                            }

                                            response = new PayRequest(newSession, payTransaction);
                                        }
                                        else
                                        {
                                            response = new ResponseFailure("invalid-data");
                                        }
                                    }
                                    else
                                    {
                                        response = new ResponseFailure("invalid-data");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    response = new ResponseFailure("invalid-data");
                                }
                            }
                            else
                            {
                                response = new ResponseFailure("invalid-session");
                            }
                        }
                        else
                        {
                            response = new ResponseFailure("invalid-email");
                        }
                    }
                    else
                    {
                        response = new ResponseFailure("invalid-session");
                    }
                }
                else
                {
                    response = new ResponseFailure("invalid-email");
                }

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: /NewSignature/
        [HttpPost]
        public JsonResult NewSignature(string session, string email, int system)
        {
            using (var db = new Entities())
            {
                Response response = null;

                if (!string.IsNullOrEmpty(email))
                {
                    if (!string.IsNullOrEmpty(session))
                    {
                        var participants = db.Sys_User.Where(z => z.dsEmail == email);

                        List<Sys_User> listParticipant = participants.ToList();

                        if (listParticipant != null && listParticipant.Count > 0)
                        {
                            int active_session = 0;

                            Sys_User participant = db.Sys_User.Find(listParticipant[0].idUser);

                            if (participant.dtLastSession != null)
                            {
                                DateTime dateNow = DateTime.Now;

                                TimeSpan timeSpan = dateNow.Subtract((DateTime)participant.dtLastSession);
                                active_session = timeSpan.Minutes;
                            }

                            if (active_session <= 60 && participant.dsSession.Equals(session))
                            {
                                Stream req = Request.InputStream;
                                req.Seek(0, System.IO.SeekOrigin.Begin);
                                string json = new StreamReader(req).ReadToEnd();

                                dynamic myObj;
                                try
                                {
                                    myObj = JsonConvert.DeserializeObject(json.Replace(@"\", ""));

                                    string newSession = SessionController.New(email);
                                    SessionController.Write(newSession, participant.idUser, system);

                                    Pay_Transaction payTrans = null;
                                    long idMobile = 0;

                                    var payTransaction = new Pay_TransactionSignature();
                                    payTransaction.dtCreate = DateTime.Now;

                                    if (myObj.idServer != null)
                                        payTransaction.idTransaction = myObj.idServer; //required

                                    if (myObj.id != null)
                                        idMobile = myObj.id;

                                    if (myObj.signature != null)
                                    {
                                        string signature = myObj.signature;
                                        payTransaction.imSignature = FixBase64ForImage(signature);
                                    }

                                    if (myObj.createdAt != null)
                                    {

                                    }

                                    payTrans = db.Pay_Transaction.Where(p => p.idMobile == idMobile).FirstOrDefault();
                                    if (payTrans != null)
                                    {
                                        payTransaction.idTransaction = payTrans.idTransaction; //required
                                    }

                                    db.Pay_TransactionSignature.Add(payTransaction);
                                    db.SaveChanges();

                                    if (payTrans != null)
                                    {
                                        payTrans.idSignature = payTransaction.idSignature;
                                        payTrans.dtLastUpdate = DateTime.Now;

                                        db.Entry(payTrans).State = EntityState.Modified;
                                    }

                                    db.SaveChanges();

                                    response = new PaySignature(newSession, payTransaction);
                                }
                                catch (Exception ex)
                                {
                                    response = new ResponseFailure("invalid-data");
                                }
                            }
                            else
                            {
                                response = new ResponseFailure("invalid-session");
                            }
                        }
                        else
                        {
                            response = new ResponseFailure("invalid-email");
                        }
                    }
                    else
                    {
                        response = new ResponseFailure("invalid-session");
                    }
                }
                else
                {
                    response = new ResponseFailure("invalid-email");
                }

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        public string FixBase64ForImage(string Image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);
            sbText.Replace(@"\n", "");
            return sbText.ToString();
        }
    }
}
