using Application.Cities.Queries.GetCitiesList;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Locations.Queries.GetLocationsListById
{
    public class GetLocationsListQueryHandler : IRequestHandler<GetLocationsListQuery, LocationsListVm>
    {
        private readonly ICarDbContext _context;
        private readonly IMapper _mapper;

        public GetLocationsListQueryHandler(ICarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LocationsListVm> Handle(GetLocationsListQuery request, CancellationToken cancellationToken)
        {
            var locations = await _context.Locations
               .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
               .OrderBy(p => p.Name)
               .ToListAsync(cancellationToken);

            var vm = new LocationsListVm
            {
                Locations = locations,
                CreateEnabled = true
            };

            return vm;
        }
    }
}
