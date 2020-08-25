using Application.Account.Queries.GetAccountDetails;
using Application.Cars.Queries.GetCarsList;
using Application.CarTypes.Queries.GetCarTypesList;
using Application.Cities.Queries.GetCitiesList;
using Application.Colors.Queries.GetColorsList;
using Application.Locations.Queries.GetLocationsListById;
using Application.PhotosCar.Queries.GetPhotosList;
using Application.Rents.Queries.GetPersonalRentsList;
using Application.Transmissions.Queries.GetTransmissionsList;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            CarDto.Mapping(this);
            AccountDetailVm.Mapping(this);
            PhotoDto.Mapping(this);
            TransmissionDto.Mapping(this);
            CarTypeDto.Mapping(this);
            ColorDto.Mapping(this);
            CityDto.Mapping(this);
            LocationDto.Mapping(this);
            RentDto.Mapping(this);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
