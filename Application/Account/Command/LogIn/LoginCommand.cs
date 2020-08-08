using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Account.Command.LogIn
{
    public class LoginCommand : IRequest<string>
    {
        [Required(ErrorMessage = " ")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Uncorrect Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = " ")]
        public string Password { get; set; }
    }
}
