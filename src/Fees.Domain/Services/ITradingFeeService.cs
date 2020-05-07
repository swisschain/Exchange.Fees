using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Fees.Domain.Entities;

namespace Fees.Domain.Services
{
    public interface ITradingFeeService
    {
        Task<IReadOnlyList<TradingFee>> GetAllAsync(IEnumerable<string> brokerIds);

        Task<IReadOnlyList<TradingFee>> GetAllAsync(string brokerId, string assetPair,
            ListSortDirection sortOrder = ListSortDirection.Ascending, Guid? cursor = null, int limit = 50);

        Task<TradingFee> GetAsync(Guid id, string brokerId);

        Task<TradingFee> AddAsync(TradingFee tradingFee);

        Task<TradingFee> UpdateAsync(TradingFee tradingFee);

        Task DeleteAsync(Guid id, string brokerId);
    }
}
