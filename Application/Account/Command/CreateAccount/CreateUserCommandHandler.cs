using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Account.Command.CreateAccount
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly ICarDbContext _context;
        private readonly IUserService _userService;

        public CreateUserCommandHandler(ICarDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.CreateUserAsync(request.FirstName, request.LastName, request.Email,
                                                request.PhoneNumber, request.Street, request.Password,
                                                request.City, request.PostalCode);
        }
    }
}
