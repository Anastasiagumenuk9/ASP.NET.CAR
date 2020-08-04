using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Account.Command.CreateAccount
{
    public class CreateUserCommand : IRequest<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        [Remote(action: "CheckEmail", controller: "Account", ErrorMessage = "Mail is alredy registered")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Uncorrect Address")]
        public string Email { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Different Password")]
        public string PasswordConfirm { get; set; }

        public string PhoneNumber { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        [RegularExpression(@"[0-9]{5}")]
        public string PostalCode { get; set; }
    }
}
