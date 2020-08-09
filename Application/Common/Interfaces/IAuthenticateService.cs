using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAuthenticateService
    {
        string GenerateToken(ClaimsIdentity identity);
        Task<ClaimsIdentity> GetIdentity(string email, string password, bool rememberMe);
    }
}
