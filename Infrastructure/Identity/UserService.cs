using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, Password);

                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, "User");

                    if (!resultRole.Succeeded)
                    {
                        throw new Exception("Exception added roles!");
                    }
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
    }
}
