﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cars.Commands.UpdateCar
{
    public class UpdateCarCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ShortDesc { get; set; }

        public ushort Price { get; set; }

        public ushort PriceSecond { get; set; }

        public ushort PriceThird { get; set; }

        public ushort PriceFourth { get; set; }

        public ushort? Run { get; set; }

        public ushort SeetsCount { get; set; }

        public double TankVolume { get; set; }

        public bool Conditioner { get; set; }

        public Guid ColorId { get; set; }

        public Guid CarTypeId { get; set; }

        public Guid TransmissionId { get; set; }
    }
}
