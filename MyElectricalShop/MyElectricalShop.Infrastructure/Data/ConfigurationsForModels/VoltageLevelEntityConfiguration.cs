﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Infrastructure.Data.ConfigurationsForModels
{
    public class VoltageLevelEntityConfiguration : IEntityTypeConfiguration<VoltageLevel>
    {
        public void Configure(EntityTypeBuilder<VoltageLevel> builder)
        {
            builder
                .ToTable("VoltageLevels");

            builder
                .HasKey(x => x.Id);
        }
    }
}
