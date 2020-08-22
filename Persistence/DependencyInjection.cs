using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
               

            services.AddIdentity<ApplicationUser, ApplicationRole>(
                options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<CarDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ICarDbContext>(provider => provider.GetService<CarDbContext>());

            return services;
        }
    }
}
