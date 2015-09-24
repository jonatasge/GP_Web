using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualPlay.Api.Return
{
    public class Transaction
    {
        public Transaction()
        {
        }

        public Transaction(Business.Models.Pay_Transaction trans)
        {
            id = trans.idMobile;
            idServer = trans.idTransaction.ToString();
            idMerchant = trans.idMerchant;
            if (trans.dtCreate != null)
                dtCreate = trans.dtCreate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (trans.dtLastUpdate != null)
                dtLastUpdate = trans.dtLastUpdate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (trans.flStatus != null)
                flStatus = trans.flStatus;
            if (trans.acquirer != null)
                acquirer = trans.acquirer;
            if (trans.acquirerNSU != null)
                acquirerNSU = trans.acquirerNSU;
            if (trans.acquirerResponseCode != null)
                acquirerResponseCode = trans.acquirerResponseCode;
            if (trans.authorizationNumber != null)
                authorizationNumber = trans.authorizationNumber;
            if (trans.cardBIN != null)
                cardBIN = trans.cardBIN;
            if (trans.cardBrand != null)
                cardBrand = trans.cardBrand;
            if (trans.cardBrandCode != null)
                cardBrandCode = trans.cardBrandCode;
            if (trans.clisitefConfirmationData != null)
                clisitefConfirmationData = trans.clisitefConfirmationData;
            if (trans.clisitefRequestNumber != null)
                clisitefRequestNumber = trans.clisitefRequestNumber;
            if (trans.customerEmail != null)
                customerEmail = trans.customerEmail;
            if (trans.customerPhone != null)
                customerPhone = trans.customerPhone;
            if (trans.customerReceipt != null)
                customerReceipt = trans.customerReceipt;
            if (trans.date.HasValue)
                date = trans.date.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (trans.fiscalDate != null)
                fiscalDate = trans.fiscalDate;
            if (trans.dtCreate != null)
                fiscalHour = trans.fiscalHour;
            if (trans.installmentAmount != null)
                installmentAmount = trans.installmentAmount;
            if (trans.isTest != null)
                isTest = trans.isTest.Value;
            if (trans.issuerInstallmentAllowed != null)
                issuerInstallmentAllowed = trans.issuerInstallmentAllowed;
            if (trans.maxIssuerInstallments != null)
                maxIssuerInstallments = trans.maxIssuerInstallments;
            if (trans.maxMerchantInstallments != null)
                maxMerchantInstallments = trans.maxMerchantInstallments;
            if (trans.merchantEmail != null)
                merchantEmail = trans.merchantEmail;
            if (trans.merchantInstallmentAllowed != null)
                merchantInstallmentAllowed = trans.merchantInstallmentAllowed;
            if (trans.merchantName != null)
                merchantName = trans.merchantName;
            if (trans.merchantReceipt != null)
                merchantReceipt = trans.merchantReceipt;
            if (trans.operation != null)
                operation = trans.operation.Value;
            if (trans.paymentFunction != null)
                paymentFunction = trans.paymentFunction;
            if (trans.paymentFunctionDescription != null)
                paymentFunctionDescription = trans.paymentFunctionDescription;
            if (trans.paymentType != null)
                paymentType = trans.paymentType;
            if (trans.pinpadInfo != null)
                pinpadInfo = trans.pinpadInfo;
            if (trans.pinpadSerialNumber != null)
                pinpadSerialNumber = trans.pinpadSerialNumber;
            if (trans.refundDate != null)
                refundDate = trans.refundDate;
            if (trans.refundDocumentNumber != null)
                refundDocumentNumber = trans.refundDocumentNumber;
            if (trans.sitefNSU != null)
                sitefNSU = trans.sitefNSU;
            if (trans.sitefVersion != null)
                sitefVersion = trans.sitefVersion;
            if (trans.state != null)
                state = trans.state.Value;
            if (trans.statusCode != null)
                statusCode = trans.statusCode.Value;
            if (trans.timestamp != null)
                timestamp = trans.timestamp;
            if (trans.token != null)
                token = trans.token;
            if (trans.type != null)
                type = trans.type.Value;
            if (trans.value != null)
                value = trans.value;
        }

        public long id { get; set; }
        public string idServer { get; set; }
        public int idMerchant { get; set; }
        public string dtCreate { get; set; }
        public string dtLastUpdate { get; set; }
        public string flStatus { get; set; }
        public string acquirer { get; set; }
        public string acquirerNSU { get; set; }
        public string acquirerResponseCode { get; set; }
        public string authorizationNumber { get; set; }
        public string cardBIN { get; set; }
        public string cardBrand { get; set; }
        public string cardBrandCode { get; set; }
        public string clisitefConfirmationData { get; set; }
        public string clisitefRequestNumber { get; set; }
        public string customerEmail { get; set; }
        public string customerPhone { get; set; }
        public string customerReceipt { get; set; }
        public string date { get; set; }
        public string fiscalDate { get; set; }
        public string fiscalHour { get; set; }
        public string installmentAmount { get; set; }
        public int isTest { get; set; }
        public string issuerInstallmentAllowed { get; set; }
        public string maxIssuerInstallments { get; set; }
        public string maxMerchantInstallments { get; set; }
        public string merchantEmail { get; set; }
        public string merchantInstallmentAllowed { get; set; }
        public string merchantName { get; set; }
        public string merchantReceipt { get; set; }
        public int operation { get; set; }
        public string paymentFunction { get; set; }
        public string paymentFunctionDescription { get; set; }
        public string paymentType { get; set; }
        public string pinpadInfo { get; set; }
        public string pinpadSerialNumber { get; set; }
        public string refundDate { get; set; }
        public string refundDocumentNumber { get; set; }
        public string sitefNSU { get; set; }
        public string sitefVersion { get; set; }
        public int state { get; set; }
        public int statusCode { get; set; }
        public string timestamp { get; set; }
        public string token { get; set; }
        public int type { get; set; }
        public string value { get; set; }
    }
}