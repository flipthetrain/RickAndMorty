using Microsoft.Web.Http;
using Microsoft.Web.Http.Routing;
using Microsoft.Web.Http.Versioning;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;

namespace RickAndMorty.API.NETFramework
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration httpConfig)
        {
            // Web API configuration and services
            httpConfig.AddApiVersioning(opts =>
            {
                //this announces valid versions in a response header named api-supported-versions
                //and deprecated verions in a response header named api-deprecated-versions
                opts.ReportApiVersions = true;

                //when an api version is not explicitly listed a default version is assumed
                opts.AssumeDefaultVersionWhenUnspecified = true;
                //this is the default api version
                opts.DefaultApiVersion = new ApiVersion(1, 2);

                //defines where the api version will be present in the request
                opts.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader()                        //uses the parameter of type ApiVersion in the route
                //new QueryStringApiVersionReader("api-version"),       //uses a query string parameter named api-version
                //new HeaderApiVersionReader("api-version"),            //uses a header named api-version
                //new MediaTypeApiVersionReader("api-version")          //uses a media type named api-version
                );

                opts.RouteConstraintName = "apiVersion";
            });

            httpConfig.AddVersionedApiExplorer(opts =>
            {
                //this sets the group name format
                //https://github.com/microsoft/aspnet-api-versioning/wiki/Version-Format
                opts.GroupNameFormat = "'v'VVV";

                //this is needed when using the UrlSegmentApiVersionReader
                opts.SubstituteApiVersionInUrl = true;
            });

           //Web API routes
           var constraintResolver = new DefaultInlineConstraintResolver()
           {
               ConstraintMap =
               {
                    ["apiVersion"]=typeof(ApiVersionRouteConstraint)
               }
           };
            httpConfig.MapHttpAttributeRoutes(constraintResolver);

            //we are using attribute routes so we don't use route templates
            //httpConfig.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/v{apiVersion:ApiVersion}/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
