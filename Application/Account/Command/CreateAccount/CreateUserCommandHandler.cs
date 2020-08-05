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
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateUserCommandHandler(ICarDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByEmailAsync(request.Email) == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = request.FirstName.Substring(0, 1).ToUpper() + request.FirstName.Substring(1).ToLower(),
                    LastName = request.LastName.Substring(0, 1).ToUpper() + request.LastName.Substring(1).ToLower(),
                    UserName = request.Email.Substring(0, request.Email.IndexOf('@')),
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Street = request.Street.Substring(0, 1).ToUpper() + request.Street.Substring(1).ToLower(),
                    City = request.City.Substring(0, 1).ToUpper() + request.City.Substring(1).ToLower(),
                    PostalCode = request.PostalCode,
                    SecurityStamp = new Guid().ToString(),  
                };

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);

                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, "User");

                    if (!resultRole.Succeeded)
                    {
                        throw new Exception("Exception added roles!");
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);

                return user.Id;
            }
            else
            {
                return "A user with this mail are alredy exist!";
            }
        }
    }
}
