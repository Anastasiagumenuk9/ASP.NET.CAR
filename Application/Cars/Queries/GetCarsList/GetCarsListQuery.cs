using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cars.Queries.GetCarsList
{
    public class GetCarsListQuery : IRequest<CarsListVm>
    {
    }
}
