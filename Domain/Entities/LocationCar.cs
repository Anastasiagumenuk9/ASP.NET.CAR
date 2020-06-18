using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class LocationCar : BaseEntity
    {
        public Guid CarId { get; set; }

        public Guid LocationId { get; set; }

        public Car Car { get; set; }

        public Location Location { get; set; }
    }
}
