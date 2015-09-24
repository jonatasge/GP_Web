using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualPlay.Api.Return
{
    public class ResponseFailure : Response
    {
        public string message { get; set; }

        public ResponseFailure(string message)
        {
            this.@return = false;
            this.message = message;
        }
    }
}