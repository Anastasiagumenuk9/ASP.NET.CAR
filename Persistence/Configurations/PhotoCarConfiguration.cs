using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations
{
    public class PhotoCarConfiguration : IEntityTypeConfiguration<PhotoCar>
    {
        public void Configure(EntityTypeBuilder<PhotoCar> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
