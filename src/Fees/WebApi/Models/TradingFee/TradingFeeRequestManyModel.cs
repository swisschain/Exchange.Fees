using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Fees.WebApi.Models.TradingFee
{
    public class TradingFeeRequestManyModel : PaginationRequest<long>
    {
        public string AssetPair { get; set; }
    }
}
