using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcThemingExample
{
    public class MvcApplication : HttpApplication
    {

        protected void Application_Start()
        {
            var virtualPathProvider = VirtualPathProviderConfig.RegisterVirtualPathProviders();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleTable.VirtualPathProvider = virtualPathProvider;
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}