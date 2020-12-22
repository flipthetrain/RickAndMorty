using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RickAndMorty.API.NETCore.App_Start
{
    /// <summary>
    /// This class contains Swagger configuration.
    /// This class is used to just to isolate the Swagger configuration logic.
    /// The steps in ConfigureServices and Configure can be copied into the same methods in Startup.cs
    /// 
    /// https://github.com/domaindrivendev/Swashbuckle.AspNetCore
    /// </summary>
    public static class SwaggerConfig
    {
        //discover all api versions through reflections
        //apiversionexplorerprovider does not respect the maptoapiversion attribute so we have to manually build a list of all the versions
        private static IEnumerable<ApiVersionInfo> allVersions =
                                 (//get all api versions by iterating through controllers and actions and discovering api versions
                                 from controller in Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(ControllerBase).IsAssignableFrom(t))
                                 from version in (
                                                 // get version from controller based on api version attributes
                                                 from versionAttr in controller.GetCustomAttributes<ApiVersionAttribute>()
                                                 from version in versionAttr.Versions
                                                 select new ApiVersionInfo() { Version = version.ToString(), IsDeprecated = versionAttr.Deprecated }
                                                 ).Union
                                                 (
                                                 //get all public methods in this controller
                                                 from method in controller.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                                                 from versionInfo in (
                                                                     // get version from method based on api version attributes
                                                                     from versionAttr in method.GetCustomAttributes<ApiVersionAttribute>()
                                                                     from version in versionAttr.Versions
                                                                     select new ApiVersionInfo() { Version = version.ToString(), IsDeprecated = versionAttr.Deprecated }
                                                                     ).Union
                                                                     (
                                                                     // get version from method based on map to api version attributes
                                                                     from versionAttr in method.GetCustomAttributes<MapToApiVersionAttribute>()
                                                                     from version in versionAttr.Versions
                                                                     select new ApiVersionInfo() { Version = version.ToString(), IsDeprecated = false }
                                                                     )
                                                 select versionInfo
                                                 )
                                 select version
                                )
                            .Distinct()
                            .OrderBy(v => v.Version);

        public static void SwaggerConfigureServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(opts =>
            {
                //create a json swaggerdoc for each api version in the controllers
                foreach (var item in allVersions)
                {
                    OpenApiInfo apiInfo = new OpenApiInfo()
                    {
                        Title = $"v{item.Version} API",
                        Version = $"v{item.Version}",
                        Description = item.IsDeprecated ? "" : "(deprecated)"
                    };
                    opts.SwaggerDoc(item.Version, apiInfo);
                }
            });
        }

        public static void SwaggerConfigure(this IApplicationBuilder app)
        {
            //add swagger middleware
            app.UseSwagger();

            //add swagger ui middleware
            app.UseSwaggerUI(opts =>
            {
                //this enables bookmarks directly to a swagger api endpoint
                opts.EnableDeepLinking();

                //this enables show the total request time in swagger
                opts.DisplayRequestDuration();

                foreach (var item in allVersions)
                {
                    opts.SwaggerEndpoint($"/swagger/{item.Version}/swagger.json", $"Rick And Morty API {item.Version}");
                }
            });
        }
    }

    public class ApiVersionInfo
    {
        public string Version { get; set; }
        public bool IsDeprecated { get; set; }
    }
}
