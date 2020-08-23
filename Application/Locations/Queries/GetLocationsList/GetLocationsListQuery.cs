using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Locations.Queries.GetLocationsListById
{
    public class GetLocationsListQuery : IRequest<LocationsListVm>
    {
    }
}
