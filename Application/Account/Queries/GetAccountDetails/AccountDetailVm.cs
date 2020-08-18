using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Account.Queries.GetAccountDetails
{
    public class AccountDetailVm : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Street{ get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public string PostalCode { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, AccountDetailVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Street, opt => opt.MapFrom(s => s.Street))
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.PasswordHash, opt => opt.MapFrom(s => s.PasswordHash))
            .ForMember(d => d.PostalCode, opt => opt.MapFrom(s => s.PostalCode))
            .ReverseMap();
        }
    }
}
