using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Locations.Queries.GetLocationsListById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CAR.Controllers
{
    public class LocationController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task <LocationsListVm> GetLocations()
        {
            var model = await Mediator.Send(new GetLocationsListQuery());

            return model;
        }
    }
}
