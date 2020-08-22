using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cars.Commands.UpdateCar
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand>
    {
        private readonly ICarDbContext _context;

        public UpdateCarCommandHandler(ICarDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Cars.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Car), request.Id);
            }

            entity.Name = request.Name;
            entity.ShortDesc = request.ShortDesc;
            entity.Price = request.Price;
            entity.PriceSecond = request.PriceSecond;
            entity.PriceThird = request.PriceThird;
            entity.PriceFourth = request.PriceFourth;
            entity.Run = request.Run;
            entity.SeetsCount = request.SeetsCount;
            entity.Conditioner = request.Conditioner;
            entity.TankVolume = request.TankVolume;
            entity.ColorId = request.ColorId;
            entity.CarTypeId = request.CarTypeId;
            entity.TransmissionId = request.TransmissionId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
