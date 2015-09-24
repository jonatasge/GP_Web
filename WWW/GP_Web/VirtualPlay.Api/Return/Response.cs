using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace VirtualPlay.Api.Return
{
    public abstract class Response
    {
        public Response()
            : this(HttpContext.Current.Request.Url.Host)
        {
        }

        public Response(string domain)
        {
            this.server = "VirtualPlay";
            this.domain = domain;
        }

        public string server { get; set; }
        public string domain { get; set; }
        public bool @return { get; set; }
    }
}