using Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CarTypes.Queries.GetCarTypesList
{
    public class CarTypesListVm
    {
        public IList<CarTypeDto> CarTypes { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
