using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(string FirstName, string LastName, string Email,
                                     string PhoneNumber, string Street, string Password,
                                     string City, string PostalCode);

        Task<Result> DeleteUserAsync(string userId);

        Task ResetPassword(string email, string code, string password);
        Task ResetPasswordLinkSender(string email);
    }
}
