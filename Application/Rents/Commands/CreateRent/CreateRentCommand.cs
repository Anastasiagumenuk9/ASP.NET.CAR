using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rents.Commands.CreateRent
{
    public class CreateRentCommand : IRequest<Guid>
    {
        public bool IsConfirmed { get; set; }

        public DateTime StartDataRend { get; set; }

        public DateTime FinishDataRend { get; set; }

        public DateTime DataConfirmed { get; set; }

        public ushort Price { get; set; }

        public Guid CarId { get; set; }

        public Guid LocationId { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
