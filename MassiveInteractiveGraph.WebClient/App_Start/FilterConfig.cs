using System.Web;
using System.Web.Mvc;
using MassiveInteractiveGraph.WebClient.ActionFilters;

namespace MassiveInteractiveGraph.WebClient
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //just for easier development
            filters.Add(new NoCacheAttribute());
            filters.Add(new LogActionFilterAttribute());
        }
    }
}