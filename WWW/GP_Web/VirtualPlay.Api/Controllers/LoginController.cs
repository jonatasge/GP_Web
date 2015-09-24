using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using VirtualPlay.Api.Infraestructure;
using VirtualPlay.Api.Return;
using VirtualPlay.Business.Models;
using Newtonsoft.Json;
using System.Data.Entity.Core.Objects;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace VirtualPlay.Api.Controllers
{
    public class LoginController : Controller
    {
        //
        // POST: /login/
        [HttpPost]
        public JsonResult getJsonLogin(string accessToken, string email)
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string json = new StreamReader(req).ReadToEnd();

            return Json(getLogin(accessToken, email, json), JsonRequestBehavior.AllowGet);
        }

        public Response getLogin(string accessToken, string email,string jsonParams)
        {
            Response response = null;

            using (var db = new Entities())
            {
                if (!string.IsNullOrEmpty(email) && ValidaEmail(email))
                {
                    if (!string.IsNullOrEmpty(accessToken) && TokenController.IsValidToken(accessToken, email))
                    {
                        dynamic myObj;
                        try
                        {
                            myObj = JsonConvert.DeserializeObject(jsonParams);

                            string password = null;

                            int idSystem = -1;
                            string ipAddress = null;
                            string dsAgent = null;

                            if (myObj.password != null)
                                password = myObj.password; //required

                            if (myObj.system != null)
                                idSystem = myObj.system; //required

                            if (myObj.ipAddress != null)
                                ipAddress = myObj.ipAddress; //required

                            if (myObj.userAgent != null)
                                dsAgent = myObj.userAgent; //required

                            if (!string.IsNullOrEmpty(password))
                            {
                                string newSession = NewSession(email);

                                ObjectResult<Sys_UserLogin_Result> listUser = db.Sys_UserLogin(email, PasswordEncrypt(password), idSystem, ipAddress, dsAgent, accessToken, newSession);

                                List<Sys_UserLogin_Result> listUserLogin = listUser.ToList();

                                if (listUserLogin != null && listUserLogin.Count > 0)
                                {
                                    int idUser = -1;
                                    int idRole = -1;
                                    int idPerson = -1;
                                    int idEnterprise = -1;
                                    int idMerchant = -1;
                                    int idUserCreate = -1;
                                    int idUserUpdate = -1;

                                    DateTime expire_at = DateTime.MinValue;
                                    DateTime create_at = DateTime.MinValue;
                                    DateTime update_at = DateTime.MinValue;

                                    string session = string.Empty;
                                    string nmUser = string.Empty;
                                    string stUser = string.Empty;
                                    string dsEmail = string.Empty;

                                    idUser = (int)listUserLogin[0].idUser;

                                    if (listUserLogin[0].idRole != null)
                                        idRole = (int)listUserLogin[0].idRole;

                                    if (listUserLogin[0].idPerson != null)
                                        idPerson = (int)listUserLogin[0].idPerson;

                                    if (listUserLogin[0].idEnterprise != null)
                                        idEnterprise = (int)listUserLogin[0].idEnterprise;

                                    if (listUserLogin[0].idMerchant != null)
                                        idMerchant = (int)listUserLogin[0].idMerchant;

                                    if (listUserLogin[0].idUserCreate != null)
                                        idUserCreate = (int)listUserLogin[0].idUserCreate;

                                    if (listUserLogin[0].idUserLastUpdate != null)
                                        idUserUpdate = (int)listUserLogin[0].idUserLastUpdate;

                                    nmUser = (string)listUserLogin[0].nmUser;
                                    dsEmail = (string)listUserLogin[0].dsEmail;
                                    stUser = (string)listUserLogin[0].stUser;
                                    session = newSession;

                                    if (listUserLogin[0].dtExpire != null)
                                        expire_at = (DateTime)listUserLogin[0].dtExpire;

                                    if (listUserLogin[0].dtCreate != null)
                                        create_at = (DateTime)listUserLogin[0].dtCreate;

                                    if (listUserLogin[0].dtLastUpdate != null)
                                        update_at = (DateTime)listUserLogin[0].dtLastUpdate;

                                    if (idUser > 0 && expire_at > DateTime.Now)
                                    {
                                        response = new Login(idUser, idRole, idPerson, idEnterprise, idMerchant, idSystem, nmUser, dsEmail, session);
                                    }
                                    else if (idUser == -1)/*Invalid Email*/
                                    {
                                        response = new ResponseFailure("invalid-login");
                                    }
                                    else if (idUser == -2)/*Invalid password*/
                                    {
                                        response = new ResponseFailure("invalid-login");
                                    }
                                    else if (idUser == -3)/*Invalid previleges*/
                                    {
                                        response = new ResponseFailure("invalid-login");
                                    }
                                    else if (idUser == -4)/*Invalid Expire Date*/
                                    {
                                        response = new ResponseFailure("invalid-login");
                                    }
                                    else
                                    {
                                        response = new ResponseFailure("invalid-login");
                                    }
                                }
                                else
                                {
                                    response = new ResponseFailure("invalid-login");
                                }
                            }
                            else
                            {
                                response = new ResponseFailure("invalid-login");
                            }
                        }
                        catch (Exception ex)
                        {
                            response = new ResponseFailure(ex.Message);
                        }
                    }
                    else
                    {
                        response = new ResponseFailure("invalid-token");
                    }
                }
                else
                {
                    response = new ResponseFailure("invalid-email");
                }
            }

            return response;
        }

        // POST: /generateNewPassword/
        [HttpPost]
        public JsonResult generateNewPassword(string accessToken, string email)
        {
            using (var db = new Entities())
            {
                Response response = null;

                if (!string.IsNullOrEmpty(email) && ValidaEmail(email))
                {
                    if (!string.IsNullOrEmpty(accessToken) && TokenController.IsValidToken(accessToken, email))
                    {
                        var participants = db.Sys_User.Where(z => z.dsEmail == email);

                        List<Sys_User> listParticipant = participants.ToList();

                        if (listParticipant != null && listParticipant.Count > 0)
                        {
                            Sys_User participant = db.Sys_User.Find(listParticipant[0].idUser);

                            var passwd = NewPassword();
                            var encrypted = PasswordEncrypt(passwd);
                            participant.dsPassword = encrypted;

                            db.Entry(participant).State = EntityState.Modified;
                            db.SaveChanges();

                            //var notifier = new Notification(participant, db);
                            // //NOTIFICAR {MUDANÇA DE SENHA}
                            //notifier.NotifyPasswordChange(encrypted, passwd);

                            response = new NewPassword(participant.idUser, participant.nmUser, (participant.idRole.HasValue ? participant.idRole.Value : -1), participant.dsEmail);
                        }
                        else
                        {
                            response = new ResponseFailure("invalid-Email");
                        }
                    }
                    else
                    {
                        response = new ResponseFailure("invalid-token");
                    }
                }
                else
                {
                    response = new ResponseFailure("invalid-Email");
                }

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: /checkSession/
        [HttpPost]
        public JsonResult generateNewSession(string session, string email)
        {
            using (var db = new Entities())
            {
                Response response = null;

                if (!string.IsNullOrEmpty(email) && ValidaEmail(email))
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
                                participant.dtLastSession = DateTime.Now;
                                participant.dsSession = NewSession(email);

                                db.Entry(participant).State = EntityState.Modified;
                                db.SaveChanges();

                                response = new Login(participant.idUser, participant.idRole.Value, participant.idPerson.Value, participant.idPerson.Value, participant.idMerchant.Value, 0, participant.nmUser, email, participant.dsSession);
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

        public static string NewSession(string email)
        {
            //create new instance of md5
            MD5 md5 = MD5.Create();

            string validSession = string.Concat(email, ".", DateTime.Now.ToString("yyyy-MM-dd.HH:mm"));

            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(validSession));

            string resultSession = BitConverter.ToString(hashData).Replace("-", "");

            return resultSession;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private string NewPassword()
        {
            int r, k;
            int passwordLength = 10;
            string password = "";
            char[] upperCase = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] lowerCase = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            int[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Random rRandom = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                r = rRandom.Next(3);

                if (r == 0)
                {
                    k = rRandom.Next(0, 25);
                    password += upperCase[k];
                }

                else if (r == 1)
                {
                    k = rRandom.Next(0, 25);
                    password += lowerCase[k];
                }

                else if (r == 2)
                {
                    k = rRandom.Next(0, 9);
                    password += numbers[k];
                }

            }

            return password;
        }

        public static string PasswordEncrypt(string str)
        {
            string EncrptKey = "Gopag;[pnuLIT)VirtualPlay";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string PasswordDecrypt(string str)
        {
            str = str.Replace(" ", "+");
            string DecryptKey = "Gopag;[pnuLIT)VirtualPlay";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] inputByteArray = new byte[str.Length];

            byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(str.Replace(" ", "+"));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }

        public static bool ValidaEmail(string email)
        {
            string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isEmail = false;

            isEmail = Regex.IsMatch(email, strRegex, RegexOptions.IgnoreCase);

            return isEmail;
        }

        public static bool ValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;

            string digito;

            int soma;

            int resto;

            cpf = cpf.Trim();

            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);

            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);

        }
    }
}