using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Infrastructure.Data.ConfigurationsForModels
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .ToTable("Products");

            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Category)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.CategoryId);

            builder
                .HasOne(x => x.Company)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.CompanyId);

            builder
                .HasOne(x => x.VoltageLevel)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.VoltageLevelId);
        }
    }
}
