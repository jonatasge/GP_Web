using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualPlay.Api.Return
{
    public class POS
    {
        public POS(Business.Models.Pos_PinPad pos)
        {
            if (pos != null)
            {
                this.id = pos.idPinPad;
                this.softDescriptor = pos.dsSoftDescriptor;
                this.sitefNumber = pos.dsSitefNumber;
                this.pinpadInfo = pos.dsPinpadInfo;
                this.serverAddress = pos.dsServerAddress;
                this.serverPort = pos.dsServerPort;
                this.serial = pos.cdSerial;
                this.proxyMode = pos.flProxyMode;
                this.status = pos.flStatus.Equals("A");
                this.createdAt = pos.dtCreate.ToString("yyyy-MM-dd HH:mm");
                this.updatedAt = pos.dtLastUpdate.ToString("yyyy-MM-dd HH:mm");
            }
        }

        public int id { get; set; }
        public string softDescriptor { get; set; }
        public string sitefNumber { get; set; }
        public string pinpadInfo { get; set; }
        public string serverAddress { get; set; }
        public string serverPort { get; set; }
        public string serial { get; set; }
        public string proxyMode { get; set; }
        public bool status { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}