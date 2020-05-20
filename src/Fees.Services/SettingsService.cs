using System.Collections.Generic;
using System.Threading.Tasks;
using Fees.Domain.Entities;
using Fees.Domain.Repositories;
using Fees.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Fees.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly ILogger<SettingsService> _logger;

        public SettingsService(ISettingsRepository settingsRepository,
            ILogger<SettingsService> logger)
        {
            _settingsRepository = settingsRepository;
            _logger = logger;
        }

        public Task<IReadOnlyList<Settings>> GetAllAsync(IEnumerable<string> brokerIds)
        {
            return _settingsRepository.GetAllAsync(brokerIds);
        }

        public Task<Settings> GetAsync(string brokerId)
        {
            return _settingsRepository.GetAsync(brokerId);
        }

        public Task<Settings> GetAsync(long id, string brokerId)
        {
            return _settingsRepository.GetAsync(id, brokerId);
        }

        public async Task<Settings> AddAsync(Settings settings)
        {
            var result = await _settingsRepository.InsertAsync(settings);

            _logger.LogInformation("Settings has been added. {$Settings}", result);

            return result;
        }

        public async Task<Settings> UpdateAsync(Settings settings)
        {
            var result = await _settingsRepository.UpdateAsync(settings);

            _logger.LogInformation("Settings has been updated. {$Settings}", result);

            return result;
        }

        public async Task DeleteAsync(long id, string brokerId)
        {
            var domain = await _settingsRepository.GetAsync(id, brokerId);

            await _settingsRepository.DeleteAsync(id, brokerId);

            _logger.LogInformation("Settings has been deleted. {$Settings}", domain);
        }
    }
}
