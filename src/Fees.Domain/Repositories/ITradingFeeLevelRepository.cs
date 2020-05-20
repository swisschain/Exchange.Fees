using System.Collections.Generic;
using System.Threading.Tasks;
using Fees.Domain.Entities;

namespace Fees.Domain.Repositories
{
    public interface ITradingFeeLevelRepository
    {
        Task<IReadOnlyList<TradingFeeLevel>> GetAllAsync(long tradingFeeId, string brokerId);

        Task<TradingFeeLevel> GetAsync(long id, string brokerId);

        Task<TradingFeeLevel> InsertAsync(TradingFeeLevel tradingFeeLevel, string brokerId);

        Task<TradingFeeLevel> UpdateAsync(TradingFeeLevel tradingFeeLevel, string brokerId);

        Task DeleteAsync(long id, string brokerId);
    }
}
