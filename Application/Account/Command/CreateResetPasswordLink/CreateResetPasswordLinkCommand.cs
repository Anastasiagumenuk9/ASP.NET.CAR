using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Account.Command.CreateResetPasswordLink
{
    public class CreateResetPasswordLinkCommand : IRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
