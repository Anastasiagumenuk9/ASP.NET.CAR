using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Account.Queries.GetAccountDetails
{
    public class GetAccountDetailQuery : IRequest<AccountDetailVm>
    {
        public string Id { get; set; }
    }
}
