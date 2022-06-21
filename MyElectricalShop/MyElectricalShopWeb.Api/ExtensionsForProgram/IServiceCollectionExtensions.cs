using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MyElectricalShop.Web.Api.ExtensionsForProgram
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerCase(this IServiceCollection services, IConfiguration configuration)
        {
            var authorizationUrl = configuration.GetValue<string>("Swagger:AuthorizationUrl");
            var tokenUrl = configuration.GetValue<string>("Swagger:TokenUrl");
            var audience = configuration.GetValue<string>("Swagger:Audience");

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
                            AuthorizationUrl = new Uri(authorizationUrl),
                            TokenUrl = new Uri(tokenUrl),
                            Scopes = new Dictionary<string, string>
                            {
                                {audience, audience}
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
