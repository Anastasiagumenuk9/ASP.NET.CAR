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
            var locations = _context.Locations.ToList();
            var cities = _context.Cities.ToList();

            var result = locations.Join(cities,
                t => t.CityId,
                p => p.Id,
                (t, p) => new LocationDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    CityId = t.CityId,
                    CityName = p.Name,

                }).ToList<LocationDto>();

            var vm = new LocationsListVm
            {
                Locations = result,
                CreateEnabled = true
            };

            return vm;
        }
    }
}
