using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace IG_WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
           
        }
    }
}
