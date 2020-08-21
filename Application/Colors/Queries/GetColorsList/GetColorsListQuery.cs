using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Colors.Queries.GetColorsList
{
    public class GetColorsListQuery : IRequest<ColorsListVm>
    {
    }
}
