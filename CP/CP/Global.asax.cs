using System.Web.Mvc;
using System.Web.Routing;
using CP.Web.App_Start;

namespace CP.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityConfig.Initialize();
        }
    }
}
