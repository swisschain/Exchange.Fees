using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Fees.Domain.Entities;
using Fees.Domain.Repositories;
using Fees.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Fees.Services
{
    public class CashOperationsFeeService : ICashOperationsFeeService
    {
        private readonly ICashOperationsFeeRepository _cashOperationsFeeRepository;
        private readonly ILogger<CashOperationsFeeService> _logger;

        public CashOperationsFeeService(ICashOperationsFeeRepository cashOperationsFeeRepository,
            ILogger<CashOperationsFeeService> logger)
        {
            _cashOperationsFeeRepository = cashOperationsFeeRepository;
            _logger = logger;
        }

        public Task<IReadOnlyList<CashOperationsFee>> GetAllAsync()
        {
            return _cashOperationsFeeRepository.GetAllAsync();
        }

        public Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(IEnumerable<Guid> brokerIds)
        {
            return _cashOperationsFeeRepository.GetAllAsync(brokerIds);
        }

        public Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(Guid brokerId)
        {
            return _cashOperationsFeeRepository.GetAllAsync(brokerId);
        }

        public Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(Guid brokerId,
            string asset,
            ListSortDirection sortOrder = ListSortDirection.Ascending,
            Guid? cursor = null,
            int limit = 50)
        {
            return _cashOperationsFeeRepository.GetAllAsync(brokerId, asset, sortOrder, cursor, limit);
        }

        public Task<CashOperationsFee> GetAsync(Guid id, Guid brokerId)
        {
            return _cashOperationsFeeRepository.GetAsync(id, brokerId);
        }

        public Task<CashOperationsFee> AddAsync(CashOperationsFee cashOperationsFee)
        {
            //TODO: validate that asset exists

            var result = _cashOperationsFeeRepository.InsertAsync(cashOperationsFee);

            _logger.LogInformation("CashOperationsFee has been added. {$CashOperationsFee}", result);

            return result;
        }

        public Task<CashOperationsFee> UpdateAsync(CashOperationsFee cashOperationsFee)
        {
            var result = _cashOperationsFeeRepository.UpdateAsync(cashOperationsFee);

            _logger.LogInformation("CashOperationsFee has been updated. {$CashOperationsFee}", result);

            return result;
        }

        public Task DeleteAsync(Guid id, Guid brokerId)
        {
            var result = _cashOperationsFeeRepository.DeleteAsync(id, brokerId);

            _logger.LogInformation("CashOperationsFee has been deleted. {$CashOperationsFee}", result);

            return result;
        }
    }
}
