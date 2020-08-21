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

namespace Application.Colors.Queries.GetColorsList
{
    public class GetColorsListQueryHandler : IRequestHandler<GetColorsListQuery, ColorsListVm>
    {
        private readonly ICarDbContext _context;
        private readonly IMapper _mapper;

        public GetColorsListQueryHandler(ICarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ColorsListVm> Handle(GetColorsListQuery request, CancellationToken cancellationToken)
        {
            var colors = await _context.Colors
              .ProjectTo<ColorDto>(_mapper.ConfigurationProvider)
              .OrderBy(p => p.Name)
              .ToListAsync(cancellationToken);

            var vm = new ColorsListVm
            {
                Colors = colors,
                CreateEnabled = true
            };

            return vm;
        }
    }
}
