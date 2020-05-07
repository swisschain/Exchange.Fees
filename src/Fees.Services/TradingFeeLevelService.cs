using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fees.Domain.Entities;
using Fees.Domain.Repositories;
using Fees.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Fees.Services
{
    public class TradingFeeLevelService : ITradingFeeLevelService
    {
        private readonly ITradingFeeLevelRepository _tradingFeeLevelRepository;
        private readonly ILogger<TradingFeeLevelService> _logger;
        private readonly IMapper _mapper;

        public TradingFeeLevelService(ITradingFeeLevelRepository tradingFeeLevelRepository,
            ILogger<TradingFeeLevelService> logger,
            IMapper mapper)
        {
            _tradingFeeLevelRepository = tradingFeeLevelRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<IReadOnlyList<TradingFeeLevel>> GetAllAsync(Guid tradingFeeId, string brokerId)
        {
            return _tradingFeeLevelRepository.GetAllAsync(tradingFeeId, brokerId);
        }

        public Task<TradingFeeLevel> GetAsync(Guid id, string brokerId)
        {
            return _tradingFeeLevelRepository.GetAsync(id, brokerId);
        }

        public async Task<TradingFeeLevel> AddAsync(TradingFeeLevel tradingFeeLevel, string brokerId)
        {
            var result = await _tradingFeeLevelRepository.InsertAsync(tradingFeeLevel, brokerId);

            _logger.LogInformation("TradingFeeLevel has been added. {$TradingFeeLevel}", result);

            return result;
        }

        public async Task<TradingFeeLevel> UpdateAsync(TradingFeeLevel tradingFeeLevel, string brokerId)
        {
            var result = await _tradingFeeLevelRepository.UpdateAsync(tradingFeeLevel, brokerId);

            _logger.LogInformation("TradingFeeLevel has been updated. {$TradingFeeLevel}", result);

            return result;
        }

        public async Task DeleteAsync(Guid id, string brokerId)
        {
            var domain = await _tradingFeeLevelRepository.GetAsync(id, brokerId);

            await _tradingFeeLevelRepository.DeleteAsync(id, brokerId);

            _logger.LogInformation("TradingFeeLevel has been deleted. {$TradingFeeLevel}", domain);
        }
    }
}
