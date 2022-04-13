using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyElectricalShop.Identity.Domain.Models;

namespace MyElectricalShop.Identity.Infrastructure.Data
{
    public class IdentityContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
    }
}
