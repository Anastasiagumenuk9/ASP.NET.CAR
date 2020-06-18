using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICarDbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<CarType> CarTypes { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<LocationCar> LocationsCars { get; set; }

        public DbSet<PhotoCar> PhotosCar { get; set; }

        public DbSet<Rent> Rents { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
