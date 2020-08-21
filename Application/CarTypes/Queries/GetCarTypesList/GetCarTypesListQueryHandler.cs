using Application.Common.Interfaces;
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

namespace Application.CarTypes.Queries.GetCarTypesList
{
    public class GetCarTypesListQueryHandler : IRequestHandler<GetCarTypesListQuery, CarTypesListVm>
    {
        private readonly ICarDbContext _context;
        private readonly IMapper _mapper;

        public GetCarTypesListQueryHandler(ICarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CarTypesListVm> Handle(GetCarTypesListQuery request, CancellationToken cancellationToken)
        {
            var carTypes = await _context.CarTypes
               .ProjectTo<CarTypeDto>(_mapper.ConfigurationProvider)
               .OrderBy(p => p.Name)
               .ToListAsync(cancellationToken);

            var vm = new CarTypesListVm
            {
                CarTypes = carTypes,
                CreateEnabled = true
            };

            return vm;
        }
    }
}
