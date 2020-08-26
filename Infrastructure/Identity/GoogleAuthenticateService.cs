using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class GoogleAuthenticateService : IGoogleAuthenticateService
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly TokenAuthenticationService _tokenAuthenticationService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public GoogleAuthenticateService(TokenAuthenticationService tokenAuthenticationService, UserManager<ApplicationUser> userManager, IAuthenticateService authenticateService, SignInManager<ApplicationUser> signInManager)
        {
            _tokenAuthenticationService = tokenAuthenticationService;
            _userManager = userManager;
            _authenticateService = authenticateService;
            _signInManager = signInManager;
        }

        public async Task<string> SignInGoogle(AuthenticateResult authResult)
        {
            var claimsGoogle = authResult.Principal.Identities
               .FirstOrDefault().Claims.Select(claim => new
               {
                   claim.Issuer,
                   claim.OriginalIssuer,
                   claim.Type,
                   claim.Value
               });

            var mail = claimsGoogle.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();

            var user = await _userManager.FindByEmailAsync(mail);

            if (user == null)
            {
                throw new Exception("The user not found");
            }

            var result = _signInManager.SignInAsync(user, false);

            if (result.IsCompletedSuccessfully)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                if (!userRoles.Any() || userRoles.Count > 1)
                {
                    throw new Exception("Incorect user roles( 0 or more then 1)");
                }

                var userRole = userRoles.Single();

                var claims = new List<Claim>
                {
                    new Claim("name", user.UserName),
                    new Claim("role",userRole),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                };

                var claimIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                                                       ClaimsIdentity.DefaultRoleClaimType);


                var token = _authenticateService.GenerateToken(claimIdentity);

                return token;
            }

            throw new Exception("Error");
        }
    }
}
