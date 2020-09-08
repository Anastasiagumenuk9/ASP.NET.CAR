using Application.Cars.Queries.GetCarDetail;
using Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Rents.Commands.CreateRent
{
    public class CreateRentCommandHandler : IRequestHandler<CreateRentCommand, Guid>
    {
        private readonly ICarDbContext _context;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateRentCommandHandler(ICarDbContext context, IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _emailService = emailService;
            _userManager = userManager;
        }

        public async Task<Guid> Handle(CreateRentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Rent
            {
                IsConfirmed = true,
                StartDataRend = request.StartDataRend,
                FinishDataRend = request.FinishDataRend,
                DataConfirmed = DateTime.Now,
                Price = request.Price,
                CarId = request.CarId,
                LocationId = request.LocationId,
                ApplicationUserId = request.ApplicationUserId
            };

            _context.Rents.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            var user = await _userManager.FindByIdAsync(request.ApplicationUserId);
            var car = await _context.Cars.FirstAsync(c => c.Id == request.CarId);

            _emailService.SendEmailAsync(user.Email, "Informtion About Rent", 
                $"Thank you for rent! Your car is {car.Name}.Start date of rent is {request.StartDataRend}.Finish date of rent is {request.StartDataRend} .The cost of rent is {request.Price}$.");

            return entity.Id;
        }
    }
}
