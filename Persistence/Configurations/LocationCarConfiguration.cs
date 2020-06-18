using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations
{
    public class LocationCarConfiguration : IEntityTypeConfiguration<LocationCar>
    {
        public void Configure(EntityTypeBuilder<LocationCar> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
