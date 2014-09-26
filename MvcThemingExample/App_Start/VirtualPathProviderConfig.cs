using System.Configuration;
using System.Web.Hosting;
using MM.Web.Mvc.Theming.Hosting;

namespace MvcThemingExample
{
    public class VirtualPathProviderConfig
    {
        public static VirtualPathProvider RegisterVirtualPathProviders()
        {
            var pathProvider = HostingEnvironment.VirtualPathProvider;

            // If a theme name is present, load the new VirtualPathProvider
            var themeName = ConfigurationManager.AppSettings["MvcThemeName"];
            var themeFolder = ConfigurationManager.AppSettings["MvcThemeFolder"];
            if(!string.IsNullOrWhiteSpace(themeName))
            {
                if(string.IsNullOrWhiteSpace(themeFolder))
                {
                    themeFolder = null;
                }

                pathProvider = new VirtualThemePathProvider(themeName, themeFolder);
                HostingEnvironment.RegisterVirtualPathProvider(pathProvider);
            }

            return pathProvider;
        }
    }
}