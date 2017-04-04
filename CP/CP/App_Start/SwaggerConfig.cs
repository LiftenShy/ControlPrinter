using System.Web.Http;
using WebActivatorEx;
using CP.Web;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace CP.Web
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c => c.SingleApiVersion("v1", "CP.Web"))
                .EnableSwaggerUi();
        }
    }
}
