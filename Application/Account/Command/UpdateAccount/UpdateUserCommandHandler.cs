using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Account.Command.UpdateAccount
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly ICarDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserCommandHandler(ICarDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userManager.FindByEmailAsync(request.Email);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.Id);
            }

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.Email = request.Email;
            entity.City = request.City;
            entity.Street = request.Street;
            entity.PostalCode = request.PostalCode;
            entity.PhoneNumber = request.PhoneNumber;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
