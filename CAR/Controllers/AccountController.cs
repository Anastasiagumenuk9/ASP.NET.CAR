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
using Application.Account.Queries.GetAccountDetails;
using IdentityServer4.Extensions;
using Application.Account.Command.UpdateAccount;
using CAR.Models;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CAR.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _applicationUserService;
        private readonly IAuthenticateService _authenticateService;
        private readonly IGoogleAuthenticateService _googleAuthenticateService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ICarDbContext _carDbContext;

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [TempData]
        public string ErrorMessage { get; set; }

        public AccountController(IAuthenticateService authenticateService, UserManager<ApplicationUser> userManager, ICurrentUserService applicationUserService, IGoogleAuthenticateService googleAuthenticateService, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, ICarDbContext carDbContext)
        {
            _authenticateService = authenticateService;
            _userManager = userManager;
            _applicationUserService = applicationUserService;
            _googleAuthenticateService = googleAuthenticateService;
            _signInManager = signInManager;
            _logger = logger;
            _carDbContext = carDbContext;
        }

        [Authorize]
        public async Task<ActionResult<AccountDetailVm>> AccountPage()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = await Mediator.Send(new GetAccountDetailQuery {Id = user.Id});

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AccountDetailVm>> AccountSettings()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = await Mediator.Send(new GetAccountDetailQuery { Id = user.Id });

            return View(model);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> AccountSettings([FromForm] UpdateUserCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok();
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

        public IActionResult EmailVerification() => View();

        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult<AccountDetailVm>> GetAccountDetail()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = await Mediator.Send(new GetAccountDetailQuery { Id = user.Id });

            return View(model);
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

        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            var token = await  _googleAuthenticateService.SignInGoogle(result);
            if (result != null)
            {
                HttpContext.Session.SetString("JWToken", token);
            }
            return RedirectToAction("AccountPage", "Account");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm]LoginCommand command)
        {
            var result = await Mediator.Send(command);

            if (result != null)
            {
                HttpContext.Session.SetString("JWToken", result);
            }

            return RedirectToAction("AccountPage", "Account", false);
        }

        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return Redirect("~/Home/Index");
        }

        public  IActionResult Register()
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