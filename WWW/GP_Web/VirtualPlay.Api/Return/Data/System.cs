using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualPlay.Api.Return
{
    public class System
    {
        public System(Business.Models.Sys_System system)
        {
            if (system != null)
            {
                this.id = system.idSystem;
                this.type = system.tpSystem;
                this.name = system.nmSystem;
                this.description = system.dsSystem;
                this.code = system.cdSystem;
                this.expire = system.dtExpire.Value.ToString("yyyy-MM-dd HH:mm");
                this.createdAt = system.dtCreate.ToString("yyyy-MM-dd HH:mm");
                this.updatedAt = system.dtLastUpdate.ToString("yyyy-MM-dd HH:mm");
            }
        }

        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string code { get; set; }
        public string expire { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}