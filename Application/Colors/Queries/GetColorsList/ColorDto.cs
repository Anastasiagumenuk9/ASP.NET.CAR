using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Colors.Queries.GetColorsList
{
    public class ColorDto : IMapFrom<Color>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<Color, ColorDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ReverseMap();
        }
    }
}
