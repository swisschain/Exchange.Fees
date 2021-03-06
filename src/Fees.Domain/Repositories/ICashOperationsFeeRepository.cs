﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Fees.Domain.Entities;

namespace Fees.Domain.Repositories
{
    public interface ICashOperationsFeeRepository
    {
        Task<IReadOnlyList<CashOperationsFee>> GetAllAsync();

        Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(IEnumerable<string> brokerIds);

        Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(string brokerId);

        Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(string brokerId, string asset,
            ListSortDirection sortOrder = ListSortDirection.Ascending, long cursor = 0, int limit = 50);

        Task<CashOperationsFee> GetAsync(string brokerId, string asset);

        Task<CashOperationsFee> GetAsync(long id, string brokerId);

        Task<CashOperationsFee> InsertAsync(CashOperationsFee cashOperationsFee);

        Task<CashOperationsFee> UpdateAsync(CashOperationsFee cashOperationsFee);

        Task DeleteAsync(long id, string brokerId);
    }
}
