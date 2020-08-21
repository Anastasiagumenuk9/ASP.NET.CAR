using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Colors.Queries.GetColorsList
{
    public class ColorsListVm
    {
        public IList<ColorDto> Colors { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
