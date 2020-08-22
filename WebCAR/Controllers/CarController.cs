using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Cars.Queries.GetCarsList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCAR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : BaseController
    {
        //[HttpGet]
        //public async Task<ActionResult<CarsListVm>> GetAll()
        //{
        //    var vm = await Mediator.Send(new GetCarsListQuery());

        //    return base.Ok(vm);
        //}
    }
}