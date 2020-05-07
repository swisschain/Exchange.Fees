using System;
using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Fees.WebApi.Models.TradingFee
{
    public class TradingFeeRequestManyModel : PaginationRequest<Guid>
    {
        public string AssetPair { get; set; }
    }
}
