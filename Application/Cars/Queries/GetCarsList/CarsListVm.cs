using Application.PhotosCar.Queries.GetPhotosList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cars.Queries.GetCarsList
{
    public class CarsListVm
    {
        public IList<CarDto> Cars { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
