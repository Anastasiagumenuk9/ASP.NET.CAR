using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using Application.Cars.Queries.GetCarsList;
using Application.Locations.Queries.GetLocationsListById;

namespace Application.Rents.Queries.GetPersonalRentsList
{
    public class GetPersonalRentsListQueryHandler : IRequestHandler<GetPersonalRentsListQuery, PersonalRentsListVm>
    {
        private readonly ICarDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetPersonalRentsListQueryHandler(ICarDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PersonalRentsListVm> Handle(GetPersonalRentsListQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cars = await _context.Cars
               .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
               .OrderBy(p => p.Name)
               .ToListAsync(cancellationToken);

            var locations = await _context.Locations
               .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
               .OrderBy(p => p.Name)
               .ToListAsync(cancellationToken);

            var rents = await _context.Rents.Where(c => c.ApplicationUserId == userId)
               .ProjectTo<RentDto>(_mapper.ConfigurationProvider)
               .OrderBy(p => p.DataConfirmed)
               .ToListAsync(cancellationToken);

            var result = rents.Join(cars,
                p => p.CarId,
                t => t.Id,
                (p, t) => new RentDto
                {
                    Id = p.Id,
                    IsConfirmed = p.IsConfirmed,
                    StartDataRend = p.StartDataRend,
                    FinishDataRend = p.FinishDataRend,
                    DataConfirmed = p.DataConfirmed,
                    ApplicationUserId = p.ApplicationUserId,
                    Car = t.Name,
                    LocationId = p.LocationId,
                    Price = p.Price
                }).Join(locations,
                p => p.LocationId,
                t => t.Id,
                (p, t) => new RentDto
                {
                    Id = p.Id,
                    IsConfirmed = p.IsConfirmed,
                    StartDataRend = p.StartDataRend,
                    FinishDataRend = p.FinishDataRend,
                    DataConfirmed = p.DataConfirmed,
                    ApplicationUserId = p.ApplicationUserId,
                    Car = p.Car,
                    Location = t.Name,
                    Price = p.Price
                }).ToList<RentDto>();

            var vm = new PersonalRentsListVm
            {
                Rents = result,
                CreateEnabled = true
            };

            return vm;
        }
    }
}
