using Application.Cities.Queries.GetCitiesList;
using Application.Locations.Queries.GetLocationsListById;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CitiesLocations.Queries
{
    public class CitiesLocationsListVm
    {
        public IList<CityDto> Cities { get; set; }

        public IList<LocationDto> Locations { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
