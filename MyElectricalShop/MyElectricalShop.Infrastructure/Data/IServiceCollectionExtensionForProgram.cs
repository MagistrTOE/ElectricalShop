using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Infrastructure.Data.Repositories;
using MyElectricalShop.Infrastructure.Repositories;

namespace MyElectricalShop.Infrastructure.Data
{
    public static class IServiceCollectionExtensionForProgram
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyElectricalShopContext>(opt => opt
                    .UseNpgsql(configuration.GetConnectionString("MyElectricalShopContext")));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IVoltageLevelRepository, VoltageLevelRepository>();
            services.AddTransient<ICartRepository, CartRepository>();

            return services;
        }
    }
}
