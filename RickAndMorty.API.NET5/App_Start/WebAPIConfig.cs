﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace RickAndMorty.API.NET5.App_Start
{
    /// <summary>
    /// This class contains API Version configuration.
    /// This class is used to just to isolate the API Version configuration logic.
    /// The steps in ConfigureServices and Configure can be copied into the same methods in Startup.cs
    /// https://github.com/microsoft/aspnet-api-versioning
    /// </summary>
    /// <param name="services"></param>
    public static class WebAPIConfig
    {
        // this serice registration method is called 2nd
        public static void WebAPIVersioningConfigureServices(this IServiceCollection services)
        {
            //create a temporary service provider so you can get your initially registered services if needed
            //this service provider will only contain services registered prior to this statement
            //make sure to always use a using block or make sure to manuall dispose of your temporary serviceProvider
            //so that you don't have multiple copies of your singleton instances
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                services.AddApiVersioning(opts =>
                {
                    //this announces valid versions in a response header named api-supported-versions
                    //and deprecated verions in a response header named api-deprecated-versions
                    opts.ReportApiVersions = true;

                    //when an api version is not explicitly listed a default version is assumed
                    opts.AssumeDefaultVersionWhenUnspecified = true;
                    //this is the default api version
                    opts.DefaultApiVersion = new ApiVersion(1, 0);

                    //defines where the api version will be present in the request
                    opts.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader()                        //uses the parameter of type ApiVersion in the route
                    //new QueryStringApiVersionReader("api-version"),       //uses a query string parameter named api-version
                    //new HeaderApiVersionReader("api-version"),            //uses a header named api-version
                    //new MediaTypeApiVersionReader("api-version")          //uses a media type named api-version
                    );
                });

                services.AddVersionedApiExplorer(opts =>
                {
                    //this sets the group name format
                    //https://github.com/microsoft/aspnet-api-versioning/wiki/Version-Format
                    opts.GroupNameFormat = "VVVV";

                    //this is needed when using the UrlSegmentApiVersionReader
                    opts.SubstituteApiVersionInUrl = true;
                });
            }
        }
    }
}
