using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fees.Domain.Entities;

namespace Fees.Domain.Services
{
    public interface ITradingFeeLevelService
    {
        Task<IReadOnlyList<TradingFeeLevel>> GetAllAsync(Guid tradingFeeId, string brokerId);

        Task<TradingFeeLevel> GetAsync(Guid id, string brokerId);

        Task<TradingFeeLevel> AddAsync(TradingFeeLevel tradingFeeLevel, string brokerId);

        Task<TradingFeeLevel> UpdateAsync(TradingFeeLevel tradingFeeLevel, string brokerId);

        Task DeleteAsync(Guid id, string brokerId);
    }
}
