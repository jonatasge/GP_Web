using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualPlay.Business.Models;

namespace VirtualPlay.Api.Return
{
    public class Login : Response
    {
        public Login(int id, int idRole, int idPerson, int idEnterprise, int idMerchant, int idSystem, string name, string email, string session)
        {
            this.@return = true;
            this.id = id;
            this.idRole = idRole;
            this.idPerson = idPerson;
            this.idEnterprise = idEnterprise;
            this.idMerchant = idMerchant;
            this.name = name;
            this.email = email;
            this.session = session;

            using (var db = new Entities())
            {
                Sys_System sysSystem = db.Sys_System.Where(c => c.idSystem == idSystem).SingleOrDefault();

                if (sysSystem != null)
                {
                    system = new System(sysSystem);

                    Sys_Merchant sysMerchant = db.Sys_Merchant.Where(c => c.idMerchant == idMerchant).SingleOrDefault();

                    if (sysMerchant != null)
                    {
                        merchant = new Merchant(sysMerchant);

                        posList = new List<POS>();

                        foreach (Sys_MerchantPinPad merchantPinPad in db.Sys_MerchantPinPad.Where(c => c.idMerchant == idMerchant && c.flStatus.Equals("A")).ToList())
                        {
                            posList.Add(new POS(db.Pos_PinPad.Where(c => c.idPinPad == merchantPinPad.idPinpad).SingleOrDefault()));
                        }
                    }
                }
            }
        }

        public int id { get; set; }
        public int idRole { get; set; }
        public int idPerson { get; set; }
        public int idEnterprise { get; set; }
        public int idMerchant { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string session { get; set; }

        public System system { get; set; }
        public Merchant merchant { get; set; }
        public List<POS> posList { get; set; }
    }
}