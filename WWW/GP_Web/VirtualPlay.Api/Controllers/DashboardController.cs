using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using VirtualPlay.Api.Return;
using VirtualPlay.Business.Models;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace VirtualPlay.Api.Controllers
{
    public class DashboardController : Controller
    {
        // POST: /Sale/
        [HttpPost]
        public object[] Sale(int merchant, int year, int month, int day)
        {
            object[] data = null;

            using (var db = new Entities())
            {
                ObjectResult<Pay_TransactionChartSale_Result> listDashboard = db.Pay_TransactionChartSale(merchant, year, month, day);

                List<Pay_TransactionChartSale_Result> listDashboardChart = listDashboard.ToList();

                if (listDashboardChart != null)
                {
                    var chartData = new object[listDashboardChart.Count() + 1];
                    chartData[0] = new object[] 
                    {
                        "Operation",
                        "Value",
                        "Amount",
                        "Currency"
                    };
                    int j = 0;
                    foreach (var i in listDashboardChart)
                    {
                        j++;
                        chartData[j] = new object[] { Business.Description.getOperation((Business.Enums.Operation)i.operation.Value), i.value.Value, i.amount.Value, string.Concat("R$ ", i.value.Value.ToString("0.00")) };
                    }
                    data = chartData;
                }
            }

            return data;
        }

        // POST: /Refund/
        [HttpPost]
        public object[] Refund(int merchant, int year, int month, int day)
        {
            object[] data = null;

            using (var db = new Entities())
            {
                ObjectResult<Pay_TransactionChartRefund_Result> listDashboard = db.Pay_TransactionChartRefund(merchant, year, month, day);

                List<Pay_TransactionChartRefund_Result> listDashboardChart = listDashboard.ToList();

                if (listDashboardChart != null)
                {
                    var chartData = new object[listDashboardChart.Count() + 1];
                    chartData[0] = new object[] 
                    {
                        "Operation",
                        "Value",
                        "Amount",
                        "Currency"
                    };
                    int j = 0;
                    foreach (var i in listDashboardChart)
                    {
                        j++;
                        chartData[j] = new object[] { Business.Description.getCardBrand((Business.Enums.CardBrand)int.Parse(i.cardBrand)), i.value.Value, i.amount.Value, string.Concat("R$ ", i.value.Value.ToString("0.00")) };
                    }
                    data = chartData;
                }
            }

            return data;
        }

        // POST: /Installment/
        [HttpPost]
        public object[] Installment(int merchant, int year, int month, int day)
        {
            object[] data = null;

            using (var db = new Entities())
            {
                ObjectResult<Pay_TransactionChartInstallment_Result> listDashboard = db.Pay_TransactionChartInstallment(merchant, year, month, day);

                List<Pay_TransactionChartInstallment_Result> listDashboardChart = listDashboard.ToList();

                if (listDashboardChart != null)
                {
                    var chartData = new object[listDashboardChart.Count() + 1];
                    chartData[0] = new object[] 
                    {
                        "Installment",
                        "Value",
                        "Amount",
                        "Currency"
                    };
                    int j = 0;
                    foreach (var i in listDashboardChart)
                    {
                        j++;
                        chartData[j] = new object[] { i.installment, i.value, i.amount, string.Concat("R$ ", i.value.Value.ToString("0.00")) };
                    }
                    data = chartData;
                }
            }

            return data;
        }

        // POST: /CardBrand/
        [HttpPost]
        public object[] CardBrand(int merchant, int year, int month, int day)
        {
            object[] data = null;

            using (var db = new Entities())
            {
                ObjectResult<Pay_TransactionChartCardBrand_Result> listDashboard = db.Pay_TransactionChartCardBrand(merchant, year, month, day);

                List<Pay_TransactionChartCardBrand_Result> listDashboardChart = listDashboard.ToList();

                if (listDashboardChart != null)
                {
                    var chartData = new object[listDashboardChart.Count() + 1];
                    chartData[0] = new object[] 
                    {
                        "CardBrand",
                        "Value",
                        "Amount",
                        "Currency"
                    };
                    int j = 0;
                    foreach (var i in listDashboardChart)
                    {
                        j++;
                        chartData[j] = new object[] { Business.Description.getCardBrand((Business.Enums.CardBrand)int.Parse(i.cardBrand)), i.value.Value, i.amount.Value, string.Concat("R$ ", i.value.Value.ToString("0.00")) };
                    }
                    data = chartData;
                }
            }

            return data;
        }

    }
}
