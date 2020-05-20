using System;
using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Fees.WebApi.Models.CashOperationsFee
{
    public class CashOperationsFeeRequestManyModel : PaginationRequest<long>
    {
        public string Asset { get; set; }
    }
}
