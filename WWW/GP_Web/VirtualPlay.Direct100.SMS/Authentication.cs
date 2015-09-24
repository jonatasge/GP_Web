using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;

using Newtonsoft.Json;

namespace VirtualPlay.Direct100.SMS
{
    public class Authentication
    {
        public string User { get; set; }
        public string Password { get; set; }

        public decimal Balance { get; set; }

        private Result result;

        private string authToken;

        public Authentication()
        {
        }

        public Authentication(string user, string password)
        {
            this.User = user;
            this.Password = password;
        }

        public string Token { get { return this.authToken; } }
        public Result Status { get { return this.result; } }

        public bool IsAuthenticate()
        {
            bool authenticated = false;
            this.authToken = null;
            this.result = Result.INTERNAL_SERVER_ERROR;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://direct100.inesting.com/API/V2/Auth");
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                string json = "{\"Username\":\"" + this.User + "\"," +
                              "\"Password\":\"" + this.Password + "\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    ResultJson resultJson;
                    resultJson = JsonConvert.DeserializeObject<ResultJson>(result);

                    try
                    {
                        if (resultJson.Token != null)
                        {
                            authenticated = true;
                            this.authToken = resultJson.Token;
                            this.result = Result.SUCCESS;
                        }
                        else if (resultJson.Error != null)
                        {
                            authenticated = false;
                            if (resultJson.Error.Equals("ERROR00"))
                                this.result = Result.INTERNAL_SERVER_ERROR;
                            else if (resultJson.Error.Equals("ERROR01"))
                                this.result = Result.NO_VALID_CONTACT;
                            else if (resultJson.Error.Equals("ERROR02"))
                                this.result = Result.NOT_ENOUGH_CREDIT;
                            else if (resultJson.Error.Equals("ERROR03"))
                                this.result = Result.INVALID_TOKEN;
                            else if (resultJson.Error.Equals("ERROR04"))
                                this.result = Result.MESSAGE_REQUIRED;
                            else if (resultJson.Error.Equals("ERROR05"))
                                this.result = Result.INVALID_CREDENTIALS;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (resultJson != null && resultJson.Error != null)
                        {
                            authenticated = false;
                            if (resultJson.Error.Equals("ERROR00"))
                                this.result = Result.INTERNAL_SERVER_ERROR;
                            else if (resultJson.Error.Equals("ERROR01"))
                                this.result = Result.NO_VALID_CONTACT;
                            else if (resultJson.Error.Equals("ERROR02"))
                                this.result = Result.NOT_ENOUGH_CREDIT;
                            else if (resultJson.Error.Equals("ERROR03"))
                                this.result = Result.INVALID_TOKEN;
                            else if (resultJson.Error.Equals("ERROR04"))
                                this.result = Result.MESSAGE_REQUIRED;
                            else if (resultJson.Error.Equals("ERROR05"))
                                this.result = Result.INVALID_CREDENTIALS;
                        }
                    }
                }
            }

            return authenticated;
        }

        public Result BalanceCalculate()
        {
            decimal resultBalance = 0;
            this.result = Result.INTERNAL_SERVER_ERROR;

            string requestUri = string.Format("https://direct100.inesting.com/API/{0}/V2/Balance", Token);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                ResultBalanceJson resultJson;
                resultJson = JsonConvert.DeserializeObject<ResultBalanceJson>(result);

                try
                {
                    if (resultJson.Balance != null)
                    {
                        string strBalance = resultJson.Balance;

                        if (!string.IsNullOrEmpty(strBalance))
                        {
                            strBalance = strBalance.Substring(0, strBalance.IndexOf(' '));

                            this.Balance = Convert.ToDecimal(strBalance);
                        }

                        this.result = Result.SUCCESS;
                    }
                    else if (resultJson.Error != null)
                    {
                        if (resultJson.Error.Equals("ERROR00"))
                            this.result = Result.INTERNAL_SERVER_ERROR;
                        else if (resultJson.Error.Equals("ERROR01"))
                            this.result = Result.NO_VALID_CONTACT;
                        else if (resultJson.Error.Equals("ERROR02"))
                            this.result = Result.NOT_ENOUGH_CREDIT;
                        else if (resultJson.Error.Equals("ERROR03"))
                            this.result = Result.INVALID_TOKEN;
                        else if (resultJson.Error.Equals("ERROR04"))
                            this.result = Result.MESSAGE_REQUIRED;
                        else if (resultJson.Error.Equals("ERROR05"))
                            this.result = Result.INVALID_CREDENTIALS;
                        else
                            this.result = Result.INTERNAL_SERVER_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    if (resultJson != null && resultJson.Error != null)
                    {
                        if (resultJson.Error.Equals("ERROR00"))
                            this.result = Result.INTERNAL_SERVER_ERROR;
                        else if (resultJson.Error.Equals("ERROR01"))
                            this.result = Result.NO_VALID_CONTACT;
                        else if (resultJson.Error.Equals("ERROR02"))
                            this.result = Result.NOT_ENOUGH_CREDIT;
                        else if (resultJson.Error.Equals("ERROR03"))
                            this.result = Result.INVALID_TOKEN;
                        else if (resultJson.Error.Equals("ERROR04"))
                            this.result = Result.MESSAGE_REQUIRED;
                        else if (resultJson.Error.Equals("ERROR05"))
                            this.result = Result.INVALID_CREDENTIALS;
                        else
                            this.result = Result.INTERNAL_SERVER_ERROR;
                    }
                }
            }

            return this.result;
        }

        public enum Result
        {
            INTERNAL_SERVER_ERROR = 0,
            NO_VALID_CONTACT = 1,
            NOT_ENOUGH_CREDIT = 2,
            INVALID_TOKEN = 3,
            MESSAGE_REQUIRED = 4,
            INVALID_CREDENTIALS = 5,
            SUCCESS = 10
        }

        public class ResultJson
        {
            public string Token { get; set; }
            public string Expires { get; set; }
            public string Error { get; set; }
            public string Description { get; set; }
        }

        public class ResultBalanceJson
        {
            public string Balance { get; set; }
            public string Error { get; set; }
        }
    }
}
