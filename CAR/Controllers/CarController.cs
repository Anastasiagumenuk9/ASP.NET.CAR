using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Cars.Queries.GetCarsList;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CAR.Controllers
{
    public class CarController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult<CarsListVm>> GetCars()
        {
            var model = await Mediator.Send(new GetCarsListQuery());

            return View(model);
        }
    }
}
