using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CitiesLocations.Queries
{
    public class GetCitiesLocationsListQuery : IRequest<CitiesLocationsListVm>
    {
    }
}
