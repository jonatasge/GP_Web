using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualPlay.Business.Models;

namespace VirtualPlay.Api.Return
{
    public class Operation
    {
        public Operation(Pay_TransactionChartOperation_Result operation)
        {
            if (operation != null)
            {
                this.operation = operation.operation.Value;
                this.operationDescription = Business.Description.getOperation((Business.Enums.Operation)this.operation);
                this.amount = operation.amount.Value;
                this.value = operation.value.Value;
            }
        }

        public int operation { get; set; }
        public string operationDescription { get; set; }
        public int amount { get; set; }
        public decimal value { get; set; }
    }
}