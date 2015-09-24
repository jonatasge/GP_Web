using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualPlay.Api.Helper
{
    public static class UserHelper
    {
        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static string GetUserAgent()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string userAgent = context.Request.UserAgent;

            if (!string.IsNullOrEmpty(userAgent))
            {
                return userAgent;
            }

            return context.Request.ServerVariables["HTTP_USER_AGENT"];
        }
    }
}