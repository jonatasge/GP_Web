using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace VirtualPlay.Api.Infraestructure
{
    public class EmailHelper
    {
        private readonly SmtpClient client;

        public EmailHelper()
        {
            client = new SmtpClient();
        }

        public void Send(string subject, IEnumerable<string> recipients, string message)
        {
            var pkt = new MailMessage
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            foreach (var mail in recipients)
                pkt.To.Add(new MailAddress(mail));

            client.Send(pkt);
            pkt.Dispose();
        }

        public void Send(string subject, string recipient, string message)
        {
            try
            {
                Send(subject, new[] { recipient }, message);
            }
            catch
            { }
        }
    }
}