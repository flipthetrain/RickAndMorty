using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RickAndMorty.API.NET5.Infrastructure;

namespace RickAndMorty.API.NET5.App_Start
{
    /// <summary>
    /// This is the global application configuration class
    /// </summary>
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // this service registration method is called 1st
        public void ConfigureServices(IServiceCollection services)
        {
            //register the infrastructure configuration service as a singleton so that all references to this type get the same object instance
            //generally do this first in case you need to get any config values for future service registrations
            services.AddSingleton<Config>();

            //create a temporary service provider so you can get your initially registered services if needed
            //this service provider will only contain services registered prior to this statement
            //make sure to always use a using block or make sure to manuall dispose of your temporary serviceProvider
            //so that you don't have multiple copies of your singleton instances
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                //we need our config object to register other services
                Config _config = serviceProvider.GetRequiredService<Config>();

                //add cors options to services
                //configure the cors policy -- this example just sets the default policy, usually a single policy is sufficient but you can use named policies
                //https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.corsservicecollectionextensions.addcors?view=aspnetcore-5.0
                //https://docs.microsoft.com/en-us/previous-versions/aspnet/dn726408(v=vs.118)
                services.AddCors(opts =>
                {
                    CorsPolicy defaultPolicy = new CorsPolicy();
                    defaultPolicy.Origins.Add(_config.AllowedHosts);
                    opts.AddDefaultPolicy(defaultPolicy);
                });

                //add authorization services
                //https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.authorizationservicecollectionextensions.addauthorization?view=aspnetcore-2.2
                services.AddAuthorization();

                //call the WebApi versioning configuration in App_Start/WebApiConfig
                services.WebAPIVersioningConfigureServices();

                //call the Swagger configuration in App_Start/SwaggerConfig
                services.SwaggerConfigureServices();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Middleware modules are processed in the order they are added to the pipeline using the Chain of Responsibility pattern
            //sometimes the order of middleware does not matter; sometimes the order does matter
            //it's up to the developer to know if the order can be rearranged
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-5.0
            //https://en.wikipedia.org/wiki/Chain-of-responsibility_pattern

            if (env.IsDevelopment())
            {
                //use developer exceptions middleware
                //https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.developerexceptionpageextensions.usedeveloperexceptionpage?view=aspnetcore-5.0
                //https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.diagnostics.developerexceptionpagemiddleware?view=aspnetcore-5.0
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //use HSTS (HTTP Strcit Transport Security) middleware
                //https://tools.ietf.org/html/rfc6797
                //https://github.com/aspnet/BasicMiddleware/blob/master/src/Microsoft.AspNetCore.HttpsPolicy/HstsBuilderExtensions.cs
                //https://github.com/aspnet/BasicMiddleware/blob/87d4df52fa20be378bcd6c15f0eacb972fbac481/src/Microsoft.AspNetCore.HttpsPolicy/HstsMiddleware.cs
                app.UseHsts();
            }

            //add cors support middleware
            //https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS
            //https://fetch.spec.whatwg.org/#http-cors-protocol
            //https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.corsmiddlewareextensions.usecors?view=aspnetcore-5.0#:~:text=cross%20domain%20requests.-,UseCors(IApplicationBuilder),to%20allow%20cross%20domain%20requests.
            //https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.cors.infrastructure.corsmiddleware?view=aspnetcore-5.0
            app.UseCors();

            //add swagger support middleware
            app.SwaggerConfigure();

            //add https redirection support middleware
            //https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.httpspolicybuilderextensions.usehttpsredirection?view=aspnetcore-5.0
            //https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.httpspolicy.httpsredirectionmiddleware?view=aspnetcore-5.0
            app.UseHttpsRedirection();

            //add routing support middleware
            //https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.routingbuilderextensions.userouter?view=aspnetcore-5.0
            //https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.routermiddleware?view=aspnetcore-5.0
            app.UseRouting();

            //add authorization support middleware
            //https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.authorizationappbuilderextensions.useauthorization?view=aspnetcore-5.0
            //https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authorization.authorizationmiddleware?view=aspnetcore-5.0
            app.UseAuthorization();

            //add endpoint mapping support middlware
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
