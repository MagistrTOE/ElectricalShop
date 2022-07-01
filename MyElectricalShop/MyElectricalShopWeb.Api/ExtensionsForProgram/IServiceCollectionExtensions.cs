using Core.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MyElectricalShop.Web.Api.ExtensionsForProgram
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerCase(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerSettings = configuration.GetSection("Swagger").Get<SwaggerSettings>();

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
                            AuthorizationUrl = new Uri(swaggerSettings.AuthorizationUrl),
                            TokenUrl = new Uri(swaggerSettings.TokenUrl),
                            Scopes = new Dictionary<string, string>
                            {
                                {swaggerSettings.Audience, swaggerSettings.Audience}
                            }
                        }
                    }
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }

        public static IServiceCollection AddAuthenticationCase(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("IdentityServer4").Get<IdentitySettings>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = settings.AuthorityUrl;
                    options.Audience = settings.Audience;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ClockSkew = settings.TokenLifeTime,
                        ValidateAudience = false
                    };
                });
            return services;
        }
    }
    public static class MvcOptionExtensions
    {
        public static void UseCentralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        {
            opts.Conventions.Add(new RoutePrefixConvention(routeAttribute));
        }

        public static void UseCentralRoutePrefix(this MvcOptions opts, string prefix)
        {
            opts.UseCentralRoutePrefix(new RouteAttribute(prefix));
        }
    }

    public class RoutePrefixConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _routePrefix;

        public RoutePrefixConvention(IRouteTemplateProvider route)
        {
            _routePrefix = new AttributeRouteModel(route);
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var selector in application.Controllers.SelectMany(c => c.Selectors))
            {
                selector.AttributeRouteModel = selector.AttributeRouteModel != null
                    ? AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel)
                    : _routePrefix;
            }
        }
    }
}
