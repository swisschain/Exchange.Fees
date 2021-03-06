﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class TradingFeeRepository : ITradingFeeRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IMapper _mapper;

        public TradingFeeRepository(ConnectionFactory connectionFactory, IMapper mapper)
        {
            _connectionFactory = connectionFactory;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TradingFee>> GetAllAsync(IEnumerable<string> brokerIds)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<TradingFeeEntity> query = context.TradingFees;

                query = query.Where(x => brokerIds.Contains(x.BrokerId));

                query = query.Include(x => x.Levels);

                var data = await query.ToListAsync();

                return _mapper.Map<List<TradingFee>>(data);
            }
        }

        public async Task<IReadOnlyList<TradingFee>> GetAllAsync(string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<TradingFeeEntity> query = context.TradingFees;

                query = query.Where(x => x.BrokerId == brokerId);

                query = query.Include(x => x.Levels);

                var data = await query.ToListAsync();

                return _mapper.Map<List<TradingFee>>(data);
            }
        }

        public async Task<IReadOnlyList<TradingFee>> GetAllAsync(string brokerId, string assetPair,
            ListSortDirection sortOrder = ListSortDirection.Ascending, long cursor = 0, int limit = 50)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<TradingFeeEntity> query = context.TradingFees;

                query = query.Where(x => x.BrokerId == brokerId);

                if (!string.IsNullOrWhiteSpace(assetPair))
                    query = query.Where(x => x.AssetPair == assetPair);

                if (sortOrder == ListSortDirection.Ascending)
                {
                    if (cursor > 0)
                        query = query.Where(x => x.Id >= cursor);

                    query = query.OrderBy(x => x.Id);
                }
                else
                {
                    if (cursor > 0)
                        query = query.Where(x => x.Id < cursor);

                    query = query.OrderByDescending(x => x.Id);
                }

                query = query.Take(limit);

                query = query.Include(x => x.Levels);

                var data = await query.ToListAsync();

                return _mapper.Map<List<TradingFee>>(data);
            }
        }

        public async Task<TradingFee> GetAsync(long id, string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<TradingFeeEntity> query = context.TradingFees;

                var data = await query
                    .Where(x => x.Id == id)
                    .Where(x => x.BrokerId == brokerId)
                    .Include(x => x.Levels)
                    .SingleOrDefaultAsync();

                return _mapper.Map<TradingFee>(data);
            }
        }

        public async Task<TradingFee> GetAsync(string brokerId, string assetPair)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<TradingFeeEntity> query = context.TradingFees;

                var data = await query
                    .Where(x => x.BrokerId == brokerId)
                    .Where(x => x.AssetPair == assetPair)
                    .Include(x => x.Levels)
                    .SingleOrDefaultAsync();

                return _mapper.Map<TradingFee>(data);
            }
        }

        public async Task<TradingFee> InsertAsync(TradingFee tradingFee)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var existedTradingFee = await GetAsync(tradingFee.Id, tradingFee.BrokerId, context);

                if (existedTradingFee != null)
                    throw new DuplicatedEntityException(ErrorCode.DuplicateItem, $"TradingFee with id '{tradingFee.Id}' already exists.");

                existedTradingFee = await GetAsync(tradingFee.BrokerId, tradingFee.AssetPair, context);

                if (existedTradingFee != null)
                    throw new DuplicatedEntityException(ErrorCode.DuplicateItem, $"TradingFee with the asset pair '{tradingFee.AssetPair ?? "null"}' already exists.");

                var data = _mapper.Map<TradingFeeEntity>(tradingFee);

                data.Created = DateTime.UtcNow;
                data.Modified = data.Created;

                context.TradingFees.Add(data);

                await context.SaveChangesAsync();

                return _mapper.Map<TradingFee>(data);
            }
        }

        public async Task<TradingFee> UpdateAsync(TradingFee tradingFee)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var data = await GetAsync(tradingFee.Id, tradingFee.BrokerId, context);

                if (data == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"TradingFee with id '{tradingFee.Id}' does not exist.");

                // save fields that has not be updated
                var assetPair = data.AssetPair;
                var levels = data.Levels;
                var created = data.Created;

                _mapper.Map(tradingFee, data);

                // restore fields that has not be updated
                data.AssetPair = assetPair;
                data.Levels = levels;
                data.Created = created;
                data.Modified = DateTime.UtcNow;

                await context.SaveChangesAsync();

                return _mapper.Map<TradingFee>(data);
            }
        }

        public async Task DeleteAsync(long id, string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var existed = await GetAsync(id, brokerId, context);

                if (existed == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"TradingFee with id '{id}' does not exist.");

                context.Remove(existed);

                await context.SaveChangesAsync();
            }
        }

        private async Task<TradingFeeEntity> GetAsync(long id, string brokerId, DataContext context)
        {
            IQueryable<TradingFeeEntity> query = context.TradingFees;

            var existed = await query
                .Where(x => x.Id == id)
                .Where(x => x.BrokerId == brokerId)
                .Include(x => x.Levels)
                .SingleOrDefaultAsync();

            return existed;
        }

        private async Task<TradingFeeEntity> GetAsync(string brokerId, string assetPair, DataContext context)
        {
            IQueryable<TradingFeeEntity> query = context.TradingFees;

            query = query.Where(x => x.BrokerId == brokerId);

            if (assetPair == null)
                query = query.Where(x => x.AssetPair == assetPair);
            else
                query = query.Where(x => EF.Functions.ILike(x.AssetPair, $"{assetPair}"));

            query = query.Include(x => x.Levels);

            var existed = await query.SingleOrDefaultAsync();

            return existed;
        }
    }
}
