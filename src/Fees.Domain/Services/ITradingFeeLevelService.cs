using System.Collections.Generic;
using System.Threading.Tasks;
using Fees.Domain.Entities;

namespace Fees.Domain.Services
{
    public interface ITradingFeeLevelService
    {
        Task<IReadOnlyList<TradingFeeLevel>> GetAllAsync(long tradingFeeId, string brokerId);

        Task<TradingFeeLevel> GetAsync(long id, string brokerId);

        Task<TradingFeeLevel> AddAsync(TradingFeeLevel tradingFeeLevel, string brokerId);

        Task<TradingFeeLevel> UpdateAsync(TradingFeeLevel tradingFeeLevel, string brokerId);

        Task DeleteAsync(long id, string brokerId);
    }
}
