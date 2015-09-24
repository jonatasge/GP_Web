using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using VirtualPlay.MyAccount.Managers;

namespace VirtualPlay.MyAccount
{
    public class Application : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    // Get the forms authentication ticket.
                    var identity = new GenericIdentity(authTicket.Name, "Forms");
                    var principal = new MyPrincipal(identity);

                    // Deserialize the json data and set it on the custom principal.
                    var serializer = new JavaScriptSerializer();
                    principal.User = (User)serializer.Deserialize(authTicket.UserData, typeof(User));

                    // Set the context user.
                    Context.User = principal;
                }
            }
        }
    }
}
