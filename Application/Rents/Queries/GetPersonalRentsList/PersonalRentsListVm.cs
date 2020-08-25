using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rents.Queries.GetPersonalRentsList
{
    public class PersonalRentsListVm
    {
        public IList<RentDto> Rents { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
