using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualPlay.Business.Models;

namespace VirtualPlay.Api.Return
{
    public class PaySignature : Response
    {
        public PaySignature(string session, Business.Models.Pay_TransactionSignature payTransaction)
        {
            this.@return = true;
            this.session = session;

            transactionSignature = new TransactionSignature(payTransaction);
        }

        public string session { get; set; }
        public TransactionSignature transactionSignature { get; set; }
    }
}