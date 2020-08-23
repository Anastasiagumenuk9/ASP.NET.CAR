using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
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

        public CreateRentCommandHandler(ICarDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateRentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Rent
            {
                IsConfirmed = request.IsConfirmed,
                StartDataRend = request.StartDataRend,
                FinishDataRend = request.FinishDataRend,
                DataConfirmed = request.DataConfirmed,
                Price = request.Price,
                CarId = request.CarId,
                LocationId = request.LocationId,
                ApplicationUserId = request.ApplicationUserId
            };

            _context.Rents.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
