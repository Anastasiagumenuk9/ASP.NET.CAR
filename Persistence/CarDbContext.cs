using Application.Common.Interfaces;
using Ardalis.EFCore.Extensions;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class CarDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, ICarDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public CarDbContext(DbContextOptions<CarDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public CarDbContext(
            DbContextOptions<CarDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarType> CarTypes { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<LocationCar> LocationsCars { get; set; }

        public DbSet<PhotoCar> PhotosCar { get; set; }

        public DbSet<Rent> Rents { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
        }
    }
}
