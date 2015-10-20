using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

using VirtualPlay.Business;

namespace VirtualPlay.MyAccount
{
    public static class DDLHelper
    {
        public static IList<SelectListItem> GetYears()
        {
            IList<SelectListItem> _result = new List<SelectListItem>();

            for (int i = DateTime.Now.Year; i >= DateTime.Now.AddYears(-5).Year; i--)
            {
                _result.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString(), Selected = (i == DateTime.Now.Year ? true : false) });
            }

            return _result;
        }

        public static IList<SelectListItem> GetMonths()
        {
            IList<SelectListItem> _result = new List<SelectListItem>();

            _result.Add(new SelectListItem { Value = "-1", Text = "Selecione o Mês", Selected = true });
            for (int i = 1; i <= 12; i++)
            {
                _result.Add(new SelectListItem { Value = i.ToString(), Text = Business.Description.getMonth(i) });
            }

            return _result;
        }

        public static IList<SelectListItem> GetDays()
        {
            IList<SelectListItem> _result = new List<SelectListItem>();

            _result.Add(new SelectListItem { Value = "-1", Text = "Selecione o Dia", Selected = true });
            for (int i = 1; i <= 31; i++)
            {
                _result.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }

            return _result;
        }

        public static IList<SelectListItem> GetOperation()
        {
            IList<SelectListItem> _result = new List<SelectListItem>();
            
            _result.Add(new SelectListItem { Value = "1", Text = "Crédito, Débito, Estorno" , Selected = true});
            _result.Add(new SelectListItem { Value = "2", Text = "Crédito e Débito", Selected = false });
            _result.Add(new SelectListItem { Value = "3", Text = "Crédito", Selected = false });
            _result.Add(new SelectListItem { Value = "4", Text = "Débito", Selected = false });
            _result.Add(new SelectListItem { Value = "5", Text = "Estorno", Selected = false });
            _result.Add(new SelectListItem { Value = "6", Text = "Outros", Selected = false });
            _result.Add(new SelectListItem { Value = "0", Text = "Tudo", Selected = false });

            return _result;
        }

        public static IList<SelectListItem> GetStatus()
        {
            IList<SelectListItem> _result = new List<SelectListItem>();

            _result.Add(new SelectListItem { Value = "0", Text = "Tudo", Selected = true });
            _result.Add(new SelectListItem { Value = "1", Text = "Autorizada", Selected = false });
            _result.Add(new SelectListItem { Value = "2", Text = "Não Autorizada", Selected = false });
            _result.Add(new SelectListItem { Value = "3", Text = "Cancelada", Selected = false });
            _result.Add(new SelectListItem { Value = "4", Text = "Falha/Erro", Selected = false });
            _result.Add(new SelectListItem { Value = "5", Text = "Pendente", Selected = false });

            return _result;
        }

        public static IList<SelectListItem> GetCardBrand()
        {
            IList<SelectListItem> _result = new List<SelectListItem>();

            _result.Add(new SelectListItem { Value = "0", Text = "Tudo", Selected = true });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.VISA).ToString(), Text = "Visa", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.MASTERCARD).ToString(), Text = "MasterCard", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.DINERS).ToString(), Text = "Diners", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.AMEX).ToString(), Text = "American Express", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.SOLLO).ToString(), Text = "Sollo", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.SIDECARD).ToString(), Text = "SideCard", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.PRIVATE_LABEL).ToString(), Text = "Private Label", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.REDESHOP).ToString(), Text = "Redeshop", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.PAO_DE_ACUCAR).ToString(), Text = "Pão de Açúcar", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.FININVEST).ToString(), Text = "Fininvest", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.JCB).ToString(), Text = "JCB", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.HIPERCARD).ToString(), Text = "Hipercard", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.AURA).ToString(), Text = "Aura", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.LOSANGO).ToString(), Text = "Losango", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.SOROCRED).ToString(), Text = "Sorocred", Selected = false });
            _result.Add(new SelectListItem { Value = ((int)Enums.CardBrand.DISCOVERY).ToString(), Text = "Discovery", Selected = false });

            return _result;
        }
    }
}