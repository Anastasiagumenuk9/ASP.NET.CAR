using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Rents.Commands.CreateRent;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Cities.Queries.GetCitiesList;
using Application.Locations.Queries.GetLocationsListById;
using Application.CitiesLocations.Queries;

namespace CAR.Controllers
{
    public class RentController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult<CitiesLocationsListVm>> AddRent()
        {
            var model = await Mediator.Send(new GetCitiesLocationsListQuery());

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddRent([FromForm] CreateRentCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
