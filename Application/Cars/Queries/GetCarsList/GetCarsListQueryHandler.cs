using Application.Common.Interfaces;
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
            var cars = await _context.Cars
                .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            var vm = new CarsListVm
            {
                Cars = cars,
                CreateEnabled = true // TODO: Set based on user permissions.
            };

            return vm;
        }
    }
}
