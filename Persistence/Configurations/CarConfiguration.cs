using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(p => p.PhotosCar)
                 .WithOne(p => p.Car)
                 .HasForeignKey(s => s.CarId);

            builder.HasMany(p => p.LocationsCars)
                 .WithOne(p => p.Car)
                 .HasForeignKey(s => s.CarId);

            builder.HasMany(p => p.Rents)
                 .WithOne(p => p.Car)
                 .HasForeignKey(s => s.CarId);
        }
    }
}
