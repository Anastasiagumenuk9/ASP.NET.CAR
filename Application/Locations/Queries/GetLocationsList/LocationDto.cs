using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Locations.Queries.GetLocationsListById
{
    public class LocationDto : IMapFrom<Location>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CityName { get; set; }

        public Guid CityId { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<Location, LocationDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.CityId, opt => opt.MapFrom(s => s.CityId))
                .ReverseMap();
        }
    }
}
