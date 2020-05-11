using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fees.Domain.Entities;

namespace Fees.Domain.Repositories
{
    public interface ISettingsRepository
    {
        Task<IReadOnlyList<Settings>> GetAllAsync(IEnumerable<string> brokerIds);

        Task<Settings> GetAsync(string brokerId);

        Task<Settings> GetAsync(Guid id, string brokerId);

        Task<Settings> InsertAsync(Settings settings);

        Task<Settings> UpdateAsync(Settings settings);

        Task DeleteAsync(Guid id, string brokerId);
    }
}
