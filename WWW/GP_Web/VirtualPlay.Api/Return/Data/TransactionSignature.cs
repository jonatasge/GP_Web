using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualPlay.Api.Return
{
    public class TransactionSignature
    {
        public TransactionSignature()
        {
        }

        public TransactionSignature(Business.Models.Pay_TransactionSignature trans)
        {
            idSignature = trans.idSignature;
            idTransaction = trans.idTransaction.ToString();
            imSignature = trans.imSignature;
            if (trans.dtCreate != null)
                dtCreate = trans.dtCreate.Value.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public long idSignature { get; set; }
        public string idTransaction { get; set; }
        public string imSignature { get; set; }
        public string dtCreate { get; set; }
    }
}