using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Rents.Commands.CreateRent;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> AddRent()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddRent([FromForm] CreateRentCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
