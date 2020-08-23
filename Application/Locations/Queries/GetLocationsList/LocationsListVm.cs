using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Locations.Queries.GetLocationsListById
{
    public class LocationsListVm
    {
        public IList<LocationDto> Locations { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
