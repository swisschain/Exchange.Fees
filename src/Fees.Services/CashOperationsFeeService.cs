using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Assets.Client;
using Assets.Client.Models.Assets;
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
            IAssetsClient assetsClient, ILogger<CashOperationsFeeService> logger, IMapper mapper)
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
            ListSortDirection sortOrder = ListSortDirection.Ascending, long cursor = 0, int limit = 50)
        {
            return _cashOperationsFeeRepository.GetAllAsync(brokerId, asset, sortOrder, cursor, limit);
        }

        public Task<IReadOnlyList<CashOperationsFeeHistory>> GetAllHistoriesAsync(long? cashOperationFeeId, string brokerId, string userId,
            string asset, ListSortDirection sortOrder = ListSortDirection.Ascending, long cursor = 0, int limit = 50)
        {
            return _cashOperationsFeeHistoryRepository.GetAllAsync(cashOperationFeeId, brokerId, userId, asset,
                sortOrder, cursor, limit);
        }

        public Task<CashOperationsFee> GetAsync(long id, string brokerId)
        {
            return _cashOperationsFeeRepository.GetAsync(id, brokerId);
        }

        public Task<CashOperationsFee> GetAsync(string brokerId, string asset)
        {
            return _cashOperationsFeeRepository.GetAsync(brokerId, asset);
        }

        public async Task<CashOperationsFee> AddAsync(string userId, CashOperationsFee cashOperationsFee)
        {
            var asset = await GetAsset(cashOperationsFee.BrokerId, cashOperationsFee.Asset);

            ValidateAccuracy(cashOperationsFee, asset);

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
            var asset = await GetAsset(cashOperationsFee.BrokerId, cashOperationsFee.Asset);

            ValidateAccuracy(cashOperationsFee, asset);

            var result = await _cashOperationsFeeRepository.UpdateAsync(cashOperationsFee);

            var history = _mapper.Map<CashOperationsFeeHistory>(result);
            history.UserId = userId;
            history.OperationType = HistoryOperationType.Modified;

            await _cashOperationsFeeHistoryRepository.InsertAsync(history);

            _logger.LogInformation("CashOperationsFee has been updated. {$CashOperationsFee}", result);

            return result;
        }

        public async Task DeleteAsync(long id, string brokerId, string userId)
        {
            var domain = await _cashOperationsFeeRepository.GetAsync(id, brokerId);

            await _cashOperationsFeeRepository.DeleteAsync(id, brokerId);

            var history = _mapper.Map<CashOperationsFeeHistory>(domain);
            history.UserId = userId;
            history.OperationType = HistoryOperationType.Deleted;

            await _cashOperationsFeeHistoryRepository.InsertAsync(history);

            _logger.LogInformation("CashOperationsFee has been deleted. {$CashOperationsFee}", domain);
        }

        private async Task<AssetModel> GetAsset(string brokerId, string asset)
        {
            var assets = await _assetsClient.Assets.GetAllByBrokerId(brokerId);

            var assetModel = assets.FirstOrDefault(x => x.Symbol == asset);

            if (assetModel == null)
                throw new EntityNotFoundException(ErrorCode.ItemNotFound, "Asset does not exist.");

            return assetModel;
        }

        private void ValidateAccuracy(CashOperationsFee cashOperationsFee, AssetModel asset)
        {
            var cashInStr = cashOperationsFee.CashInValue.ToString(CultureInfo.InvariantCulture);
            var cashInFractionLength = cashInStr.Substring(cashInStr.IndexOf(".") + 1).Length;
            if (cashInFractionLength > asset.Accuracy)
                throw new IncorrectAccuracyException(ErrorCode.IncorrectAccuracy, "CashIn accuracy is bigger then asset accuracy.");

            var cashOutStr = cashOperationsFee.CashOutValue.ToString(CultureInfo.InvariantCulture);
            var cashOutFractionLength = cashOutStr.Substring(cashOutStr.IndexOf(".") + 1).Length;
            if (cashOutFractionLength > asset.Accuracy)
                throw new IncorrectAccuracyException(ErrorCode.IncorrectAccuracy, "CashOut accuracy is bigger then asset accuracy.");

            var cashTransferStr = cashOperationsFee.CashTransferValue.ToString(CultureInfo.InvariantCulture);
            var cashTransferFractionLength = cashTransferStr.Substring(cashTransferStr.IndexOf(".") + 1).Length;
            if (cashTransferFractionLength > asset.Accuracy)
                throw new IncorrectAccuracyException(ErrorCode.IncorrectAccuracy, "CashTransfer accuracy is bigger then asset accuracy.");
        }
    }
}
