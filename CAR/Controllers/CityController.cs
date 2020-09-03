using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Cities.Queries.GetCitiesList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CAR.Controllers
{
    public class CityController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult<CitiesListVm>> GetCities(Guid id)
        {
            var model = await Mediator.Send(new GetCitiesListQuery(id));

            return View(model);
        }
    }
}
