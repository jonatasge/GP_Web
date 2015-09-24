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
        // POST: /Operation/
        [HttpPost]
        public JsonResult Operation(string session, string email, int merchant, int year, int month, int day)
        {
            using (var db = new Entities())
            {
                Response response = null;

                if (!string.IsNullOrEmpty(email))
                {
                    if (!string.IsNullOrEmpty(session))
                    {
                        var participants = db.Sys_User.Where(z => z.dsEmail == email);

                        List<Sys_User> listParticipant = participants.ToList();

                        if (listParticipant != null && listParticipant.Count > 0)
                        {
                            int active_session = 0;

                            Sys_User participant = db.Sys_User.Find(listParticipant[0].idUser);

                            if (participant.dtLastSession != null)
                            {
                                DateTime dateNow = DateTime.Now;

                                TimeSpan timeSpan = dateNow.Subtract((DateTime)participant.dtLastSession);
                                active_session = timeSpan.Minutes;
                            }

                            if (active_session <= 60 && participant.dsSession.Equals(session))
                            {
                                Stream req = Request.InputStream;
                                req.Seek(0, System.IO.SeekOrigin.Begin);
                                string json = new StreamReader(req).ReadToEnd();

                                try
                                {
                                    participant.dtLastSession = DateTime.Now;
                                    participant.dsSession = LoginController.NewSession(participant.dsEmail);

                                    db.Entry(participant).State = EntityState.Modified;
                                    db.SaveChanges();

                                    ObjectResult<Pay_TransactionChartOperation_Result> listDashboard = db.Pay_TransactionChartOperation(merchant, year, month, day);

                                    List<Pay_TransactionChartOperation_Result> listDashboardChart = listDashboard.ToList();

                                    response = new ListOperation(participant.dsSession, listDashboardChart);
                                }
                                catch (Exception ex)
                                {
                                    response = new ResponseFailure("invalid-data");
                                }
                            }
                            else
                            {
                                response = new ResponseFailure("invalid-session");
                            }
                        }
                        else
                        {
                            response = new ResponseFailure("invalid-email");
                        }
                    }
                    else
                    {
                        response = new ResponseFailure("invalid-session");
                    }
                }
                else
                {
                    response = new ResponseFailure("invalid-email");
                }

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: /Installment/
        [HttpPost]
        public JsonResult Installment(string session, string email, int merchant, int year, int month, int day)
        {
            using (var db = new Entities())
            {
                Response response = null;

                if (!string.IsNullOrEmpty(email))
                {
                    if (!string.IsNullOrEmpty(session))
                    {
                        var participants = db.Sys_User.Where(z => z.dsEmail == email);

                        List<Sys_User> listParticipant = participants.ToList();

                        if (listParticipant != null && listParticipant.Count > 0)
                        {
                            int active_session = 0;

                            Sys_User participant = db.Sys_User.Find(listParticipant[0].idUser);

                            if (participant.dtLastSession != null)
                            {
                                DateTime dateNow = DateTime.Now;

                                TimeSpan timeSpan = dateNow.Subtract((DateTime)participant.dtLastSession);
                                active_session = timeSpan.Minutes;
                            }

                            if (active_session <= 60 && participant.dsSession.Equals(session))
                            {
                                Stream req = Request.InputStream;
                                req.Seek(0, System.IO.SeekOrigin.Begin);
                                string json = new StreamReader(req).ReadToEnd();

                                try
                                {
                                    participant.dtLastSession = DateTime.Now;
                                    participant.dsSession = LoginController.NewSession(participant.dsEmail);

                                    db.Entry(participant).State = EntityState.Modified;
                                    db.SaveChanges();

                                    ObjectResult<Pay_TransactionChartInstallment_Result> listDashboard = db.Pay_TransactionChartInstallment(merchant, year, month, day);

                                    List<Pay_TransactionChartInstallment_Result> listDashboardChart = listDashboard.ToList();

                                    response = new ListInstallment(participant.dsSession, listDashboardChart);
                                }
                                catch (Exception ex)
                                {
                                    response = new ResponseFailure("invalid-data");
                                }
                            }
                            else
                            {
                                response = new ResponseFailure("invalid-session");
                            }
                        }
                        else
                        {
                            response = new ResponseFailure("invalid-email");
                        }
                    }
                    else
                    {
                        response = new ResponseFailure("invalid-session");
                    }
                }
                else
                {
                    response = new ResponseFailure("invalid-email");
                }

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: /CardBrand/
        [HttpPost]
        public JsonResult CardBrand(string session, string email, int merchant, int year, int month, int day)
        {
            using (var db = new Entities())
            {
                Response response = null;

                if (!string.IsNullOrEmpty(email))
                {
                    if (!string.IsNullOrEmpty(session))
                    {
                        var participants = db.Sys_User.Where(z => z.dsEmail == email);

                        List<Sys_User> listParticipant = participants.ToList();

                        if (listParticipant != null && listParticipant.Count > 0)
                        {
                            int active_session = 0;

                            Sys_User participant = db.Sys_User.Find(listParticipant[0].idUser);

                            if (participant.dtLastSession != null)
                            {
                                DateTime dateNow = DateTime.Now;

                                TimeSpan timeSpan = dateNow.Subtract((DateTime)participant.dtLastSession);
                                active_session = timeSpan.Minutes;
                            }

                            if (active_session <= 60 && participant.dsSession.Equals(session))
                            {
                                Stream req = Request.InputStream;
                                req.Seek(0, System.IO.SeekOrigin.Begin);
                                string json = new StreamReader(req).ReadToEnd();

                                try
                                {
                                    participant.dtLastSession = DateTime.Now;
                                    participant.dsSession = LoginController.NewSession(participant.dsEmail);

                                    db.Entry(participant).State = EntityState.Modified;
                                    db.SaveChanges();

                                    ObjectResult<Pay_TransactionChartCardBrand_Result> listDashboard = db.Pay_TransactionChartCardBrand(merchant, year, month, day);

                                    List<Pay_TransactionChartCardBrand_Result> listDashboardChart = listDashboard.ToList();

                                    response = new ListCardBrand(participant.dsSession, listDashboardChart);
                                }
                                catch (Exception ex)
                                {
                                    response = new ResponseFailure("invalid-data");
                                }
                            }
                            else
                            {
                                response = new ResponseFailure("invalid-session");
                            }
                        }
                        else
                        {
                            response = new ResponseFailure("invalid-email");
                        }
                    }
                    else
                    {
                        response = new ResponseFailure("invalid-session");
                    }
                }
                else
                {
                    response = new ResponseFailure("invalid-email");
                }

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
