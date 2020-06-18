using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class City : BaseEntity
    {
        public City()
        {
            Locations = new HashSet<Location>();
        }

        public string Name { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}
