using Application.Common.Interfaces;
using Ardalis.EFCore.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class CarDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, ICarDbContext
    {

        public CarDbContext(DbContextOptions<CarDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarType> CarTypes { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<LocationCar> LocationsCars { get; set; }

        public DbSet<PhotoCar> PhotosCar { get; set; }

        public DbSet<Rent> Rents { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
        }
    }
}
