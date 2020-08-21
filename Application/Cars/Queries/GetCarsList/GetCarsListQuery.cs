using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cars.Queries.GetCarsList
{
    public class GetCarsListQuery : IRequest<CarsListVm>
    {
        public int _page { get; set; }
        public int _pageSize { get; set; }

        public GetCarsListQuery(int page, int pageSize)
        {
            _page = page;
            _pageSize = pageSize;
        }
    }
}
