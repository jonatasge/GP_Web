using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPlay.Business
{
    public class Description
    {
        public static string getOperation(Enums.Operation operation)
        {
            string operationDescription = string.Empty;

            switch (operation)
            {
                case Enums.Operation.GENERIC:
                    operationDescription = "Pagamento genérico";
                    break;
                case Enums.Operation.CHEQUE:
                    operationDescription = "Cheque";
                    break;
                case Enums.Operation.DEBIT:
                    operationDescription = "Débito";
                    break;
                case Enums.Operation.CREDIT:
                    operationDescription = "Crédito";
                    break;
                case Enums.Operation.VOUCHER:
                    operationDescription = "Voucher";
                    break;
                case Enums.Operation.REFUND:
                    operationDescription = "Estorno de venda";
                    break;
                case Enums.Operation.PREAUTH:
                    operationDescription = "Pré-autorização";
                    break;
                case Enums.Operation.PRINT:
                    operationDescription = "Re-impressão comprovante";
                    break;
                case Enums.Operation.CONNECTION_TEST:
                    operationDescription = "Teste de comunicação com o SiTef";
                    break;
                case Enums.Operation.LOAD_BIN_TABLES:
                    operationDescription = "Carga de tabelas";
                    break;
                case Enums.Operation.UNDEFINED:
                default:
                    operationDescription = "Indefinido";
                    break;
            }

            return operationDescription;
        }

        public static string getCardBrand(Enums.CardBrand cardBrand)
        {
            string cardBrandDescription = string.Empty;

            switch (cardBrand)
            {
                case Enums.CardBrand.VISA:
                    cardBrandDescription = "Visa";
                    break;
                case Enums.CardBrand.MASTERCARD:
                    cardBrandDescription = "Mastercard";
                    break;
                case Enums.CardBrand.DINERS:
                    cardBrandDescription = "Diners";
                    break;
                case Enums.CardBrand.AMEX:
                    cardBrandDescription = "American Express";
                    break;
                case Enums.CardBrand.SOLLO:
                    cardBrandDescription = "Sollo";
                    break;
                case Enums.CardBrand.SIDECARD:
                    cardBrandDescription = "Sidecard (Redecard)";
                    break;
                case Enums.CardBrand.PRIVATE_LABEL:
                    cardBrandDescription = "Private Label (Redecard) ";
                    break;
                case Enums.CardBrand.REDESHOP:
                    cardBrandDescription = "Redeshop";
                    break;
                case Enums.CardBrand.PAO_DE_ACUCAR:
                    cardBrandDescription = "Pão de Açúcar";
                    break;
                case Enums.CardBrand.FININVEST:
                    cardBrandDescription = "Fininvest (Visanet) ";
                    break;
                case Enums.CardBrand.JCB:
                    cardBrandDescription = "JCB";
                    break;
                case Enums.CardBrand.HIPERCARD:
                    cardBrandDescription = "Hipercard";
                    break;
                case Enums.CardBrand.AURA:
                    cardBrandDescription = "Aura";
                    break;
                case Enums.CardBrand.LOSANGO:
                    cardBrandDescription = "Losango";
                    break;
                case Enums.CardBrand.SOROCRED:
                    cardBrandDescription = "Sorocred";
                    break;
                case Enums.CardBrand.DISCOVERY:
                    cardBrandDescription = "Discovery";
                    break;
                case Enums.CardBrand.UNDEFINED:
                    cardBrandDescription = "Outro, não definido";
                    break;
            }

            return cardBrandDescription;
        }

        public static string getStatus(Enums.Status status)
        {
            string statusDescription = string.Empty;

            switch (status)
            {
                case Enums.Status.PENDING:
                    statusDescription = "Incompleta";
                    break;
                case Enums.Status.SENT:
                    statusDescription = "Enviada ao TEF";
                    break;
                case Enums.Status.PRINTING:
                    statusDescription = "Aguardando Confirmação";
                    break;
                case Enums.Status.FINALIZING:
                    statusDescription = "Aguardando Confirmação";
                    break;
                case Enums.Status.FINISHED:
                    statusDescription = "Finalizada";
                    break;
                case Enums.Status.CANCELLING:
                    statusDescription = "Marcada para Cancelamento";
                    break;
                case Enums.Status.CANCELED:
                    statusDescription = "Cancelada";
                    break;
                case Enums.Status.FAILED:
                    statusDescription = "Falhou";
                    break;
            }

            return statusDescription;
        }

        public static string getMonth(int month)
        {
            string statusDescription = string.Empty;

            switch (month)
            {
                case 1:
                    statusDescription = "Janeiro";
                    break;
                case 2:
                    statusDescription = "Fevereiro";
                    break;
                case 3:
                    statusDescription = "Março";
                    break;
                case 4:
                    statusDescription = "Abril";
                    break;
                case 5:
                    statusDescription = "Maio";
                    break;
                case 6:
                    statusDescription = "Junho";
                    break;
                case 7:
                    statusDescription = "Julho";
                    break;
                case 8:
                    statusDescription = "Agosto";
                    break;
                case 9:
                    statusDescription = "Setembro";
                    break;
                case 10:
                    statusDescription = "Outubro";
                    break;
                case 11:
                    statusDescription = "Novembro";
                    break;
                case 12:
                    statusDescription = "Dezembro";
                    break;
            }

            return statusDescription;
        }

    }
}
