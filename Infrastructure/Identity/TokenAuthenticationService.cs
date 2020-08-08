using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly TokenManagement _tokenManagement;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TokenAuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<TokenManagement> tokenManagement)
        {
            _tokenManagement = tokenManagement.Value;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string GenerateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );

            var tokenjwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return $"Bearer {tokenjwt}";
        }

        public async Task<ClaimsIdentity> GetIdentity(string Email, string password)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                throw new Exception("The user not found");
            }

            //TODO PasswordSignInAsync don't work!
            //var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, lockoutOnFailure: false);

            if (user.Email == Email && UserService.VerifyHashedPassword(user.PasswordHash, password))
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
                return claimIdentity;
            }

            return new ClaimsIdentity();
        }
    }
}
