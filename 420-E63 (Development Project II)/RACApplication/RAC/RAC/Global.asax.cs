using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RAC.BusinessLogic;

namespace RAC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            /*
             * Adds new global filters.
             *
             * See also:
             * PageHistoryAttribute.cs
             * HomeController.PreviousPage()
             *
             * Paramaters:
             * GlobalFilterCollection - The list of current global filters
             */

            filters.Add(new PageHistoryAttribute());
        }
    }
}
