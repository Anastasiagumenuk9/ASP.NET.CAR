using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.PhotosCar.Queries.GetPhotosList
{
    public class PhotoDto : IMapFrom<PhotoCar>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Photo { get; set; }

        public Guid CarId { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<PhotoCar, PhotoDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Photo, opt => opt.MapFrom(s => s.Photo))
                .ForMember(d => d.CarId, opt => opt.MapFrom(s => s.CarId))              
                .ReverseMap();
        }
    }
}
