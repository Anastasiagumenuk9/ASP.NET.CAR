using Application.Cities.Queries.GetCitiesList;
using Application.Common.Interfaces;
using Application.Locations.Queries.GetLocationsListById;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CitiesLocations.Queries
{
    public class GetCitiesLocationsListQueryHandler : IRequestHandler<GetCitiesLocationsListQuery, CitiesLocationsListVm>
    {
        private readonly ICarDbContext _context;
        private readonly IMapper _mapper;

        public GetCitiesLocationsListQueryHandler(ICarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CitiesLocationsListVm> Handle(GetCitiesLocationsListQuery request, CancellationToken cancellationToken)
        {
            var cities = await _context.Cities
                .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            var locations = await _context.Locations
               .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
               .OrderBy(p => p.Name)
               .ToListAsync(cancellationToken);

            var vm = new CitiesLocationsListVm
            {
                Cities = cities,
                Locations = locations,
                CreateEnabled = true
            };

            return vm;
        }
    }
}
