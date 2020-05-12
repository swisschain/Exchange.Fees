using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swisschain.Exchange.Fees.Client.Api;
using Swisschain.Exchange.Fees.Client.Common;
using Swisschain.Exchange.Fees.Client.Models.TradingFees;
using Swisschain.Exchange.Fees.Contract;

namespace Swisschain.Exchange.Fees.Client.Grpc
{
    internal class TradingFeesApi : BaseGrpcClient, ITradingFeesApi
    {
        private readonly TradingFees.TradingFeesClient _client;

        public TradingFeesApi(string address) : base(address)
        {
            _client = new TradingFees.TradingFeesClient(Channel);
        }

        public async Task<IReadOnlyList<TradingFeeModel>> GetAllByBrokerId(string brokerId)
        {
            var response = await _client.GetAllByBrokerIdAsync(new GetAllTradingFeesByBrokerIdRequest { BrokerId = brokerId });

            var result = response.TradingFees
                .Select(tradingFee => new TradingFeeModel(tradingFee,
                    tradingFee.Levels.Select(x => new TradingFeeLevel(x))))
                .ToList();

            return result;
        }

        public async Task<TradingFeeModel> GetByBrokerIdAndAssetPair(string brokerId, string assetPair)
        {
            var response = await _client.GetByBrokerIdAndAssetPairAsync(new GetTradingFeeByBrokerIdAndAssetPairRequest { BrokerId = brokerId, AssetPair = assetPair });

            if (response.TradingFee == null)
                return null;

            var result = new TradingFeeModel(response.TradingFee,
                response.TradingFee.Levels.Select(x => new TradingFeeLevel(x)));

            return result;
        }
    }
}
