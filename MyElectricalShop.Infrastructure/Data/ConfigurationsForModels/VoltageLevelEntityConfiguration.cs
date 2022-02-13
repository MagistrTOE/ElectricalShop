using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Infrastructure.Data.ConfigurationsForModels
{
    public class VoltageLevelEntityConfiguration : IEntityTypeConfiguration<VoltageLevel>
    {
        public void Configure(EntityTypeBuilder<VoltageLevel> builder)
        {
            builder
                .HasKey(x => x.Id);
        }
    }
}
