using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VarBal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                    name: "Code",
                    url: "{controller}/{action}/{code}",
                    defaults: new { controller = "Home", action = "Index", code = "" }
                    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        routes.MapRoute(
        name: "Booking",
        url: "{controller}/{action}/{id}",
        defaults: new { controller = "Home", action = "Booking", id = UrlParameter.Optional }
    );
        }
    }
}
