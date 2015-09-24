using System;
using System.IO;
using System.Linq;
using System.Web;

using VirtualPlay.Business;
using VirtualPlay.Business.Models;

namespace VirtualPlay.Api.Infraestructure
{
    public class Notification
    {
        public Sys_WebmailLog Log { get; set; }
        
        private Entities _db;
        private EmailHelper helper;

        public Notification(Sys_User participant, Entities db)
        {
            helper = new EmailHelper();
            _db = db;

            Log = new Sys_WebmailLog
            {
                Sys_User = participant,
                dtCreatedAt = DateTime.Now,
                flStatus = true,
                flSent = false
            };
        }

        /*
        public void NotifyNotWin(test_drive testDrive, coupon coupon)
        {
            Log.template = "cupom";
            Log.test_drive = testDrive;

            _db.email_log.Add(Log);
            _db.SaveChanges();
        }

        public void NotifyWin(test_drive testDrive, coupon coupon)
        {
            Log.template = "premio-cupom";
            Log.test_drive = testDrive;

            _db.email_log.Add(Log);
            _db.SaveChanges();
        }
        */

        public void NotifyPasswordChange(string Encrypted, string Decrypted)
        {
            Log.cdTemplate = "lembrete";
            Log.dsExtra = Encrypted;

            _db.Sys_WebmailLog.Add(Log);
            _db.SaveChanges();
        }

        public void NotifyRegistered()
        {
            Log.cdTemplate = "comprovacao-cadastro";

            _db.Sys_WebmailLog.Add(Log);
            _db.SaveChanges();
        }
    }
}