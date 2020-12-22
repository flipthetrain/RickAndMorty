using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RickAndMorty.API.NET5.App_Start
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
        public static void SwaggerConfigureServices(this IServiceCollection services)
        {
            IApiVersionDescriptionProvider apiVersionDescriptionProvider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(opts =>
            {
                //assign api to correct verions
                #region configure swagger documents for each api version
                //create a json swaggerdoc for each api version in the controllers
                foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    OpenApiInfo apiInfo = new OpenApiInfo()
                    {
                        Title = $"v{item.ApiVersion} API",
                        Version = $"v{item.ApiVersion}",
                        Description = item.IsDeprecated ? "" : "(deprecated)"
                    };
                    opts.SwaggerDoc(item.ApiVersion.ToString(), apiInfo);
                }

                //determines if the given apiDesc belongs in the given version
                opts.DocInclusionPredicate((version, apiDesc) =>
                {
                    bool result = apiDesc.GetApiVersion().ToString().Equals(version, System.StringComparison.OrdinalIgnoreCase);
                    return result;
                });
                #endregion

                //add xml documentation to swagger
                opts.OperationFilter<SwaggerDefaultValues>();

                //configure security
                //when the user clicks "authorize" in swagger api the user will be redirected to the following url:
                #region authorize
                #region ApiKey Auth
                //ApiKey Cookie
                opts.AddSecurityDefinition("apiKeyCookie", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Cookie,
                    Name = "ApiKeyCookie",
                });

                //ApiKey Header
                opts.AddSecurityDefinition("apiKeyHeader", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "ApiKeyHeader"
                });

                //ApiKey Path
                opts.AddSecurityDefinition("apiKeyPath", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Path,
                    Name = "ApiKeyPath"
                });

                //ApiKey Query
                opts.AddSecurityDefinition("apiKeyQuery", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Query,
                    Name = "ApiKeyQuery"
                });
                #endregion

                #region http
                //Http Basic
                //https://tools.ietf.org/html/rfc7617
                opts.AddSecurityDefinition("httpBasic", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Basic",
                });

                //Http Bearer
                //https://tools.ietf.org/html/rfc6750
                opts.AddSecurityDefinition("httpBearer", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                });
                #endregion

                #region OAuth2
                //OAuth2 Authorization Code
                opts.AddSecurityDefinition("OAuth2AuthCode", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("https://localhost:5001/Token"),
                            RefreshUrl = new Uri("https://localhost:5001/Token"),
                            AuthorizationUrl = new Uri("https://localhost:5001/oauth2/auth"),
                            Scopes = {
                                    { "scopeId1","scopeDesc1"},
                                    { "scopeId2","scopeDesc2"},
                                    { "scopeId3","scopeDesc3"}
                                }
                        }
                    }
                });

                //OAuth2 Client Credentials
                opts.AddSecurityDefinition("OAuth2ClientCredentials", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("https://localhost:5001/Token"),
                            RefreshUrl = new Uri("https://localhost:5001/Token"),
                            AuthorizationUrl = new Uri("https://localhost:5001/oauth2/auth"),
                            Scopes = {
                                    { "scopeId1","scopeDesc1"},
                                    { "scopeId2","scopeDesc2"},
                                    { "scopeId3","scopeDesc3"}
                                }
                        }
                    }
                });

                //OAuth2 Implicit
                opts.AddSecurityDefinition("OAuth2Implicit", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            TokenUrl = new Uri("https://localhost:5001/Token"),
                            RefreshUrl = new Uri("https://localhost:5001/Token"),
                            AuthorizationUrl = new Uri("https://localhost:5001/oauth2/auth"),
                            Scopes = {
                                { "scopeId1","scopeDesc1"},
                                { "scopeId2","scopeDesc2"},
                                { "scopeId3","scopeDesc3"}
                            }
                        }
                    }
                });


                //OAuth2 Password
                opts.AddSecurityDefinition("OAuth2Password", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Password = new OpenApiOAuthFlow()
                        {
                            TokenUrl = new Uri("https://localhost:5001/Token"),
                            RefreshUrl = new Uri("https://localhost:5001/Token"),
                            AuthorizationUrl = new Uri("https://localhost:5001/oauth2/auth"),
                            Scopes = {
                                { "scopeId1","scopeDesc1"},
                                { "scopeId2","scopeDesc2"},
                                { "scopeId3","scopeDesc3"}
                            }
                        },
                    },
                });
            });
            #endregion
            #endregion
        }

        public static void SwaggerConfigure(this IApplicationBuilder app)
        {
            IApiVersionDescriptionProvider apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            //add swagger middleware
            app.UseSwagger();

            //add swagger ui middleware
            app.UseSwaggerUI(opts =>
            {
                //this enables bookmarks directly to a swagger api endpoint
                opts.EnableDeepLinking();
                //this enables show the total request time in swagger
                opts.DisplayRequestDuration();

                //configure swaggerui for oauth2
                opts.OAuthAppName("RickAndMortyApp");
                opts.OAuthClientId("RickAndMortyClient");
                opts.OAuthClientSecret("RickAndMortySecret");
                //used to pass additional information to the OAuth server
                opts.OAuthAdditionalQueryStringParams(new Dictionary<string, string>()
                {
                    {"key1","value1" },
                    {"key2","value2" },
                    {"key3","value3" }
                });

                foreach (var item in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    opts.SwaggerEndpoint($"/swagger/{item.ApiVersion}/swagger.json", $"Rick And Morty API {item.ApiVersion}");
                }
            });
        }
    }

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            ApiDescription apiDescription = context.ApiDescription;
            operation.Deprecated |= apiDescription.IsDeprecated();
            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var item in operation.Parameters)
            {
                ApiParameterDescription apiParamDesc = apiDescription.ParameterDescriptions.First(p => p.Name.Equals(item.Name, System.StringComparison.Ordinal));
                if (apiParamDesc.ParameterDescriptor == null)
                {
                    item.Description = apiParamDesc.ModelMetadata?.Description;
                }
                item.Required |= apiParamDesc.IsRequired;
            }

        }
    }
}
