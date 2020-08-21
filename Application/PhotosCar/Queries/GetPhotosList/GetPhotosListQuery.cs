using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.PhotosCar.Queries.GetPhotosList
{
    public class GetPhotosListQuery : IRequest<PhotosListVm>
    {
    }
}
