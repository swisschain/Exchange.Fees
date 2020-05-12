using System.Collections.Generic;
using System.Threading.Tasks;
using Swisschain.Exchange.Fees.Client.Models.TradingFees;

namespace Swisschain.Exchange.Fees.Client.Api
{
    public interface ITradingFeesApi
    {
        Task<IReadOnlyList<TradingFeeModel>> GetAllByBrokerId(string brokerId);

        Task<TradingFeeModel> GetByBrokerIdAndAssetPair(string brokerId, string assetPair);
    }
}
