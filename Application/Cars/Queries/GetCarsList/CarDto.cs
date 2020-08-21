using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Cars.Queries.GetCarsList
{
    public class CarDto : IMapFrom<Car>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ShortDesc { get; set; }

        public ushort Price { get; set; }

        public ushort? Run { get; set; }

        public ushort SeetsCount { get; set; }

        public bool Available { get; set; }

        public bool Conditioner { get; set; }

        public Guid ColorId { get; set; }

        public Guid CarTypeId { get; set; }

        public Guid TransmissionId { get; set; }

        public byte []Photo { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<Car, CarDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.ShortDesc, opt => opt.MapFrom(s => s.ShortDesc))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                .ForMember(d => d.Run, opt => opt.MapFrom(s => s.Run))
                .ForMember(d => d.SeetsCount, opt => opt.MapFrom(s => s.SeetsCount))
                .ForMember(d => d.Available, opt => opt.MapFrom(s => s.Available))
                .ForMember(d => d.Conditioner, opt => opt.MapFrom(s => s.Conditioner))
                .ForMember(d => d.ColorId, opt => opt.MapFrom(s => s.ColorId))
                .ForMember(d => d.CarTypeId, opt => opt.MapFrom(s => s.CarTypeId))
                .ForMember(d => d.TransmissionId, opt => opt.MapFrom(s => s.TransmissionId))
                .ForSourceMember(x => x.Created, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.CreatedBy, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.LastModified, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.LastModifiedBy, opt => opt.DoNotValidate())
                .ReverseMap();
        }
    }
}
