using System.Security.Principal;

namespace VirtualPlay.MyAccount.Managers
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User
    {
        public int id { get; set; }
        public int idRole { get; set; }
        public int idPerson { get; set; }
        public int idEnterprise { get; set; }
        public int idMerchant { get; set; }

        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public string session { get; set; }
        public string accessToken { get; set; }
        
        public int system { get; set; }
        
        public string ipAddress { get; set; }
        public string userAgent { get; set; }
    }

    public class MyPrincipal : IPrincipal
    {
        public MyPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public IIdentity Identity
        {
            get;
            private set;
        }

        public User User { get; set; }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}