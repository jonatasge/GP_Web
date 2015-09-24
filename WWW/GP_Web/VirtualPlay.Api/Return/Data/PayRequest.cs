using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualPlay.Business.Models;

namespace VirtualPlay.Api.Return
{
    public class PayRequest : Response
    {
        public PayRequest(string session, Business.Models.Pay_Transaction payTransaction)
        {
            this.@return = true;
            this.session = session;

            transaction = new Transaction(payTransaction);
        }

        public string session { get; set; }
        public Transaction transaction { get; set; }
    }
}