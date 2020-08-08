using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Application.Common.Exceptions;

namespace Application.Cars.Commands.DeleteCar
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand>
    {
        private readonly ICarDbContext _context;

        public DeleteCarCommandHandler(ICarDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Cars.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Car), request.Id);
            }

            var hasOrders = _context.Cars.Any(od => od.Id == entity.Id);
            if (hasOrders)
            {
                throw new DeleteFailureException(nameof(Car), request.Id, "There are existing orders associated with this car.");
            }

            _context.Cars.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
