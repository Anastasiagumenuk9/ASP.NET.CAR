using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.PhotosCar.Queries.GetPhotosList
{
    public class GetPhotosListQueryHandler : IRequestHandler<GetPhotosListQuery, PhotosListVm>
    {
        private readonly ICarDbContext _context;
        private readonly IMapper _mapper;

        public GetPhotosListQueryHandler(ICarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PhotosListVm> Handle(GetPhotosListQuery request, CancellationToken cancellationToken)
        {
            var photos = await _context.PhotosCar
                .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            var vm = new PhotosListVm
            {
                PhotosCar = photos,
                CreateEnabled = true // TODO: Set based on user permissions.
            };

            return vm;
        }
    }
}
