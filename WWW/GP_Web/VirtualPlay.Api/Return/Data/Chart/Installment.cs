using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualPlay.Business.Models;

namespace VirtualPlay.Api.Return
{
    public class Installment
    {
        public Installment(Pay_TransactionChartInstallment_Result installment)
        {
            if (installment != null)
            {
                this.operation = installment.operation.Value;
                this.operationDescription = Business.Description.getOperation((Business.Enums.Operation)this.operation);
                this.installment = installment.installment;
                this.amount = installment.amount.Value;
                this.value = installment.value.Value;
            }
        }

        public int operation { get; set; }
        public string operationDescription { get; set; }
        public string installment { get; set; }
        public int amount { get; set; }
        public decimal value { get; set; }
    }
}