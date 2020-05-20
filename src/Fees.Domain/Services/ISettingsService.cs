using System.Collections.Generic;
using System.Threading.Tasks;
using Fees.Domain.Entities;

namespace Fees.Domain.Services
{
    public interface ISettingsService
    {
        Task<IReadOnlyList<Settings>> GetAllAsync(IEnumerable<string> brokerIds);

        Task<Settings> GetAsync(string brokerId);

        Task<Settings> GetAsync(long id, string brokerId);

        Task<Settings> AddAsync(Settings settings);

        Task<Settings> UpdateAsync(Settings settings);

        Task DeleteAsync(long id, string brokerId);
    }
}
