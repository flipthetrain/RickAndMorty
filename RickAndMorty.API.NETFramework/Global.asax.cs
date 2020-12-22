using System.Web.Http;

namespace RickAndMorty.API.NETFramework
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //we remove this so that we control the HTTP Configuration object and registering is not magic
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            HttpConfiguration httpConfig = GlobalConfiguration.Configuration;

            WebApiConfig.Register(httpConfig);

            SwaggerConfig.Register(httpConfig);

            httpConfig.EnsureInitialized();
        }
    }
}
