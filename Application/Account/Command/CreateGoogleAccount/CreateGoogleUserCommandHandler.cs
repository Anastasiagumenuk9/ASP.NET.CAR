using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Account.Command.CreateGoogleAccount
{
    public class CreateGoogleUserCommandHandler// : IRequestHandler<CreateGoogleUserCommand, string>
    {
        private readonly ICarDbContext _context;
        private readonly IUserService _userService;

        public CreateGoogleUserCommandHandler(ICarDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        //public async Task<string> Handle(CreateGoogleUserCommand request, CancellationToken cancellationToken)
        //{
            
        //}
    }
}
