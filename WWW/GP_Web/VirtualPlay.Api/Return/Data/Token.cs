using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualPlay.Api.Return
{
    public class Token : Response
    {
        public Token(string accessToken)
        {
            this.@return = true;
            this.accessToken = accessToken;
        }

        public string accessToken { get; set; }
    }
}