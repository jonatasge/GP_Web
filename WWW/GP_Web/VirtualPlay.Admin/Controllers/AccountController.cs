using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using VirtualPlay.Api.Controllers;
using VirtualPlay.Api.Return;
using Newtonsoft.Json;
using VirtualPlay.Api.Helper;
using System.Web.Security;
using System.Web.Script.Serialization;

using VirtualPlay.Admin.Managers;
using VirtualPlay.Admin.Models;

namespace VirtualPlay.Admin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Logoff(Session, Response);
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                DateTime dateSecret = DateTime.Now;
                string secret = null;

                TokenController tokenControler = new TokenController();
                LoginController loginControler = new LoginController();
                
                secret = tokenControler.getSecret("Gopag", dateSecret, model.Email);

                var token = tokenControler.getAccessToken(secret, model.Email, dateSecret.ToString("yyyyMMddHHmm"));

                if (token.@return)
                {

                    User appUser = new User();
                    appUser.password = model.Password;
                    appUser.system = 1;
                    appUser.ipAddress = UserHelper.GetIPAddress();
                    appUser.userAgent = UserHelper.GetUserAgent();
                    string jsonParams = JsonConvert.SerializeObject(appUser);

                    var login = loginControler.getLogin(((Token)token).accessToken, model.Email, jsonParams);

                    if (login.@return && ((Login)login).id > 0)
                    {
                        appUser.id = ((Login)login).id;
                        appUser.name = ((Login)login).name;
                        appUser.email = ((Login)login).email;
                        appUser.session = ((Login)login).session;
                        appUser.idMerchant = ((Login)login).idMerchant;

                        await SignInAsync(appUser, model.RememberMe, appUser, Response);
                        //return RedirectToAction("", "Sales");
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-mail ou senha inválido.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-mail ou senha inválido.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // GET: /Account/LogOff
        [AllowAnonymous]
        public ActionResult LogOff(string returnUrl)
        {
            Logoff(Session, Response);
            return RedirectToAction("Index", "Home");
        }

        public static void Logoff(HttpSessionStateBase session, HttpResponseBase response)
        {
            // Delete the user details from cache.
            session.Abandon();

            // Delete the authentication ticket and sign out.
            FormsAuthentication.SignOut();

            // Clear authentication cookie.
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            response.Cookies.Add(cookie);
        }

        private async Task SignInAsync(User user, bool isPersistent, User appUser, HttpResponseBase response)
        {
            Logoff(Session, Response);
            FormsAuthentication.SetAuthCookie(user.id.ToString(), isPersistent);

            var serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(appUser);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    appUser.email,
                    DateTime.Now,
                    DateTime.Now.AddDays(1),
                    true,
                    userData,
                    FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("", "Dashboard");
            }
        }
    }
}