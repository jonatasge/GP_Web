using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualPlay.Business.Models;

namespace VirtualPlay.Api.Return
{
    public class ListOperation : Response
    {
        public ListOperation(string session, List<Pay_TransactionChartOperation_Result> listOperation)
        {
            this.@return = true;
            this.session = session;

            if (listOperation != null)
            {
                this.items = new List<Operation>();

                foreach (Pay_TransactionChartOperation_Result item in listOperation)
                {
                    items.Add(new Operation(item));
                }
            }
        }

        public string session { get; set; }
        public List<Operation> items { get; set; }
    }
}