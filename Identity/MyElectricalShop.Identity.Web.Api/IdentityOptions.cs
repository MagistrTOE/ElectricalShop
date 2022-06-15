using IdentityServer4.Models;

namespace MyElectricalShop.Identity.Web.Api
{
    public class IdentityOptions
    {
        public ICollection<ApiScope> ApiScopes { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ICollection<Secret> Secrets { get; set; }
        public string Secret { get; set; }
    }
}
