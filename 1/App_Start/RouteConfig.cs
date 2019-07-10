using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Home",
            //    url: "about",
            //    defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            //    );

            //routes.MapRoute(
            //    name: "Test",
            //    url: "Home/test/{id}",
            //    defaults: new { controller = "Home", action = "Test", id = UrlParameter.Optional }
            //    );
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
