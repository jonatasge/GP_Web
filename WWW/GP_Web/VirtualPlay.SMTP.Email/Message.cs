using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace VirtualPlay.SMTP.Email
{
    public class Message
    {
        public string ConnectionStrings { get; set; }
        public string Schema { get; set; }
        public int IdWebmail { get; set; }
        private int idSequence;

        public int IdSystem { get; set; }
        public int IdUserCreate { get; set; }

        public string cdTemplate { get; set; }
        public string cdIdentification1 { get; set; }
        public string cdIdentification2 { get; set; }
        public string cdIdentification3 { get; set; }

        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool IsBodyHtml { get; set; }

        public string Subject { get; set; }
        public string MailFrom { get; set; }
        public string MailFromName { get; set; }
        public string MailTo { get; set; }
        public string Body { get; set; }

        public string Extra { get; set; }
        public string[] Attachments { get; set; }

        private SmtpClient client;
        private MailAddress sendMailFrom;

        public Result SendResult { get; set; }
        public string SendResultMessage { get; set; }

        public Message()
        {
            this.IdWebmail = -1;
        }

        public Message(int idWebmail)
        {
            this.IdWebmail = idWebmail;
        }

        public Message(string host, int port, string userName, string password)
        {
            this.IdWebmail = -1;

            this.Host = host;
            this.Port = port;
            this.UserName = userName;
            this.Password = password;
        }

        public Result Send(string mailTo, string body)
        {
            return Send(this.MailFrom, mailTo, body);
        }

        public Result Send(string mailFrom, string mailTo, string body)
        {
            return Send(this.Subject, mailFrom, mailTo, body);
        }

        public Result Send(string subject, string mailFrom, string mailTo, string body)
        {
            return Send(subject, mailFrom, this.MailFromName, mailTo, body);
        }

        public Result Send(string subject, string mailFrom, string mailFromName, string mailTo, string body)
        {
            this.Subject = subject;
            this.MailFrom = mailFrom;
            this.MailFromName = mailFromName;
            this.MailTo = mailTo;
            this.Body = body;

            return Send();
        }

        public Result Send()
        {
            if (this.IdWebmail != -1)
            {
                LoadWebmail();
            }

            MailMessage msg = new MailMessage();
            msg.From = getMailFrom();
            msg.Subject = this.Subject;
            msg.To.Add(new MailAddress(this.MailTo));
            msg.Body = this.Body;
            msg.IsBodyHtml = this.IsBodyHtml;

            if (Attachments != null)
            {
                foreach (var attachment in Attachments)
                {
                    msg.Attachments.Add(new Attachment(attachment));
                }
            }

            try
            {
                getClient().Send(msg);
                this.SendResult = Result.OK;
                this.SendResultMessage = "Success";
                WebmailUpdate();
            }
            catch (Exception ex)
            {
                this.SendResult = Result.ERROR;
                this.SendResultMessage = ex.Message;
            }
            finally
            {
                Log.ConnectionStrings = ConnectionStrings;
                Log.Schema = Schema;
                Log.Insert(this.IdWebmail
                         , this.idSequence
                         , this.MailTo
                         , this.SendResult != Result.OK ? this.SendResultMessage : null
                         , this.Subject
                         , this.Body
                         , (this.SendResult == Result.OK)
                         , this.Extra
                         , this.IdSystem
                         , this.IdUserCreate
                         , this.cdTemplate
                         , this.cdIdentification1
                         , this.cdIdentification2
                         , this.cdIdentification3);
            }

            msg.Dispose();

            return this.SendResult;
        }

        private void LoadWebmail()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;
            System.Data.SqlClient.SqlDataReader reader = null;

            connection = new System.Data.SqlClient.SqlConnection(ConnectionStrings);

            command = new System.Data.SqlClient.SqlCommand(Schema + ".Sys_WebmailConfSelect", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("@idWebmail", System.Data.SqlDbType.Int).Value = this.IdWebmail;

            connection.Open();
            reader = command.ExecuteReader();

            if (reader != null)
            {
                if (reader.HasRows)
                {
                    reader.Read();

                    this.idSequence = reader.GetInt32(1);//idSequence
                    this.MailFrom = reader.GetString(2);//dsFrom
                    this.MailFromName = reader.GetString(3);//dsFromName
                    this.Host = reader.GetString(4);//dsHost
                    this.Port = reader.GetInt32(5);//nbPort
                    this.UserName = reader.GetString(6);//cdUserName
                    this.Password = reader.GetString(7);//cdPassword
                    this.EnableSsl = reader.GetBoolean(8);//flEnableSsl
                    this.UseDefaultCredentials = reader.GetBoolean(9);//flUseDefaultCredentials
                }
            }
        }

        private void WebmailUpdate()
        {
            System.Data.SqlClient.SqlConnection connection = null;
            System.Data.SqlClient.SqlCommand command = null;

            connection = new System.Data.SqlClient.SqlConnection(ConnectionStrings);

            command = new System.Data.SqlClient.SqlCommand(Schema + ".Sys_WebmailConfUpdate", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("@idWebmail", System.Data.SqlDbType.Int).Value = this.IdWebmail;
            command.Parameters.Add("@idSequence", System.Data.SqlDbType.Int).Value = this.idSequence;

            connection.Open();
            command.ExecuteScalar();
        }

        private SmtpClient getClient()
        {
            if (client == null)
            {
                client = new SmtpClient
                {
                    Host = this.Host,
                    Port = this.Port,
                    EnableSsl = this.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = this.UseDefaultCredentials,
                    Credentials = new NetworkCredential(this.UserName, this.Password)
                };
            }

            return client;
        }

        private MailAddress getMailFrom()
        {
            if (this.MailFrom != null)
            {
                if (sendMailFrom == null || sendMailFrom.Address.Equals(this.MailFrom))
                {
                    if (this.MailFromName != null)
                    {
                        sendMailFrom = new MailAddress(this.MailFrom, this.MailFromName);
                    }
                    else
                    {
                        sendMailFrom = new MailAddress(this.MailFrom);
                    }
                }
            }

            return sendMailFrom;
        }

        public enum Result
        {
            ERROR = 500,
            OK = 200
        }
    }
}
