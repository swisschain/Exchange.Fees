using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Fees.WebApi.Models.Audit.CashOperationsFee
{
    public class CashOperationsFeeHistoryRequestManyModel : PaginationRequest<long>
    {
        public long? CashOperationFeeId { get; set; }

        public string UserId { get; set; }

        public string Asset { get; set; }
    }
}
