 using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PhotoCar : BaseEntity
    {
        public string Name { get; set; }

        public byte[] Photo { get; set; }

        public Guid CarId { get; set; }

        public Car Car { get; set; }
    }
}
