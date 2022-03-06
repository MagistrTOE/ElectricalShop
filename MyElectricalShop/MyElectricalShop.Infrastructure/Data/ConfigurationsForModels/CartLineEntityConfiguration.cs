using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Infrastructure.Data.ConfigurationsForModels
{
    public class CartLineEntityConfiguration : IEntityTypeConfiguration<CartLine>
    {
        public void Configure(EntityTypeBuilder<CartLine> builder)
        {
            builder
                .ToTable("CartLines");

            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Product)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.ProductId);

            builder
                .HasOne(x => x.Cart)
                .WithMany(x => x.CartLines)
                .IsRequired()
                .HasForeignKey(x => x.CartId);

            builder
                .HasIndex(x => new { x.ProductId, x.CartId })
                .IsUnique();
        }
    }
}
