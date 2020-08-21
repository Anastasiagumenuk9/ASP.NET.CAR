using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CarTypes.Queries.GetCarTypesList
{
    public class CarTypeDto : IMapFrom<CarType>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<CarType, CarTypeDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ReverseMap();
        }
    }
}
