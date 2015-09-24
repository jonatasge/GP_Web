using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualPlay.Api.Return
{
    public class NewPassword : Response
    {
        public NewPassword(int id, string name, int idRole, string email)
        {
            this.@return = true;
            this.id = id;
            this.idRole = idRole;
            this.name = name;
            this.email = email;
            //this.accessToken = accessToken;
        }

        public int id { get; set; }
        public int idRole { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string accessToken { get; set; }
    }
}