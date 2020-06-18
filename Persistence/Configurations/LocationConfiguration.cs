using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

             builder.HasMany(p => p.LocationsCars)
                 .WithOne(p => p.Location)
                 .HasForeignKey(s => s.LocationId);

             builder.HasMany(p => p.Rents)
                 .WithOne(p => p.Location)
                 .HasForeignKey(s => s.LocationId);
        }
    }
}
