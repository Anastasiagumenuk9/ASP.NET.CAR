using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Account.Queries.GetAccountDetails
{
    public class GetAccountDetailQueryHandler : IRequestHandler<GetAccountDetailQuery, AccountDetailVm>
    {
        private readonly ICarDbContext _context;
        private readonly IMapper _mapper;

        public GetAccountDetailQueryHandler(ICarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AccountDetailVm> Handle(GetAccountDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.ApplicationUsers
                 .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.Id);
            }

            return _mapper.Map<AccountDetailVm>(entity);
        }
    }
}
