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

            return client.Select(x => new Client
            {
                ClientId = x.ClientId,
                RequireClientSecret = true,
                ClientSecrets = new List<Secret>
                {
                    new Secret(x.ClientSecret.Sha256())
                },
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                ProtocolType = "oidc",
                AllowAccessTokensViaBrowser = false,
                RedirectUris = x.RedirectUris,
                PostLogoutRedirectUris = x.PostLogoutRedirectUris,
                AllowedScopes = x.AllowedScopes,
                AllowedCorsOrigins = x.AllowedCorsOrigins
            });
        }
    }
}