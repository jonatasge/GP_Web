using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualPlay.Business.Models;

namespace VirtualPlay.Api.Return
{
    public class CardBrand
    {
        public CardBrand(Pay_TransactionChartCardBrand_Result cardBrand)
        {
            if (cardBrand != null)
            {
                this.cardBrand = cardBrand.cardBrand;
                this.cardBrandDescription = Business.Description.getCardBrand((Business.Enums.CardBrand)int.Parse(this.cardBrand));
                this.amount = cardBrand.amount.Value;
                this.value = cardBrand.value.Value;
            }
        }

        public string cardBrand { get; set; }
        public string cardBrandDescription { get; set; }
        public int amount { get; set; }
        public decimal value { get; set; }
    }
}