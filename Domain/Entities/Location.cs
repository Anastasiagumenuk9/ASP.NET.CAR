using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Location : BaseEntity
    {
        public Location()
        {
            LocationsCars = new HashSet<LocationCar>();
            Rents = new HashSet<Rent>();
        }

        public string Name { get; set; }

        public Guid CityId { get; set; }

        public City City { get; set; }

        public ICollection<LocationCar> LocationsCars { get; set; }

        public ICollection<Rent> Rents { get; set; }
    }
}
