using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Cars.Queries.GetCarsList;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Common.Pagination;
using Persistence;
using Microsoft.AspNetCore.Authorization;
using Application.Cars.Queries.GetCarDetail;

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

        public async Task<ActionResult<CarsListVm>> GetCars(int page = 1)
        {
            var model = await Mediator.Send(new GetCarsListQuery(page,3));

            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult<CarDetailVm>> GetCarById(Guid id)
        {
            var vm = await Mediator.Send(new GetCarDetailQuery { Id = id });

            return base.Ok(vm);
        }
    }
}
