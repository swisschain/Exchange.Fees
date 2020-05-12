using System.Collections.Generic;
using System.Threading.Tasks;
using Swisschain.Exchange.Fees.Client.Models.CashOperationsFees;

namespace Swisschain.Exchange.Fees.Client.Api
{
    public interface ICashOperationsFeesApi
    {
        Task<IReadOnlyList<CashOperationsFeeModel>> GetAllByBrokerId(string brokerId);

        Task<CashOperationsFeeModel> GetByBrokerIdAndAsset(string brokerId, string asset);
    }
}
