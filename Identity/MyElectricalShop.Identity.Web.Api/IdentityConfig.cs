using IdentityServer4;
using IdentityServer4.Models;
using System.Linq;

namespace MyElectricalShop.Identity.Web.Api
{
    public class IdentityConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources() =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> ApiResources() =>
            new ApiResource[]
            {
                new ApiResource("MyElectricalShop", "MyElectricalShop API"),
                new ApiResource("identity_api", "Identity API")
            };

        public static IEnumerable<ApiScope> GetApiScopes(IConfiguration configuration)
        {
            var apiScopes = configuration.GetSection("IdentityOptions").Get<IdentityOptions>().ApiScopes.ToArray();

            return apiScopes.Select(x => new ApiScope
            {
                Name = x.Name,
                DisplayName = x.DisplayName,
                Description = x.Description,
                Required = x.Required,
                Emphasize = x.Required,
                Enabled = true,
                ShowInDiscoveryDocument = true
            });
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            var client = configuration.GetSection("IdentityOptions").Get<IdentityOptions>().Clients.ToArray();
            var secret = configuration.GetSection("IdentityOptions").Get<IdentityOptions>().Secrets.ToArray();

            return client.Select(x => new Client
            {
                ClientId = x.ClientId,
                RequireClientSecret = true,
                ClientSecrets = ,
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                ProtocolType = "oidc",
                AllowAccessTokensViaBrowser = false,
                RedirectUris = x.RedirectUris,
                PostLogoutRedirectUris = x.PostLogoutRedirectUris,
                AllowedScopes = x.AllowedScopes,
                AllowedCorsOrigins = x.AllowedCorsOrigins
            });
        }

            //public static IEnumerable<Client> Clients() =>
            //new List<Client>
            //{
            //    new Client
            //    {
            //        ClientId = "swagger",
            //        RequireClientSecret = true,
            //        ClientSecrets = new List<Secret>
            //        {
            //            new Secret("secret_swagger".Sha256())
            //        },
            //        AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
            //        ProtocolType = "oidc",
            //        AllowAccessTokensViaBrowser = false,
            //        RedirectUris = 
            //        {"http://localhost:10000/swagger/oauth2-redirect.html"},
            //        PostLogoutRedirectUris = {"http://localhost:5000/swagger/"},
            //        AllowedScopes =
            //        {
            //            "identity_api",
            //            IdentityServerConstants.StandardScopes.OpenId,
            //            IdentityServerConstants.StandardScopes.Profile,
            //        },
            //        AllowedCorsOrigins =
            //        {
            //            "http://localhost:10000",
            //        }
            //    },
            //    new Client
            //    {
            //        ClientId = "swagger_shop",
            //        RequireClientSecret = true,
            //        ClientSecrets = new List<Secret>
            //        {
            //            new Secret("secret_swagger_shop".Sha256())
            //        },
            //        AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
            //        ProtocolType = "oidc",
            //        AllowAccessTokensViaBrowser = false,
            //        RedirectUris = {"https://localhost:5001/swagger/oauth2-redirect.html"},
            //        PostLogoutRedirectUris = {"https://localhost:5001/swagger/"},
            //        AllowedScopes =
            //        {
            //            "MyElectricalShop",
            //            IdentityServerConstants.StandardScopes.OpenId,
            //            IdentityServerConstants.StandardScopes.Profile,
            //        },
            //        AllowedCorsOrigins =
            //        {
            //            "https://localhost:5001"
            //        }
            //    },
            //};

            
    }
}