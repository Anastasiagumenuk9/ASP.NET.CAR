using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CarTypes.Queries.GetCarTypesList
{
    public class GetCarTypesListQuery : IRequest<CarTypesListVm>
    {
    }
}
