using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Fees.Domain.Entities;

namespace Fees.Domain.Repositories
{
    public interface ICashOperationsFeeHistoryRepository
    {
        Task<IReadOnlyList<CashOperationsFeeHistory>> GetAllAsync(Guid? cashOperationFeeId, string brokerId, string userId, string asset,
            ListSortDirection sortOrder = ListSortDirection.Ascending, Guid? cursor = null, int limit = 50);

        Task<CashOperationsFeeHistory> InsertAsync(CashOperationsFeeHistory cashOperationsFeeHistory);
    }
}
