# MM.Web.Mvc.Theming

Provides support for a cascading, hierarchy-driven, theming support in ASP.NET MVC 5

## Who is this for?

If you're developing an MVC solution that will require multiple deployment, but will need to have a different UI for those deployments, this is a potential solution for you.

## How does it work?

Using ASP.NET's [VirtualPathProvider](http://msdn.microsoft.com/en-us/library/system.web.hosting.virtualpathprovider(v=vs.110).aspx), we override requested views, stylesheets, JavaScript, and images _if_ they exist in the theme's folder. If they don't exist, we default back to the top-level's file. This allows for building a cascading theme, where you only override the pieces that change.
