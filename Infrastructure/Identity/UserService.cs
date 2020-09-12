using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http;
using System.Web.Mvc;
using System.Web;
using Microsoft.Net.Http.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using MailKit;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly LinkGenerator _generator;

        public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor, IUrlHelperFactory urlHelperFactory, LinkGenerator generator, IActionContextAccessor actionContextAccessor)
        {
            _userManager = userManager;
            _accessor = accessor;
            _urlHelperFactory = urlHelperFactory;
            _generator = generator;
            _actionContextAccessor = actionContextAccessor;
        }

        private static bool ByteArraysEqual(byte[] b0, byte[] b1)
        {
            if (b0 == b1)
            {
                return true;
            }

            if (b0 == null || b1 == null)
            {
                return false;
            }

            if (b0.Length != b1.Length)
            {
                return false;
            }

            for (int i = 0; i < b0.Length; i++)
            {
                if (b0[i] != b1[i])
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<string> CreateGoogleUserAsync(AuthenticateResult authResult)
        {
            var claimsGoogle = authResult.Principal.Identities
               .FirstOrDefault().Claims.Select(claim => new
               {
                   claim.Issuer,
                   claim.OriginalIssuer,
                   claim.Type,
                   claim.Value
               });

            var Email = claimsGoogle.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).FirstOrDefault();

            var UserName = Email.Substring(0, Email.IndexOf('@'));
            var FirstName = claimsGoogle.Where(c => c.Type == ClaimTypes.Name)
                   .Select(c => c.Value).FirstOrDefault();
            var LastName = claimsGoogle.Where(c => c.Type == ClaimTypes.Surname)
                   .Select(c => c.Value).FirstOrDefault();
            var PhoneNumber = claimsGoogle.Where(c => c.Type == ClaimTypes.MobilePhone)
                   .Select(c => c.Value).FirstOrDefault();
            var PaswordHash = claimsGoogle.Where(c => c.Type == ClaimTypes.Hash)
                   .Select(c => c.Value).FirstOrDefault();

            return await CreateUserAsync(FirstName, LastName, Email, PhoneNumber, "", "", "", "");
        }

        public async Task<string> CreateUserAsync(string FirstName, string LastName, string Email,
                                                  string PhoneNumber, string Street, string Password,
                                                  string City, string PostalCode)
        {
            if (await _userManager.FindByNameAsync(Email) == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = FirstName.Substring(0, 1).ToUpper() + FirstName.Substring(1).ToLower(),
                    LastName = LastName.Substring(0, 1).ToUpper() + LastName.Substring(1).ToLower(),
                    UserName = Email.Substring(0, Email.IndexOf('@')),
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    Street = Street.Substring(0, 1).ToUpper() + Street.Substring(1).ToLower(),
                    City = City.Substring(0, 1).ToUpper() + City.Substring(1).ToLower(),
                    PostalCode = PostalCode,
                    SecurityStamp = new Guid().ToString(),
                };

                user.PasswordHash = HashPassword(Password);
                var result = await _userManager.CreateAsync(user, Password);

                if (result.Succeeded)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, "User");

                    if (!resultRole.Succeeded)
                    {
                        throw new Exception("Exception added roles!");
                    }

                    string confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;

                    var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

                    string confirmationLink = urlHelper.Action("ConfirmEmail",
                    "Account", new
                    {
                        userid = user.Id,
                        token = confirmationToken
                    },
                    protocol: _accessor.HttpContext.Request.Scheme);

                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(user.Email, "Confirm your account",
                        $"Confirm registration, following the <a href='{confirmationLink}'>link</a>");
                }

                return user.Id;
            }
            else
            {
                throw new Exception("User with this mail are registered!");
            }
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }


        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                 throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return  Convert.ToBase64String(dst);
        }

        public async Task ResetPassword(string email, string code, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("The user not found");
            }

            await _userManager.ResetPasswordAsync(user, code, password);
        }

        public async Task ResetPasswordLinkSender(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                throw new Exception("User not found");
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
            
            string confirmationLink = urlHelper.Action("ResetPassword",
                    "Account", new
                    {
                        Email = user.Email,
                        Code = code
                    },
                    protocol: _accessor.HttpContext.Request.Scheme);

            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(email, "Reset Password",
                $"Follow the <a href='{confirmationLink }'>link</a> to reset password.");
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }
    }
}
