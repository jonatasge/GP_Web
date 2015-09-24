using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Mail;

namespace VirtualPlay.SMTP.Email
{
    public class Smtp
    {
        private SmtpClient client;

        public SmtpClient getClient()
        {
            //if (client == null)
            //{
            //    client = new SmtpClient
            //    {
            //        Host = this.Host,
            //        Port = this.Port,
            //        EnableSsl = this.EnableSsl,
            //        DeliveryMethod = SmtpDeliveryMethod.Network,
            //        UseDefaultCredentials = this.UseDefaultCredentials,
            //        Credentials = new NetworkCredential(this.UserName, this.Password)
            //    };
            //}

            return client;
        }
    }

    public enum SmtpConfig
    {
        VIRTUALPLAY_ADMIN = 0,
        VIRTUALPLAY_GOPAG_ADMIN = 1,
        VIRTUALPLAY_GOPAG_MY_ACCOUNT = 2,
        VIRTUALPLAY_GOPAG_APP_ANDROID = 3
    }
}
