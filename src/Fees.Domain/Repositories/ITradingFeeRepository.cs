using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Fees.Domain.Entities;

namespace Fees.Domain.Repositories
{
    public interface ITradingFeeRepository
    {
        Task<IReadOnlyList<TradingFee>> GetAllAsync(IEnumerable<string> brokerIds);

        Task<IReadOnlyList<TradingFee>> GetAllAsync(string brokerId);

        Task<IReadOnlyList<TradingFee>> GetAllAsync(string brokerId, string assetPair,
            ListSortDirection sortOrder = ListSortDirection.Ascending, long cursor = 0, int limit = 50);

        Task<TradingFee> GetAsync(long id, string brokerId);

        Task<TradingFee> GetAsync(string brokerId, string assetPair);

        Task<TradingFee> InsertAsync(TradingFee tradingFee);

        Task<TradingFee> UpdateAsync(TradingFee tradingFee);

        Task DeleteAsync(long id, string brokerId);
    }
}
