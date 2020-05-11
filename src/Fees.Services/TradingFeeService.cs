using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Assets.Client;
using AutoMapper;
using Fees.Domain.Entities;
using Fees.Domain.Exceptions;
using Fees.Domain.Repositories;
using Fees.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Fees.Services
{
    public class TradingFeeService : ITradingFeeService
    {
        private readonly ITradingFeeRepository _tradingFeeRepository;
        private readonly IAssetsClient _assetsClient;
        private readonly ILogger<TradingFeeService> _logger;

        public TradingFeeService(ITradingFeeRepository tradingFeeRepository,
            IAssetsClient assetsClient,
            ILogger<TradingFeeService> logger)
        {
            _tradingFeeRepository = tradingFeeRepository;
            _logger = logger;
            _assetsClient = assetsClient;
        }

        public Task<IReadOnlyList<TradingFee>> GetAllAsync(IEnumerable<string> brokerIds)
        {
            return _tradingFeeRepository.GetAllAsync(brokerIds);
        }

        public Task<IReadOnlyList<TradingFee>> GetAllAsync(string brokerId)
        {
            return _tradingFeeRepository.GetAllAsync(brokerId);
        }

        public Task<IReadOnlyList<TradingFee>> GetAllAsync(string brokerId, string assetPair,
            ListSortDirection sortOrder = ListSortDirection.Ascending, Guid? cursor = null, int limit = 50)
        {
            return _tradingFeeRepository.GetAllAsync(brokerId, assetPair, sortOrder, cursor, limit);
        }

        public Task<TradingFee> GetAsync(Guid id, string brokerId)
        {
            return _tradingFeeRepository.GetAsync(id, brokerId);
        }

        public async Task<TradingFee> AddAsync(TradingFee tradingFee)
        {
            var assets = await _assetsClient.Assets.GetAllByBrokerId(tradingFee.BrokerId);

            if (tradingFee.Asset != null &&
                !assets.Select(x => x.Symbol).Contains(tradingFee.Asset))
            {
                throw new EntityNotFoundException(ErrorCode.ItemNotFound, "Asset does not exist.");
            }

            var assetPairs = await _assetsClient.AssetPairs.GetAllByBrokerId(tradingFee.BrokerId);

            if (tradingFee.AssetPair != null &&
                !assetPairs.Select(x => x.Symbol).Contains(tradingFee.AssetPair))
            {
                throw new EntityNotFoundException(ErrorCode.ItemNotFound, "Asset pair does not exist.");
            }

            var result = await _tradingFeeRepository.InsertAsync(tradingFee);

            _logger.LogInformation("TradingFee has been added. {$TradingFee}", result);

            return result;
        }

        public async Task<TradingFee> UpdateAsync(TradingFee tradingFee)
        {
            var result = await _tradingFeeRepository.UpdateAsync(tradingFee);

            _logger.LogInformation("TradingFee has been updated. {$TradingFee}", result);

            return result;
        }

        public async Task DeleteAsync(Guid id, string brokerId)
        {
            var domain = await _tradingFeeRepository.GetAsync(id, brokerId);

            await _tradingFeeRepository.DeleteAsync(id, brokerId);

            _logger.LogInformation("TradingFee has been deleted. {$TradingFee}", domain);
        }
    }
}
