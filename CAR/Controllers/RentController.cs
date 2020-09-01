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
using Application.Rents.Queries.GetPersonalRentsList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.Common.Interfaces;
using System.Runtime.CompilerServices;
using Domain.Entities;

namespace CAR.Controllers
{
    public class RentController : Controller
    {
        private IMediator _mediator;
        private ICarDbContext db;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public IActionResult Index()
        {
            return View();
        }

        public RentController(ICarDbContext _db)
        {
            db = _db;
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

        public JsonResult GetMembers(string id)
        {
            var locations = db.Locations.ToList();
            var cities = db.Cities.ToList();

            var result = locations.Join(cities,
                t => t.CityId,
                p => p.Id,
                (t, p) => new LocationDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    CityId = t.CityId,
                    CityName = p.Name,

                });

            return Json(new SelectList(result.Where(c => c.CityName == id), "Id", "Name"));

        }

        public async Task<ActionResult<PersonalRentsListVm>> GetPersonalRents()
        {
            var model = await Mediator.Send(new GetPersonalRentsListQuery());

            return View(model);
        }
    }
}
