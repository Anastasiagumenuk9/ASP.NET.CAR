using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Account.Command.UpdateAccount
{
    public class UpdateUserCommand : IRequest
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        [Required(ErrorMessage = " ")]
        public string LastName { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = " ")]
        [Remote(action: "CheckMail", controller: "Account", ErrorMessage = "Mail Is Registered")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Uncorrect Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = " ")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "To Short")]
        public string Password { get; set; }

        [Required(ErrorMessage = " ")]
        [Compare("Password", ErrorMessage = "Different Password")]
        public string PasswordConfirm { get; set; }

        public string PhoneNumber { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }
    }
}
