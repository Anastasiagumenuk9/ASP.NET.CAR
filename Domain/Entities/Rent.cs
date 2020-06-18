using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Rent : BaseEntity
    {
        public bool IsConfirmed { get; set; }

        public DateTime StartDataRend { get; set; }

        public DateTime FinishDataRend { get; set; }

        public DateTime DataConfirmed { get; set; }

        public ushort Price { get; set; }

        public Guid CarId { get; set; }

        public Car Car { get; set; }

        public Guid LocationId { get; set; }

        public Location Location { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
