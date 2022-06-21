using IdentityServer4;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyModel;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Security.Claims;

namespace MyElectricalShop.Identity.Web.Api.ExtensionsForProgram
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRCase(this IServiceCollection services)
        {
            var assemblies = DependencyContext.Default.RuntimeLibraries
                .SelectMany(assembly => assembly.GetDefaultAssemblyNames(DependencyContext.Default)
                .Where(assemblyName => assemblyName.FullName.StartsWith(nameof(MyElectricalShop)))
                .Select(Assembly.Load))
                .ToArray();

            return services.AddMediatR(assemblies);
        }

        public static IServiceCollection AddSwaggerCase(this IServiceCollection services, IConfiguration configuration)
        {
            var authorizationUrl = configuration.GetValue<string>("Swagger:AuthorizationUrl");
            var tokenUrl = configuration.GetValue<string>("Swagger:TokenUrl");
            var audience = configuration.GetValue<string>("IdentityServer4:Audience");

            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Identity",
                    Version = "v1",
                    Description = "Identity Server API"
                });
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
                                {"openid", "openid"},
                                {audience, audience}
                            }
                        }
                    }
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        public static AuthenticationBuilder AddAuthenticationCase(this IServiceCollection services, IConfiguration configuration)
        {
            var authorityUrl = configuration.GetValue<string>("IdentityServer4:AuthorityUrl");

            return services.AddAuthentication(IdentityServerConstants.DefaultCookieAuthenticationScheme)
                .AddOpenIdConnect(options =>
                {
                    options.SignInScheme = IdentityServerConstants.DefaultCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                    options.SaveTokens = true;
                    options.ClientId = "shop_identity";
                    options.ClientSecret = "shop_secret";
                    options.RequireHttpsMetadata = false;
                    options.Authority = authorityUrl;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RoleClaimType = ClaimTypes.Role
                    };
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = authorityUrl;
                    options.Audience = configuration.GetValue<string>("IdentityServer4:Audience");
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ClockSkew = configuration.GetValue<TimeSpan>("IdentityServer4:TokenLifeTime"),
                        ValidateAudience = false
                    };
                });
        }
    }
}
