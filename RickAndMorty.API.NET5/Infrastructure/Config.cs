using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RickAndMorty.API.NET5.Infrastructure
{
    /// <summary>
    /// This record is responsible for obtaining all configuration values needed for the application to operation successfully
    /// this record needs to be able to read from any configuration data source (i.e. appsettings.json, keyvaults, databases, etc.)
    /// </summary>
    public record Config
    {
        private readonly IConfiguration _configuration;
        public Config(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string AllowedHosts => _configuration["AllowedHosts"];
        public string SwaggerOAuthAppName => _configuration["SwaggerOAuthAppName"];
        public string SwaggerOAuthClientId => _configuration["SwaggerOAuthClientId"];
        public string SwaggerOAuthClientSecret => _configuration["SwaggerOAuthClientSecret"];
        //this is a C# IIFE -- Immediately Invoked Function Expression
        public Dictionary<string, string> SwaggerOAuthScopes => ((Func<Dictionary<string, string>>)(() =>
        {
            Dictionary<string, string> results = (
                                                from p in _configuration.GetSection("SwaggerOAuthScopes").GetChildren()
                                                select new { p.Key, p.Value }
                                                ).ToDictionary(x => x.Key, x => x.Value);
            return results;
        }))();
        //this is a C# IIFE -- Immediately Invoked Function Expression
        public Dictionary<string, string> SwaggerQueryStringParams => ((Func<Dictionary<string,string>>)(() =>
        {
            Dictionary<string, string> results = (
                                                from p in _configuration.GetSection("SwaggerQueryStringParams").GetChildren()
                                                select new { p.Key, p.Value }
                                                ).ToDictionary(x => x.Key, x => x.Value);
            return results;
        }))();
    }
}
