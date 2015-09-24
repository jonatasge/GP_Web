using System.Web;
using System.Web.Mvc;

namespace VirtualPlay.MyAccount
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
