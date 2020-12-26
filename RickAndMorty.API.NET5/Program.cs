using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RickAndMorty.API.NET5.App_Start;

namespace RickAndMorty.API.NET5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //add additional app configuration entries here
                .ConfigureAppConfiguration((context, config) =>
                {
                    //get the default configuration in case you need to read existing values
                    //for example you may need to read appsettings.json to get the name of a keyvault to read more values
                    //so you could use this builtConfig to get the keyvault name since appsettings.json is read by default
                    var builtConfig = config.Build();

                    //add additional configuration builders to the config
                    //https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfigurationbuilder?view=dotnet-plat-ext-5.0
                    //https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.azurekeyvaultconfigurationextensions.addazurekeyvault?view=dotnet-plat-ext-3.1
                    config.AddEnvironmentVariables();
                })

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //use startup class in lieu of separate methods
                    //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup?view=aspnetcore-5.0
                    webBuilder.UseStartup<Startup>();
                });
    }
}
