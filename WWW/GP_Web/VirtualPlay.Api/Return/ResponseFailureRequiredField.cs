using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualPlay.Api.Return
{
    public class ResponseFailureRequiredField : Response
    {
        public string message { get; set; }
        public List<string> required_fields { get; set; }

        public ResponseFailureRequiredField(string message, List<string> required_fields)
        {
            this.@return = false;
            this.message = message;
            this.required_fields = required_fields;
        }
    }
}