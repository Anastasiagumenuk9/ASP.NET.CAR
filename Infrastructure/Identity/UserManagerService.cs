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
    public class UserManagerService : IUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string mail, string password)
        {
            if (await _userManager.FindByNameAsync(mail) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = userName,
                    Email = mail,
                };

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, "User");

                    if (!resultRole.Succeeded)
                    {
                        throw new Exception("Exception added roles!");
                    }
                }

                user.PasswordHash = user.PasswordHash;

                return (result.ToApplicationResult(), user.Id);
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
