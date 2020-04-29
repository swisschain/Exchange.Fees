using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Fees.Domain.Entities;

namespace Fees.Domain.Services
{
    public interface ICashOperationsFeeService
    {
        Task<IReadOnlyList<CashOperationsFee>> GetAllAsync();

        Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(IEnumerable<Guid> brokerIds);

        Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(Guid brokerId);

        Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(Guid brokerId, string asset,
            ListSortDirection sortOrder = ListSortDirection.Ascending, Guid? cursor = null, int limit = 50);

        Task<CashOperationsFee> GetAsync(Guid id, Guid brokerId);

        Task<CashOperationsFee> AddAsync(CashOperationsFee cashOperationsFee);

        Task<CashOperationsFee> UpdateAsync(CashOperationsFee cashOperationsFee);

        Task DeleteAsync(Guid id, Guid brokerId);
    }
}
