using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualPlay.Api.Return
{
    public class Merchant
    {
        public Merchant()
        {
        }

        public Merchant(Business.Models.Sys_Merchant merchant)
        {
            if (merchant != null)
            {
                this.id = merchant.idMerchant;
                this.email = merchant.dsEmail;
                this.softDescriptor = merchant.dsSoftDescriptor;
                this.sitefNumber = merchant.dsSitefNumber;
                this.pinpadInfo = merchant.dsPinpadInfo;
                this.admin = merchant.flAdmin.Equals("Y");
                this.debug = merchant.flDebug.Equals("Y");
                this.status = merchant.flStatus.Equals("A");
                this.createdAt = merchant.dtCreate.ToString("yyyy-MM-dd HH:mm");
                this.updatedAt = merchant.dtLastUpdate.ToString("yyyy-MM-dd HH:mm");
            }
        }

        public int id { get; set; }
        public string email { get; set; }
        public string softDescriptor { get; set; }
        public string sitefNumber { get; set; }
        public string pinpadInfo { get; set; }
        public bool admin { get; set; }
        public bool debug { get; set; }
        public bool status { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}