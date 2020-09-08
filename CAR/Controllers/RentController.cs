using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Rents.Commands.CreateRent;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Locations.Queries.GetLocationsListById;
using Application.CitiesLocations.Queries;
using Application.Rents.Queries.GetPersonalRentsList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.Common.Interfaces;
using System.Runtime.CompilerServices;
using Domain.Entities;
using Application.Cities.Queries.GetCitiesList;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CitiesListVm>> AddRent(Guid id)
        {
            var model = await Mediator.Send(new GetCitiesListQuery(id));

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> AddRent(CreateRentCommand command)
        {
            return await Mediator.Send(command);        
        }

        public async Task<ActionResult<PersonalRentsListVm>> GetPersonalRents()
        {
            var model = await Mediator.Send(new GetPersonalRentsListQuery());

            return View(model);
        }
    }
}
