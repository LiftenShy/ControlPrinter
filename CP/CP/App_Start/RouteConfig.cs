using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls.WebParts;

namespace CP.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "defult",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "HomePage", id = UrlParameter.Optional },
                new []{ "CP.Web.Controllers" }
            );
        }
    }
}
