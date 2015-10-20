using System;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;
using VirtualPlay.Api.Return;
using VirtualPlay.Business.Models;

namespace VirtualPlay.Api.Controllers
{
    public class SessionController : Controller
    {
        public static string New(string email)
        {
            //create new instance of md5
            MD5 md5 = MD5.Create();

            string validSession = string.Concat(email, ".", DateTime.Now.ToString("yyyy-MM-dd.HH:mm"));

            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(validSession));

            string resultSession = BitConverter.ToString(hashData).Replace("-", "");

            return resultSession;
        }

        public static void Write(string session, int user, int system)
        {
            using (var db = new Entities())
            {
                Sys_UserSession userSession = db.Sys_UserSession.Find(user, system);

                if (userSession != null)
                {
                    userSession.dsSession = session;
                    userSession.dtLastUpdate = DateTime.Now;
                    db.Entry(userSession).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    userSession = new Sys_UserSession();

                    userSession.idUser = user;
                    userSession.idSystem = system;

                    userSession.dsSession = session;

                    userSession.idUserLastUpdate = user;

                    userSession.dtCreate = DateTime.Now;
                    userSession.dtLastUpdate = DateTime.Now;

                    userSession.flStatus = "A";

                    db.Entry(userSession).State = EntityState.Added;
                    db.SaveChanges();
                }

                Sys_User sysUser = db.Sys_User.Find(user);
                sysUser.dsSession = session;
                db.Entry(sysUser).State = EntityState.Modified;

                db.SaveChanges();
            }
        }
    }
}
