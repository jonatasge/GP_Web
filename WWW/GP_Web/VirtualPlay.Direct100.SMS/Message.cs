using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;

using Newtonsoft.Json;

namespace VirtualPlay.Direct100.SMS
{
    public class Message
    {
        public string ConnectionStrings { get; set; }

        private string requestUri = "https://direct100.inesting.com/API/#TOKEN#/V2/Sms";
        private string userSender;

        public string Schema { get; set; }
        public int IdSystem { get; set; }
        public int IdUser { get; set; }

        public string Id { get; set; }
        public string Balance { get; set; }

        public string Extra { get; set; }

        public Message(string userSender)
        {
            this.userSender = userSender;
        }

        public Result Send(string authToken, string numberPhone, string message)
        {
            Result sendStatus = Result.INTERNAL_SERVER_ERROR;

            Id = null;
            Balance = null;

            requestUri = requestUri.Replace("#TOKEN#", authToken);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"Username\":\"" + this.userSender + "\"," +
                              "\"Message\":\"" + message + "\"," +
                              "\"Telephones\":\"" + numberPhone + "\"," +
                              "\"DateToSend\":\"" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "\"," +
                              "\"Sender\":\"1040\"," +
                              "\"Type\":\"SMS\"}";

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
                        if (resultJson.Id != null)
                        {
                            Id = resultJson.Id;
                            Balance = resultJson.Balance;
                            sendStatus = Result.SUCCESS;
                        }
                        else if (resultJson.Error != null)
                        {
                            if (resultJson.Error.Equals("ERROR00"))
                                sendStatus = Result.INTERNAL_SERVER_ERROR;
                            else if (resultJson.Error.Equals("ERROR01"))
                                sendStatus = Result.NO_VALID_CONTACT;
                            else if (resultJson.Error.Equals("ERROR02"))
                                sendStatus = Result.NOT_ENOUGH_CREDIT;
                            else if (resultJson.Error.Equals("ERROR03"))
                                sendStatus = Result.INVALID_TOKEN;
                            else if (resultJson.Error.Equals("ERROR04"))
                                sendStatus = Result.MESSAGE_REQUIRED;
                            else if (resultJson.Error.Equals("ERROR05"))
                                sendStatus = Result.INVALID_CREDENTIALS;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (resultJson != null && resultJson.Error != null)
                        {
                            if (resultJson.Error.Equals("ERROR00"))
                                sendStatus = Result.INTERNAL_SERVER_ERROR;
                            else if (resultJson.Error.Equals("ERROR01"))
                                sendStatus = Result.NO_VALID_CONTACT;
                            else if (resultJson.Error.Equals("ERROR02"))
                                sendStatus = Result.NOT_ENOUGH_CREDIT;
                            else if (resultJson.Error.Equals("ERROR03"))
                                sendStatus = Result.INVALID_TOKEN;
                            else if (resultJson.Error.Equals("ERROR04"))
                                sendStatus = Result.MESSAGE_REQUIRED;
                            else if (resultJson.Error.Equals("ERROR05"))
                                sendStatus = Result.INVALID_CREDENTIALS;
                        }
                    }
                }
            }

            LogInsert(sendStatus, numberPhone, message);
           
            return sendStatus;
        }

        private void LogInsert(Result sendStatus, string numberPhone, string message)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            connection = new System.Data.SqlClient.SqlConnection(ConnectionStrings);

            command = new System.Data.SqlClient.SqlCommand(Schema + ".Sys_SmsLogInsert", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("@nbSmsTo", System.Data.SqlDbType.VarChar, 13).Value = numberPhone;
            command.Parameters.Add("@dsError", System.Data.SqlDbType.VarChar, -1).Value = sendStatus != Result.SUCCESS ? GetMessageError(sendStatus) : null;
            command.Parameters.Add("@dsMessage", System.Data.SqlDbType.VarChar, -1).Value = message;
            command.Parameters.Add("@dsExtra", System.Data.SqlDbType.VarChar, -1).Value = this.Extra;
            command.Parameters.Add("@flSent", System.Data.SqlDbType.Bit).Value = (sendStatus == Result.SUCCESS);
            command.Parameters.Add("@idSystem", System.Data.SqlDbType.Int).Value = IdSystem;
            command.Parameters.Add("@idUserCreate", System.Data.SqlDbType.Int).Value = IdUser;

            connection.Open();
            command.ExecuteScalar();
        }

        private string GetMessageError(Result sendStatus)
        {
            string messageError = null;

            switch (sendStatus)
            {
                case Result.INTERNAL_SERVER_ERROR:
                    messageError = "Internal Server Error";
                    break;
                case Result.NO_VALID_CONTACT:
                    messageError = "No Valid Contact";
                    break;
                case Result.NOT_ENOUGH_CREDIT:
                    messageError = "Not enough credit";
                    break;
                case Result.INVALID_TOKEN:
                    messageError = "Invalid Token";
                    break;
                case Result.MESSAGE_REQUIRED:
                    messageError = "Message Required";
                    break;
                case Result.INVALID_CREDENTIALS:
                    messageError = "Invalid Credentials";
                    break;
            }

            return messageError;
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
            public string Id { get; set; }
            public string Balance { get; set; }
            public string Error { get; set; }
            public string Description { get; set; }
        }
    }
}
