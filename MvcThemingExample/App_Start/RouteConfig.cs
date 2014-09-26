using System.Web.Mvc;
using System.Web.Routing;

namespace MvcThemingExample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // Ignore static files that have custom handlers set in Web.config (match to system.webServer > handlers)
            routes.IgnoreRoute("{*staticfile}", new
            {
                staticfile = @".*\.(css|png|js|gif|jpg)(/.*)?"
            });

            routes.MapRoute("Default", "{controller}/{action}/{id}", new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            });
        }
    }
}