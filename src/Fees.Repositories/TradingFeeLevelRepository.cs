using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fees.Domain.Entities;
using Fees.Domain.Exceptions;
using Fees.Domain.Repositories;
using Fees.Repositories.Context;
using Fees.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fees.Repositories
{
    public class TradingFeeLevelRepository : ITradingFeeLevelRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IMapper _mapper;

        public TradingFeeLevelRepository(ConnectionFactory connectionFactory, IMapper mapper)
        {
            _connectionFactory = connectionFactory;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TradingFeeLevel>> GetAllAsync(Guid tradingFeeId, string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var tradingFee = await GetTradingFeeAsync(tradingFeeId, brokerId, context);

                if (tradingFee == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"TradingFee with id '{tradingFeeId}' does not exist.");

                IQueryable<TradingFeeLevelEntity> query = context.TradingFeeLevels;

                query = query.Where(x => x.TradingFeeId == tradingFeeId);

                var data = await query.ToListAsync();

                return _mapper.Map<List<TradingFeeLevel>>(data);
            }
        }

        public async Task<TradingFeeLevel> GetAsync(Guid id, string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var tradingFeeLevel = await GetAsync(id, context);

                if (tradingFeeLevel == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"TradingFeeLevel with id '{id}' does not exist.");

                var tradingFee = await GetTradingFeeAsync(tradingFeeLevel.TradingFeeId, brokerId, context);

                if (tradingFee == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"TradingFee with id '{tradingFeeLevel.TradingFeeId}' does not exist.");

                return _mapper.Map<TradingFeeLevel>(tradingFeeLevel);
            }
        }

        public async Task<TradingFeeLevel> InsertAsync(TradingFeeLevel tradingFeeLevel, string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var existedTradingFeeLevel = await GetAsync(tradingFeeLevel.Id, context);

                if (existedTradingFeeLevel != null)
                    throw new DuplicatedEntityException(ErrorCode.DuplicateItem, $"TradingFeeLevel with id '{tradingFeeLevel.Id}' already exists.");

                existedTradingFeeLevel = await GetAsync(tradingFeeLevel.TradingFeeId, tradingFeeLevel.Volume, context);

                if (existedTradingFeeLevel != null)
                    throw new DuplicatedEntityException(ErrorCode.DuplicateItem, $"TradingFeeLevel with id '{tradingFeeLevel.TradingFeeId}' does not exist.");

                var tradingFee = await GetTradingFeeAsync(tradingFeeLevel.TradingFeeId, brokerId, context);

                if (tradingFee == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"TradingFee with id '{tradingFeeLevel.TradingFeeId}' does not exist.");

                var data = _mapper.Map<TradingFeeLevelEntity>(tradingFeeLevel);

                data.Created = DateTime.UtcNow;
                data.Modified = data.Created;

                context.TradingFeeLevels.Add(data);

                await context.SaveChangesAsync();

                return _mapper.Map<TradingFeeLevel>(data);
            }
        }

        public async Task<TradingFeeLevel> UpdateAsync(TradingFeeLevel tradingFeeLevel, string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var data = await GetAsync(tradingFeeLevel.Id, context);

                if (data == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"TradingFeeLevel with id '{tradingFeeLevel.Id}' does not exist.");

                var tradingFee = await GetTradingFeeAsync(data.TradingFeeId, brokerId, context);

                if (tradingFee == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"TradingFee with id '{data.TradingFeeId}' does not exist.");

                // save fields that has not be updated
                var created = data.Created;
                var tradingFeeId = data.TradingFeeId;

                _mapper.Map(tradingFeeLevel, data);

                // restore fields that has not be updated
                data.Created = created;
                data.TradingFeeId = tradingFeeId;

                data.Modified = DateTime.UtcNow;

                await context.SaveChangesAsync();

                return _mapper.Map<TradingFeeLevel>(data);
            }
        }

        public async Task DeleteAsync(Guid id, string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var existed = await GetAsync(id, context);

                if (existed == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"TradingFeeLevel with id '{id}' does not exist.");

                var tradingFee = await GetTradingFeeAsync(existed.TradingFeeId, brokerId, context);

                if (tradingFee == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"TradingFee with id '{existed.TradingFeeId}' does not exist.");

                context.Remove(existed);

                await context.SaveChangesAsync();
            }
        }

        private async Task<TradingFeeLevelEntity> GetAsync(Guid id, DataContext context)
        {
            IQueryable<TradingFeeLevelEntity> query = context.TradingFeeLevels;

            var existed = await query
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            return existed;
        }

        private async Task<TradingFeeLevelEntity> GetAsync(Guid tradingFeeId, decimal volume, DataContext context)
        {
            IQueryable<TradingFeeLevelEntity> query = context.TradingFeeLevels;

            var existed = await query
                .Where(x => x.Id == tradingFeeId)
                .Where(x => x.Volume == volume)
                .SingleOrDefaultAsync();

            return existed;
        }

        private async Task<TradingFeeEntity> GetTradingFeeAsync(Guid tradingFeeId, string brokerId, DataContext context)
        {
            IQueryable<TradingFeeEntity> query = context.TradingFees;

            var existed = await query
                .Where(x => x.Id == tradingFeeId)
                .Where(x => x.BrokerId == brokerId)
                .SingleOrDefaultAsync();

            return existed;
        }
    }
}
