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

        public static IServiceCollection AddSwaggerCase(this IServiceCollection services)
        {
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
                            AuthorizationUrl = new Uri("http://localhost:10000/connect/authorize"),
                            TokenUrl = new Uri("http://localhost:10000/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"openid", "openid"},
                                {"identity_api", "identity_api"}
                            }
                        }
                    }
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        public static AuthenticationBuilder AddAuthenticationCase(this IServiceCollection services)
        {
            return services.AddAuthentication(IdentityServerConstants.DefaultCookieAuthenticationScheme)
                .AddOpenIdConnect(options =>
                {
                    options.SignInScheme = IdentityServerConstants.DefaultCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                    options.SaveTokens = true;
                    options.ClientId = "shop_identity";
                    options.ClientSecret = "shop_secret";
                    options.RequireHttpsMetadata = false;
                    options.Authority = "http://localhost:10000";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RoleClaimType = ClaimTypes.Role
                    };
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "http://localhost:10000";
                    options.Audience = "identity_api";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ClockSkew = new TimeSpan(1, 0, 0),
                        ValidateAudience = false
                    };
                });
        }
    }
}
