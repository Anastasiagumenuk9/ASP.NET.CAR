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

            var rents = await _context.Rents.Where(c => c.ApplicationUserId == userId)
               .ProjectTo<RentDto>(_mapper.ConfigurationProvider)
               .OrderBy(p => p.DataConfirmed)
               .ToListAsync(cancellationToken);

            var vm = new PersonalRentsListVm
            {
                Rents = rents,
                CreateEnabled = true
            };

            return vm;
        }
    }
}
