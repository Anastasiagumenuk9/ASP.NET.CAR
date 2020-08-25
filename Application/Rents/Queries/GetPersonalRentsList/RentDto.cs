using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rents.Queries.GetPersonalRentsList
{
    public class RentDto : IMapFrom<Rent>
    {
        public Guid Id { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime StartDataRend { get; set; }

        public DateTime FinishDataRend { get; set; }

        public DateTime DataConfirmed { get; set; }

        public ushort Price { get; set; }

        public Guid CarId { get; set; }

        public Guid LocationId { get; set; }

        public string ApplicationUserId { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<Rent, RentDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.IsConfirmed, opt => opt.MapFrom(s => s.IsConfirmed))
                .ForMember(d => d.StartDataRend, opt => opt.MapFrom(s => s.StartDataRend))
                .ForMember(d => d.FinishDataRend, opt => opt.MapFrom(s => s.FinishDataRend))
                .ForMember(d => d.DataConfirmed, opt => opt.MapFrom(s => s.DataConfirmed))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                .ForMember(d => d.CarId, opt => opt.MapFrom(s => s.CarId))
                .ForMember(d => d.LocationId, opt => opt.MapFrom(s => s.LocationId))
                .ForMember(d => d.ApplicationUserId, opt => opt.MapFrom(s => s.ApplicationUserId))
                .ReverseMap();
        }
    }
}
