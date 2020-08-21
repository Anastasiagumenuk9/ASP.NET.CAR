using Application.Common.Interfaces;
using Application.Common.Pagination;
using Application.PhotosCar.Queries.GetPhotosList;
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
            var photos = await _context.PhotosCar
                .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
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
                    ColorId = p.ColorId,
                    CarTypeId = p.CarTypeId,
                    TransmissionId = p.TransmissionId,
                    Photo = t.Photo

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
