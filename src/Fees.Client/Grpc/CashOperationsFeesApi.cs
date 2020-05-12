using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swisschain.Exchange.Fees.Client.Api;
using Swisschain.Exchange.Fees.Client.Common;
using Swisschain.Exchange.Fees.Client.Models.CashOperationsFees;
using Swisschain.Exchange.Fees.Contract;

namespace Swisschain.Exchange.Fees.Client.Grpc
{
    internal class CashOperationsFeesApi : BaseGrpcClient, ICashOperationsFeesApi
    {
        private readonly CashOperationsFees.CashOperationsFeesClient _client;

        public CashOperationsFeesApi(string address) : base(address)
        {
            _client = new CashOperationsFees.CashOperationsFeesClient(Channel);
        }

        public async Task<IReadOnlyList<CashOperationsFeeModel>> GetAllByBrokerId(string brokerId)
        {
            var response = await _client.GetAllByBrokerIdAsync(new GetAllCashOperationsFeesByBrokerIdRequest { BrokerId = brokerId });

            var result = response.CashOperationsFees
                .Select(cashOperationsFee => new CashOperationsFeeModel(cashOperationsFee))
                .ToList();

            return result;
        }

        public async Task<CashOperationsFeeModel> GetByBrokerIdAndAsset(string brokerId, string asset)
        {
            var response = await _client.GetByBrokerIdAndAssetAsync(new GetCashOperationsFeeByBrokerIdAndAssetRequest { BrokerId = brokerId, Asset = asset });

            if (response.CashOperationsFee == null)
                return null;

            var result = new CashOperationsFeeModel(response.CashOperationsFee);

            return result;
        }
    }
}
