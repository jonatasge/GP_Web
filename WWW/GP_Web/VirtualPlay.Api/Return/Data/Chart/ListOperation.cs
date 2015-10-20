using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualPlay.Business.Models;

namespace VirtualPlay.Api.Return
{
    public class ListOperation
    {
        public ListOperation(List<Pay_TransactionChartOperation_Result> listOperation)
        {
            if (listOperation != null)
            {
                this.Data = new List<Operation>();

                foreach (Pay_TransactionChartOperation_Result item in listOperation)
                {
                    Data.Add(new Operation(item));
                }
            }
        }

        public List<Operation> Data { get; set; }
    }
}