using System;
using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Fees.WebApi.Models.CashOperationsFee
{
    public class CashOperationsFeeRequestManyModel : PaginationRequest<Guid>
    {
        public string Asset { get; set; }
    }
}
