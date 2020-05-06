using System;
using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Fees.WebApi.Models.CashOperationsFee
{
    public class TradingFeeRequestManyModel : PaginationRequest<Guid>
    {
        public string Asset { get; set; }
    }
}
