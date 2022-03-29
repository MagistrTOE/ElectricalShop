using IdentityServer4.Models;

namespace MyElectricalShop.Identity.Web.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
           new IdentityResource[]
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
           };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("MyElectricalShop")
            };
       
        public static IEnumerable<Client> Clients =>
             new List<Client>
             {
                 new Client
                 {
                     ClientId = "client",
                     AllowedGrantTypes = GrantTypes.ClientCredentials,
                     ClientSecrets =
                     {
                        new Secret("secret".Sha256())
                     },
                     AllowedScopes = {"MyElectricalShop"}
                 }
             };
    }  
}
