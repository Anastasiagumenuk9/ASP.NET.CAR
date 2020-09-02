using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cars.Queries.GetCarDetail
{
    public class GetCarDetailQueryHandler : IRequestHandler<GetCarDetailQuery, CarDetailVm>
    {
        private readonly ICarDbContext _context;
        private readonly IMapper _mapper;

        public GetCarDetailQueryHandler(ICarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CarDetailVm> Handle(GetCarDetailQuery request, CancellationToken cancellationToken)
        {
            var vm = await _context.Cars
                .ProjectTo<CarDetailVm>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (vm == null)
            {
                throw new NotFoundException(nameof(Car), request.Id);
            }

            return vm;
        }
    }
}
