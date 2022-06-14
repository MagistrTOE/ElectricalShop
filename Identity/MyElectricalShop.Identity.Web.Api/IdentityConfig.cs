using IdentityServer4;
using IdentityServer4.Models;

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

        public static IEnumerable<ApiScope> ApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope
                {
                    Name = "identity_api",
                    DisplayName = "Identity API scope",
                    Enabled = true,
                    ShowInDiscoveryDocument = true
                },
                new ApiScope
                {
                    Name = "MyElectricalShop",
                    DisplayName = "MyElectricalShop API scope",
                    Description = null,
                    Required = true,
                    Emphasize = false,
                    Enabled = true,
                    ShowInDiscoveryDocument = true
                }
            };

        public static IEnumerable<Client> Clients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "swagger",
                    RequireClientSecret = true,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret_swagger".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    ProtocolType = "oidc",
                    AllowAccessTokensViaBrowser = false,
                    RedirectUris = 
                    {"http://localhost:10000/swagger/oauth2-redirect.html"},
                    PostLogoutRedirectUris = {"http://localhost:5000/swagger/"},
                    AllowedScopes =
                    {
                        "identity_api",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:10000",
                    }
                },
                new Client
                {
                    ClientId = "swagger_shop",
                    RequireClientSecret = true,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret_swagger_shop".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    ProtocolType = "oidc",
                    AllowAccessTokensViaBrowser = false,
                    RedirectUris = {"https://localhost:5001/swagger/oauth2-redirect.html"},
                    PostLogoutRedirectUris = {"https://localhost:5001/swagger/"},
                    AllowedScopes =
                    {
                        "MyElectricalShop",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },
                    AllowedCorsOrigins =
                    {
                        "https://localhost:5001"
                    }
                },
            };


        public static IEnumerable<ApiScope> GetApiScopes(IConfiguration configuration)
        {
             var apiScopes = configuration.GetSection("IdentityOptions").Get<IdentityOptions>().ApiScopes.ToArray();

            return  apiScopes.Select(x => new ApiScope
            {
                Name = x.Name,
                DisplayName = x.DisplayName,
                Enabled = true,
                ShowInDiscoveryDocument = true
            });
            /*new List<ApiScope>
            {
                new ApiScope
                {
                    Name = "identity_api",
                    DisplayName = "Identity API scope",
                    Enabled = true,
                    ShowInDiscoveryDocument = true
                },
                new ApiScope
                {
                    Name = "MyElectricalShop",
                    DisplayName = "MyElectricalShop API scope",
                    Description = null,
                    Required = true,
                    Emphasize = false,
                    Enabled = true,
                    ShowInDiscoveryDocument = true
                }
            };*/
        }
            
    }
}