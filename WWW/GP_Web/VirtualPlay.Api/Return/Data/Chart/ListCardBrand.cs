using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualPlay.Business.Models;

namespace VirtualPlay.Api.Return
{
    public class ListCardBrand : Response
    {
        public ListCardBrand(string session, List<Pay_TransactionChartCardBrand_Result> listCardBrand)
        {
            this.@return = true;
            this.session = session;

            if (listCardBrand != null)
            {
                this.items = new List<CardBrand>();

                foreach (Pay_TransactionChartCardBrand_Result item in listCardBrand)
                {
                    items.Add(new CardBrand(item));
                }
            }
        }

        public string session { get; set; }
        public List<CardBrand> items { get; set; }
    }
}