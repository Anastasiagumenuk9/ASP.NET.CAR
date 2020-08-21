using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Transmissions.Queries.GetTransmissionsList
{
    public class TransmissionDto : IMapFrom<Transmission>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<Transmission, TransmissionDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ReverseMap();
        }
    }
}
