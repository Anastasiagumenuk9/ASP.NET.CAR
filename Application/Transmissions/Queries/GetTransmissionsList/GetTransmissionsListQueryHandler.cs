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

namespace Application.Transmissions.Queries.GetTransmissionsList
{
    public class GetTransmissionsListQueryHandler : IRequestHandler<GetTransmissionsListQuery, TransmissionsListVm>
    {
        private readonly ICarDbContext _context;
        private readonly IMapper _mapper;

        public GetTransmissionsListQueryHandler(ICarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TransmissionsListVm> Handle(GetTransmissionsListQuery request, CancellationToken cancellationToken)
        {
            var transmissions = await _context.Transmissions
              .ProjectTo<TransmissionDto>(_mapper.ConfigurationProvider)
              .OrderBy(p => p.Name)
              .ToListAsync(cancellationToken);

            var vm = new TransmissionsListVm
            {
                Transmissions = transmissions,
                CreateEnabled = true
            };

            return vm;
        }
    }
}
