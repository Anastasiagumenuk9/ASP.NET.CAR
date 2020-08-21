using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Transmissions.Queries.GetTransmissionsList
{
    public class TransmissionsListVm
    {
        public IList<TransmissionDto> Transmissions { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
