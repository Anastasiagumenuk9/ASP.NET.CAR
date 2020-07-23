using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cars.Commands.DeleteCar
{
    public class DeleteCarCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
