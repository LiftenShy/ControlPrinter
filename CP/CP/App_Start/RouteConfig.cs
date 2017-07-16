using System.Web.Mvc;
using System.Web.Routing;

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
                new { controller = "Home", action = "HomePage", id = UrlParameter.Optional }
            );
        }
    }
}
