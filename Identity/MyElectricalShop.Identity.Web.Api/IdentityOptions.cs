using IdentityServer4.Models;

namespace MyElectricalShop.Identity.Web.Api
{
    public class IdentityOptions
    {
        public ICollection<ApiScope> ApiScopes { get; set; }
        public ICollection<ClientOptions> Clients { get; set; }

        public class ClientOptions
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public ICollection<string> RedirectUris { get; set; }
            public ICollection<string> PostLogoutRedirectUris { get; set; }
            public ICollection<string> AllowedScopes { get; set; }
            public ICollection<string> AllowedCorsOrigins { get; set; }
        }
    }
}
