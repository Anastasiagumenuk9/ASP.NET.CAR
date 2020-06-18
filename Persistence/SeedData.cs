using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class SeedData
    {
        public static async Task Initialize(CarDbContext context,
                                          UserManager<ApplicationUser> userManager,
                                          RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            ApplicationRole Admin = await CreateApplicationRole(roleManager, "Admin", "Has access to data", DateTime.Now);
            ApplicationRole Moderator = await CreateApplicationRole(roleManager, "Moderator", "Has access to data", DateTime.Now);
            ApplicationRole User = await CreateApplicationRole(roleManager, "User", "Has oportunity to rent car", DateTime.Now);
            ApplicationUser MainAdmin = await CreateApplicationUser(userManager, "Admin1", "Admin@gmail.com", "0967322916", "11111", "Admin");
        }

        private static async Task<ApplicationRole> CreateApplicationRole(RoleManager<ApplicationRole> roleManager, string role, string desc, DateTime date)
        {
            if (await roleManager.FindByNameAsync(role) == null)
            {
                var Role = new ApplicationRole(role, desc, date);
                await roleManager.CreateAsync(Role);
                return Role;
            }
            else
            {
                return await roleManager.FindByNameAsync(role);
            }
        }

        private static async Task<ApplicationUser> CreateApplicationUser(UserManager<ApplicationUser> userManager, string UserName, string email, string phoneNumber, string password, string role)
        {
            if (await userManager.FindByNameAsync(email) == null)
            {
                var user = new ApplicationUser() { UserName = UserName, Email = email, PhoneNumber = phoneNumber, PasswordHash = password.GetHashCode().ToString() };
                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role);
                }

                return user;
            }
            else
            {
                return await userManager.FindByNameAsync(email);
            }
        }
    }
}
