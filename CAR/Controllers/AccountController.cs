using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Account.Command.CreateAccount;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CAR.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _applicationUserService;

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public AccountController(UserManager<ApplicationUser> userManager, ICurrentUserService applicationUserService)
        {
            _userManager = userManager;
            _applicationUserService = applicationUserService;
        }

        [AcceptVerbs("Get", "Post")]
        public async Task <IActionResult> CheckEmail(string email)
        {
            if (await _userManager.FindByEmailAsync(email) == null)
            {
                return Json(true);
            }  
            else
            {
                return Json(false);
            }
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<string>> Register([FromForm]CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}