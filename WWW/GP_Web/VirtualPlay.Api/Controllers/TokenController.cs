using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;

using VirtualPlay.Api.Return;

namespace VirtualPlay.Api.Controllers
{
    public class TokenController : Controller
    {
        //
        // POST: /getAcessToken/
        [HttpPost]
        public JsonResult getJsonAccessToken(string secret, string email, string date)
        {
            return Json(getAccessToken(secret, email, date), JsonRequestBehavior.AllowGet);
        }

        public Response getAccessToken(string secret, string email, string date)
        {
            Response response = null;
            CompilationSection compilationSection = (CompilationSection)System.Configuration.ConfigurationManager.GetSection(@"system.web/compilation");

            if (!string.IsNullOrEmpty(secret))
            {
                if (!string.IsNullOrEmpty(date))
                {
                    if (date.Trim().Length == 12)
                    {
                        try
                        {
                            int year = Int32.Parse(date.Substring(0, 4));
                            int month = Int32.Parse(date.Substring(4, 2));
                            int day = Int32.Parse(date.Substring(6, 2));
                            int hour = Int32.Parse(date.Substring(8, 2));
                            int minute = Int32.Parse(date.Substring(10, 2));

                            var clientDate = new DateTime(year, month, day, hour, minute, 0);
                            var enc = new UTF8Encoding();
                            var validSecret = "Gopag.";

                            validSecret = string.Concat(validSecret, clientDate.ToString("yyyy-MM-dd.HH:mm."), email);

                            var data = enc.GetBytes(validSecret);
                            byte[] result;

                            var sha = new SHA1CryptoServiceProvider();

                            // This is one implementation of the abstract class SHA1.
                            result = sha.ComputeHash(data);

                            //create new instance of StringBuilder to save hashed data
                            string secretValue = BitConverter.ToString(result).Replace("-", "");

                            if (string.Compare(secretValue, secret.ToUpper()) == 0)
                            {
                                string secretToken = string.Concat("VirtualPlay.", DateTime.Now.ToString("yyyy-MM-dd.HH:mm."), email);

                                System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
                                byte[] bs = System.Text.Encoding.UTF8.GetBytes(secretToken);
                                bs = x.ComputeHash(bs);
                                System.Text.StringBuilder s = new System.Text.StringBuilder();
                                foreach (byte b in bs)
                                {
                                    s.Append(b.ToString("x2"));
                                }

                                response = new Return.Token(s.ToString());
                            }
                            else
                            {
                                response = new ResponseFailure("invalid-secret");
                            }

                        }
                        catch (Exception)
                        {
                            response = new ResponseFailure("invalid-date");
                        }
                    }
                    else
                    {
                        response = new ResponseFailure("invalid-date");
                    }
                }
                else
                {
                    response = new ResponseFailure("invalid-date");
                }
            }
            else
            {
                response = new ResponseFailure("invalid-secret");
            }

            return response;
        }

        public string getSecret(string keySecret, DateTime clientDate, string email)
        {
            string secretValue = null;
            var enc = new UTF8Encoding();

            keySecret = string.Concat(keySecret, clientDate.ToString(".yyyy-MM-dd.HH:mm."), email);

            var data = enc.GetBytes(keySecret);
            byte[] result;

            var sha = new SHA1CryptoServiceProvider();

            // This is one implementation of the abstract class SHA1.
            result = sha.ComputeHash(data);

            //create new instance of StringBuilder to save hashed data
            secretValue = BitConverter.ToString(result).Replace("-", "");

            return secretValue;
        }

        public static bool IsValidToken(string accessToken, string email)
        {
            bool isValid = false;


            string validAccessToken = "";

            //create new instance of md5
            MD5 md5 = MD5.Create();

            for (int i = -5; i <= 5; i++)
            {
                validAccessToken = string.Concat("VirtualPlay.", DateTime.Now.AddMinutes(i).ToString("yyyy-MM-dd.HH:mm."), email);

                byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(validAccessToken));

                string secretToken = BitConverter.ToString(hashData).Replace("-", "");

                if (string.Compare(secretToken, accessToken.ToUpper()) == 0)
                {
                    isValid = true;
                    break;
                }
            }

            //isValid = true;
            return isValid;
        }
    }
}
