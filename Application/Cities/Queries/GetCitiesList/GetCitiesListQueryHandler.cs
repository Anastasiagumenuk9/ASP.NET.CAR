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

namespace Application.Cities.Queries.GetCitiesList
{
    public class GetCitiesListQueryHandler : IRequestHandler<GetCitiesListQuery, CitiesListVm>
    {
        private readonly ICarDbContext _context;
        private readonly IMapper _mapper;

        public GetCitiesListQueryHandler(ICarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CitiesListVm> Handle(GetCitiesListQuery request, CancellationToken cancellationToken)
        {
            var cities = await _context.Cities
                .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            var locations = await _context.Locations
                .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            var result = cities.Join(locations,
               p => p.Id,
               t => t.CityId,
               (p, t) => new CityDto
               {
                   Id = p.Id,
                   Name = p.Name,
                   LocationId = t.Id,
                   LocationName = t.Name
               });

            var vm = new CitiesListVm
                {
                    Cities = cities,
                    CreateEnabled = true
                };

            return vm;
        }
    }
}
