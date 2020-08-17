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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Application.Account.Command.LogIn;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace CAR.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _applicationUserService;
        private readonly IAuthenticateService _authenticateService;

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public AccountController(IAuthenticateService authenticateService, UserManager<ApplicationUser> userManager, ICurrentUserService applicationUserService)
        {
            _authenticateService = authenticateService;
            _userManager = userManager;
            _applicationUserService = applicationUserService;
        }

        [AcceptVerbs("Get", "Post")]
        public async Task <IActionResult> CheckMail(string email)
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

        [AllowAnonymous]
        [HttpPost("GetToken")]
        public async Task GetToken(string email, string password, bool rememberMe)
        {
            var identity = await _authenticateService.GetIdentity(email, password, rememberMe);

            var token = _authenticateService.GenerateToken(identity);

            await Response.WriteAsync(JsonConvert.SerializeObject("Token : " + token,
                new JsonSerializerSettings { Formatting = Formatting.Indented }
            ));
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm]LoginCommand command)
        {
            var identity = await _authenticateService.GetIdentity(command.Email, command.Password, command.RememberMe);
            var token = _authenticateService.GenerateToken(identity);

            if (token != null)
            {
                HttpContext.Session.SetString("JWToken", token);
            }

            return RedirectToAction("Index", "Home");
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