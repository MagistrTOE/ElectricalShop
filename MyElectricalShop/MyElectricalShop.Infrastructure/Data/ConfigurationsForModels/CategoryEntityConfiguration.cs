using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Infrastructure.Data.ConfigurationsForModels
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .ToTable("Categories");

            builder
                .HasKey(x => x.Id);
           
            builder
                .HasData(DefaultCategory());
        }

        public List<Category> DefaultCategory()
        {
            return new List<Category>
            {
                new Category(1,"Устройство плавного пуска"),
                new Category(2,"Частотный преобразователь")
            };
        }
    }
}
