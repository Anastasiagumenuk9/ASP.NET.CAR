using Application.CarTypes.Queries.GetCarTypesList;
using Application.Colors.Queries.GetColorsList;
using Application.Common.Interfaces;
using Application.Common.Pagination;
using Application.PhotosCar.Queries.GetPhotosList;
using Application.Transmissions.Queries.GetTransmissionsList;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cars.Queries.GetCarsList
{
    public class GetCarsListQueryHandler : IRequestHandler<GetCarsListQuery, CarsListVm>
    {
        private readonly ICarDbContext _context;
        private readonly IMapper _mapper;

        public GetCarsListQueryHandler(ICarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CarsListVm> Handle(GetCarsListQuery request, CancellationToken cancellationToken)
        {
            var carTypes = await _context.CarTypes
                .ProjectTo<CarTypeDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            var colors = await _context.Colors
                .ProjectTo<ColorDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            var photos = await _context.PhotosCar
                .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            var transmissions = await _context.Transmissions
                .ProjectTo<TransmissionDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            var cars = await _context.Cars
                .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            var result = cars.Join(photos,
                p => p.Id,
                t => t.CarId,
                (p, t) => new CarDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ShortDesc = p.ShortDesc,
                    Price = p.Price,
                    Run = p.Run,
                    SeetsCount = p.SeetsCount,
                    Available = p.Available,
                    Conditioner = p.Conditioner,
                    TankVolume = p.TankVolume,
                    ColorId = p.ColorId,
                    CarTypeId = p.CarTypeId,
                    TransmissionId = p.TransmissionId,
                    Photo = t.Photo

                }).Join(colors,
                p => p.ColorId,
                t => t.Id,
                (p, t) => new CarDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ShortDesc = p.ShortDesc,
                    Price = p.Price,
                    Run = p.Run,
                    SeetsCount = p.SeetsCount,
                    Available = p.Available,
                    Conditioner = p.Conditioner,
                    TankVolume = p.TankVolume,
                    ColorId = p.ColorId,
                    CarTypeId = p.CarTypeId,
                    TransmissionId = p.TransmissionId,
                    Photo = p.Photo,
                    Color = t.Name

                }).Join(carTypes,
                p => p.CarTypeId,
                t => t.Id,
                (p, t) => new CarDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ShortDesc = p.ShortDesc,
                    Price = p.Price,
                    Run = p.Run,
                    SeetsCount = p.SeetsCount,
                    Available = p.Available,
                    Conditioner = p.Conditioner,
                    TankVolume = p.TankVolume,
                    ColorId = p.ColorId,
                    CarTypeId = p.CarTypeId,
                    TransmissionId = p.TransmissionId,
                    Photo = p.Photo,
                    Color = p.Color,
                    CarType = t.Name
                }).Join(transmissions,
                p => p.TransmissionId,
                t => t.Id,
                (p, t) => new CarDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ShortDesc = p.ShortDesc,
                    Price = p.Price,
                    Run = p.Run,
                    SeetsCount = p.SeetsCount,
                    Available = p.Available,
                    Conditioner = p.Conditioner,
                    TankVolume = p.TankVolume,
                    ColorId = p.ColorId,
                    CarTypeId = p.CarTypeId,
                    TransmissionId = p.TransmissionId,
                    Photo = p.Photo,
                    Color = p.Color,
                    CarType = p.CarType,
                    Transmission = t.Name
                }).ToList<CarDto>();

            var count = result.Count();
            var items = result.Skip((request._page - 1) * request._pageSize).Take(request._pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, request._page, request._pageSize);
            var vm = new CarsListVm
            {
                PageViewModel = pageViewModel,
                Cars = (IList<CarDto>)items,
                CreateEnabled = true
            };

            return vm;
        }
    }
}
