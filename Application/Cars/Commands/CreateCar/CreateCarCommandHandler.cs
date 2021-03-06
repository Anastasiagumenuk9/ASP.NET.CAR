﻿using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cars.Commands.CreateCar
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Guid>
    {
        private readonly ICarDbContext _context; 

        public CreateCarCommandHandler(ICarDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var entity = new Car
            {
                Name = request.Name,
                ShortDesc = request.ShortDesc,
                Price = request.Price,
                PriceSecond = request.PriceSecond,
                PriceThird = request.PriceThird,
                PriceFourth = request.PriceFourth,
                Run = request.Run,
                SeetsCount = request.SeetsCount,
                Available = request.Available,
                Conditioner = request.Conditioner,
                TankVolume = request.TankVolume,
                ColorId = request.ColorId,
                CarTypeId = request.CarTypeId,
                TransmissionId = request.TransmissionId
            };

            _context.Cars.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
            
            return entity.Id;
        }
    }
}
