using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyElectricalShop.Identity.Infrastructure.Data
{
    class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            optionsBuilder.UseNpgsql("User ID=postgres;Password=password;Host=localhost;Port=5432;Database=IdentityDb;");
            
            return new IdentityContext(optionsBuilder.Options);
        }
    }
}
