using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MyElectricalShop.Identity.Infrastructure.Data
{
    public static class IServiceCollectionExtensionForProgram
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(opt => opt
                    .UseNpgsql(configuration.GetConnectionString("IdentityContext")));
            services.AddRazorPages();
            
            return services;

        }
    }
}
