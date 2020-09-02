using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cities.Queries.GetCitiesList
{
    public class GetCitiesListQuery : IRequest<CitiesListVm>
    {
        public Guid _id;

        public GetCitiesListQuery(Guid id)
        {
            _id = id;
        }
    }
}
