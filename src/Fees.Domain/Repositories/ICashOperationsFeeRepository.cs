using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Fees.Domain.Entities;

namespace Fees.Domain.Repositories
{
    public interface ICashOperationsFeeRepository
    {
        Task<IReadOnlyList<CashOperationsFee>> GetAllAsync();

        Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(IEnumerable<Guid> brokerIds);

        Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(Guid brokerId);

        Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(Guid brokerId, string asset,
            ListSortDirection sortOrder = ListSortDirection.Ascending, Guid? cursor = null, int limit = 50);

        Task<CashOperationsFee> GetAsync(Guid id, Guid brokerId);

        Task<CashOperationsFee> InsertAsync(CashOperationsFee cashOperationsFee);

        Task<CashOperationsFee> UpdateAsync(CashOperationsFee cashOperationsFee);

        Task DeleteAsync(Guid id, Guid brokerId);
    }
}
