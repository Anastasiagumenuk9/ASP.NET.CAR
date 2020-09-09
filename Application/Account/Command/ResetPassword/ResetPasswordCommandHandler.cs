using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Account.Command.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly ICarDbContext _context;
        private readonly IUserService _userService;

        public ResetPasswordCommandHandler(ICarDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            await _userService.ResetPassword(request.Email, request.Code, request.Password);

            return Unit.Value;
        }
    }
}
