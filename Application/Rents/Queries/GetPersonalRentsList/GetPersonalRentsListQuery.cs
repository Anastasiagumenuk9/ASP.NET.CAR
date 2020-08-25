using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rents.Queries.GetPersonalRentsList
{
    public class GetPersonalRentsListQuery : IRequest<PersonalRentsListVm>
    {
    }
}
