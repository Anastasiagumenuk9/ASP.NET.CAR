using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Cars.Queries.GetCarsList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAR.Controllers
{
    public class CarController : BaseController
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<CarsListVm>> GetAll()
        {
            var vm = await Mediator.Send(new GetCarsListQuery());

            return base.Ok(vm);
        }
    }
}