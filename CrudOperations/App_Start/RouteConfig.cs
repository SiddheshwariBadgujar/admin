using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace CrudOperations
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ProductDisplay",
                url: "Product/Display/{page}",
                defaults: new { controller = "Product", action = "Display", page = 1 }
            );

            routes.MapRoute(
                name: "Product",
                url: "Product/{action}/{id}",
                defaults: new { controller = "Product", action = "Display", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Category",
                url: "Home/{action}/{id}",
                defaults: new { controller = "Home", action = "Display", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
