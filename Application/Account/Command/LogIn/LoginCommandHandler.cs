using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Account.Command.LogIn
{
    class LoginCommandHandler : IRequestHandler<LoginCommand,string>
    {
        private readonly ICarDbContext _context;
        private readonly IAuthenticateService _authService;

        public LoginCommandHandler(ICarDbContext context, IAuthenticateService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var identity = await _authService.GetIdentity(request.Email, request.Password, request.RememberMe);
            var token = _authService.GenerateToken(identity);

            return token;
        }
    }
}
