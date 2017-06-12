using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnlineScrum
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //            AreaRegistration.RegisterAllAreas();
            //            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //Database.SetInitializer<DatabaseContext>(null);
            RouteTable.Routes.MapMvcAttributeRoutes();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
