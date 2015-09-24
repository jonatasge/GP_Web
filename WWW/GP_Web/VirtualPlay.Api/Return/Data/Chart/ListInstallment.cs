using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualPlay.Business.Models;

namespace VirtualPlay.Api.Return
{
    public class ListInstallment : Response
    {
        public ListInstallment(string session, List<Pay_TransactionChartInstallment_Result> listInstallment)
        {
            this.@return = true;
            this.session = session;

            if (listInstallment != null)
            {
                this.items = new List<Installment>();

                foreach (Pay_TransactionChartInstallment_Result item in listInstallment)
                {
                    items.Add(new Installment(item));
                }
            }
        }

        public string session { get; set; }
        public List<Installment> items { get; set; }
    }
}