using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPlay.Business
{
    public class Enums
    {
        public enum Operation
        {
            GENERIC = 0,
            CHEQUE = 1,
            DEBIT = 2,
            CREDIT = 3,
            VOUCHER = 4,
            REFUND = 5,
            PREAUTH = 6,
            PRINT = 7,
            CONNECTION_TEST = 8,
            LOAD_BIN_TABLES = 9,
            UNDEFINED = 10
        }

        public enum CardBrand
        {
            UNDEFINED = 0,
            VISA = 1,
            MASTERCARD = 2,
            DINERS = 3,
            AMEX = 4,
            SOLLO = 5,
            SIDECARD = 6,
            PRIVATE_LABEL = 7,
            REDESHOP = 8,
            PAO_DE_ACUCAR = 9,
            FININVEST = 10,
            JCB = 11,
            HIPERCARD = 12,
            AURA = 13,
            LOSANGO = 14,
            SOROCRED = 15,
            DISCOVERY = 10014
        }

        public enum Status
        {
            PENDING = 0,
            SENT = 1,
            PRINTING =2,
            FINALIZING = 3,
            FINISHED = 4,
            CANCELLING = 5,
            CANCELED = 6,
            FAILED = 7
        }
    }
}
