using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MyElectricalShop.Web.Api.ExtensionsForProgram
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerCase(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "MyElectricalShop", Version = "v1", Description = "MyElectricalShop" });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    In = ParameterLocation.Header,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("http://localhost:10000/connect/authorize"),
                            TokenUrl = new Uri("http://localhost:10000/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"MyElectricalShop", "MyElectricalShop"}
                            }
                        }
                    }
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }
    }
}
