using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Account.Command.CreateResetPasswordLink
{
    public class CreateResetPasswordLinkCommandHandler : IRequestHandler<CreateResetPasswordLinkCommand>
    {
        private readonly ICarDbContext _context;
        private readonly IUserService _userService;

        public CreateResetPasswordLinkCommandHandler(ICarDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Unit> Handle(CreateResetPasswordLinkCommand request, CancellationToken cancellationToken)
        {
            await _userService.ResetPasswordLinkSender(request.Email);

            return Unit.Value;
        }
    }
}
