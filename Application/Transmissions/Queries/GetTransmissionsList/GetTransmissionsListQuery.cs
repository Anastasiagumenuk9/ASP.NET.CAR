using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Transmissions.Queries.GetTransmissionsList
{
    public class GetTransmissionsListQuery : IRequest<TransmissionsListVm>
    {
    }
}
