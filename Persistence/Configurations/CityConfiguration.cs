using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations
{
    class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
              .IsRequired()
              .HasMaxLength(50);

            builder.HasMany(p => p.Locations)
                .WithOne(p => p.City)
                .HasForeignKey(s => s.CityId);
        }
    }
}
