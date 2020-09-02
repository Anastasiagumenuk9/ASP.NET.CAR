using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cars.Queries.GetCarDetail
{
    public class GetCarDetailQuery : IRequest<CarDetailVm>
    {
        public Guid Id { get; set; }
    }
}
