using Application.Cars.Queries.GetCarsList;
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

            var car = _context.Cars.Where(c => c.Id == request._id).ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Name).Join(_context.PhotosCar,
                t => t.Id,
                p => p.CarId,
                (p, t) => new CarDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ShortDesc = p.ShortDesc,
                    Price = p.Price,
                    PriceSecond = p.PriceSecond,
                    PriceThird = p.PriceThird,
                    PriceFourth = p.PriceFourth,
                    Run = p.Run,
                    SeetsCount = p.SeetsCount,
                    Available = p.Available,
                    Conditioner = p.Conditioner,
                    TankVolume = p.TankVolume,
                    ColorId = p.ColorId,
                    CarTypeId = p.CarTypeId,
                    TransmissionId = p.TransmissionId,
                    Photo = t.Photo
                }).FirstOrDefault();

            var vm = new CitiesListVm
                {
                    Cities = cities,
                    Car = car,
                    CreateEnabled = true
                };

            return vm;
        }
    }
}
