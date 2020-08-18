using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CAR.Models;
using MediatR;
using Application.Cars.Queries.GetCarsList;
using Microsoft.Extensions.DependencyInjection;
using Application.Account.Command.CreateAccount;
using Microsoft.AspNetCore.Authorization;

namespace CAR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult<CarsListVm>> GetCars()
        {
            var model = await Mediator.Send(new GetCarsListQuery());

            return Ok(model);
        }

        public async Task<IActionResult> MyAccount()
        {
            return  View();
        }

        [HttpPost]
        public async Task<ActionResult<string>> MyAccount([FromForm]CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
