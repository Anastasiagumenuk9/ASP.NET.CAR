using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Car :  AuditableEntity
    {
        public Car()
        {
            PhotosCar = new HashSet<PhotoCar>();
            LocationsCars = new HashSet<LocationCar>();
            Rents = new HashSet<Rent>();
        }

        public string Name { get; set; }

        public string ShortDesc { get; set; }

        public ushort Price { get; set; }

        public ushort PriceSecond { get; set; }

        public ushort PriceThird { get; set; }

        public ushort PriceFourth { get; set; }

        public ushort? Run { get; set; }

        public ushort SeetsCount { get; set; }

        public double TankVolume { get; set; }

        public bool Available { get; set; }

        public bool Conditioner { get; set; }

        public Guid? ColorId { get; set; }

        public Guid? CarTypeId { get; set; }

        public Guid? TransmissionId { get; set; }

        public Color Color { get; set; }

        public CarType CarType { get; set; }

        public Transmission Transmission { get; set; }

        public ICollection<PhotoCar> PhotosCar { get; private set; }

        public ICollection<LocationCar> LocationsCars { get; private set; }

        public ICollection<Rent> Rents { get; private set; }
    }
}
