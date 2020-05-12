using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Assets.Client;
using AutoMapper;
using Fees.Domain.Entities;
using Fees.Domain.Entities.Enums;
using Fees.Domain.Exceptions;
using Fees.Domain.Repositories;
using Fees.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Fees.Services
{
    public class CashOperationsFeeService : ICashOperationsFeeService
    {
        private readonly ICashOperationsFeeRepository _cashOperationsFeeRepository;
        private readonly ICashOperationsFeeHistoryRepository _cashOperationsFeeHistoryRepository;
        private readonly IAssetsClient _assetsClient;
        private readonly ILogger<CashOperationsFeeService> _logger;
        private readonly IMapper _mapper;

        public CashOperationsFeeService(ICashOperationsFeeRepository cashOperationsFeeRepository,
            ICashOperationsFeeHistoryRepository cashOperationsFeeHistoryRepository,
            IAssetsClient assetsClient,
            ILogger<CashOperationsFeeService> logger,
            IMapper mapper)
        {
            _cashOperationsFeeRepository = cashOperationsFeeRepository;
            _cashOperationsFeeHistoryRepository = cashOperationsFeeHistoryRepository;
            _logger = logger;
            _mapper = mapper;
            _assetsClient = assetsClient;
        }

        public Task<IReadOnlyList<CashOperationsFee>> GetAllAsync()
        {
            return _cashOperationsFeeRepository.GetAllAsync();
        }

        public Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(IEnumerable<string> brokerIds)
        {
            return _cashOperationsFeeRepository.GetAllAsync(brokerIds);
        }

        public Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(string brokerId)
        {
            return _cashOperationsFeeRepository.GetAllAsync(brokerId);
        }

        public Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(string brokerId, string asset,
            ListSortDirection sortOrder = ListSortDirection.Ascending, Guid? cursor = null, int limit = 50)
        {
            return _cashOperationsFeeRepository.GetAllAsync(brokerId, asset, sortOrder, cursor, limit);
        }

        public Task<IReadOnlyList<CashOperationsFeeHistory>> GetAllHistoriesAsync(Guid? cashOperationFeeId, string brokerId, string userId,
            string asset, ListSortDirection sortOrder = ListSortDirection.Ascending, Guid? cursor = null, int limit = 50)
        {
            return _cashOperationsFeeHistoryRepository.GetAllAsync(cashOperationFeeId, brokerId, userId, asset,
                sortOrder, cursor, limit);
        }

        public Task<CashOperationsFee> GetAsync(Guid id, string brokerId)
        {
            return _cashOperationsFeeRepository.GetAsync(id, brokerId);
        }

        public Task<CashOperationsFee> GetAsync(string brokerId, string asset)
        {
            return _cashOperationsFeeRepository.GetAsync(brokerId, asset);
        }

        public async Task<CashOperationsFee> AddAsync(string userId, CashOperationsFee cashOperationsFee)
        {
            var assets = await _assetsClient.Assets.GetAllByBrokerId(cashOperationsFee.BrokerId);

            if (!assets.Select(x => x.Symbol).Contains(cashOperationsFee.Asset))
                throw new EntityNotFoundException(ErrorCode.ItemNotFound, "Asset does not exist.");

            var result = await _cashOperationsFeeRepository.InsertAsync(cashOperationsFee);

            var history = _mapper.Map<CashOperationsFeeHistory>(result);
            history.UserId = userId;
            history.OperationType = HistoryOperationType.Created;

            await _cashOperationsFeeHistoryRepository.InsertAsync(history);

            _logger.LogInformation("CashOperationsFee has been added. {$CashOperationsFee}", result);

            return result;
        }

        public async Task<CashOperationsFee> UpdateAsync(string userId, CashOperationsFee cashOperationsFee)
        {
            var result = await _cashOperationsFeeRepository.UpdateAsync(cashOperationsFee);

            var history = _mapper.Map<CashOperationsFeeHistory>(result);
            history.UserId = userId;
            history.OperationType = HistoryOperationType.Modified;

            await _cashOperationsFeeHistoryRepository.InsertAsync(history);

            _logger.LogInformation("CashOperationsFee has been updated. {$CashOperationsFee}", result);

            return result;
        }

        public async Task DeleteAsync(Guid id, string brokerId, string userId)
        {
            var domain = await _cashOperationsFeeRepository.GetAsync(id, brokerId);

            await _cashOperationsFeeRepository.DeleteAsync(id, brokerId);

            var history = _mapper.Map<CashOperationsFeeHistory>(domain);
            history.UserId = userId;
            history.OperationType = HistoryOperationType.Deleted;

            await _cashOperationsFeeHistoryRepository.InsertAsync(history);

            _logger.LogInformation("CashOperationsFee has been deleted. {$CashOperationsFee}", domain);
        }
    }
}
