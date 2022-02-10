using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MyElectricalShop.Infrastructure.Data
{
    public static class IServiceCollectionExtensionForProgram
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyElectricalShopContext>(opt => opt
                    .UseNpgsql(configuration.GetConnectionString("MyElectricalShopContext")));
            return services;

        }
    }
}
