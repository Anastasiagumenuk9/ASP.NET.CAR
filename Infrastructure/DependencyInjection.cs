

using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGoogleAuthenticateService, GoogleAuthenticateService>();
            services.AddTransient<IAuthenticateService, TokenAuthenticationService>();
            services.AddTransient<IDateTime, MachineDateTime>();

            return services;
        }
    }
}
