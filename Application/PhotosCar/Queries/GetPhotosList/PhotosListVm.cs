using System;
using System.Collections.Generic;
using System.Text;

namespace Application.PhotosCar.Queries.GetPhotosList
{
    public class PhotosListVm
    {
        public IList<PhotoDto> PhotosCar { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
