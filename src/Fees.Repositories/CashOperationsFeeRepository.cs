﻿using System;
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
    public class CashOperationsFeeRepository : ICashOperationsFeeRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IMapper _mapper;

        public CashOperationsFeeRepository(ConnectionFactory connectionFactory, IMapper mapper)
        {
            _connectionFactory = connectionFactory;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CashOperationsFee>> GetAllAsync()
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var data = await context.CashOperationsFees
                    .ToListAsync();

                return _mapper.Map<List<CashOperationsFee>>(data);
            }
        }

        public async Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(IEnumerable<string> brokerIds)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<CashOperationsFeeEntity> query = context.CashOperationsFees;

                query = query.Where(x => brokerIds.Contains(x.BrokerId));

                var data = await query.ToListAsync();

                return _mapper.Map<List<CashOperationsFee>>(data);
            }
        }

        public async Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<CashOperationsFeeEntity> query = context.CashOperationsFees;

                query = query.Where(x => x.BrokerId == brokerId);

                var data = await query.ToListAsync();

                return _mapper.Map<List<CashOperationsFee>>(data);
            }
        }

        public async Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(
            string brokerId, string asset,
            ListSortDirection sortOrder = ListSortDirection.Ascending, long cursor = 0, int limit = 50)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<CashOperationsFeeEntity> query = context.CashOperationsFees;

                query = query.Where(x => x.BrokerId == brokerId);

                if (!string.IsNullOrEmpty(asset))
                    query = query.Where(x => EF.Functions.ILike(x.Asset, $"{asset}"));

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

                var data = await query.ToListAsync();

                return _mapper.Map<List<CashOperationsFee>>(data);
            }
        }

        public async Task<CashOperationsFee> GetAsync(string brokerId, string asset)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<CashOperationsFeeEntity> query = context.CashOperationsFees;

                var data = await query
                    .Where(x => x.Asset == asset)
                    .Where(x => x.BrokerId == brokerId)
                    .SingleOrDefaultAsync();

                return _mapper.Map<CashOperationsFee>(data);
            }
        }

        public async Task<CashOperationsFee> GetAsync(long id, string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<CashOperationsFeeEntity> query = context.CashOperationsFees;

                var data = await query
                    .Where(x => x.Id == id)
                    .Where(x => x.BrokerId == brokerId)
                    .SingleOrDefaultAsync();

                return _mapper.Map<CashOperationsFee>(data);
            }
        }

        public async Task<CashOperationsFee> InsertAsync(CashOperationsFee cashOperationsFee)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var existedAsset = await GetAsync(cashOperationsFee.BrokerId, cashOperationsFee.Asset, context);

                if (existedAsset != null)
                    throw new DuplicatedEntityException(ErrorCode.DuplicateItem, $"CashOperationsFee with id '{cashOperationsFee.Id}' already exists.");

                var data = _mapper.Map<CashOperationsFeeEntity>(cashOperationsFee);

                data.Created = DateTime.UtcNow;
                data.Modified = data.Created;

                context.CashOperationsFees.Add(data);

                await context.SaveChangesAsync();

                return _mapper.Map<CashOperationsFee>(data);
            }
        }

        public async Task<CashOperationsFee> UpdateAsync(CashOperationsFee cashOperationsFee)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var data = await GetAsync(cashOperationsFee.Id, cashOperationsFee.BrokerId, context);

                if (data == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"{typeof(CashOperationsFee)} with id '{cashOperationsFee.Id}' does not exist.");

                // save fields that has not be updated
                var asset = data.Asset;
                var created = data.Created;

                _mapper.Map(cashOperationsFee, data);

                // restore fields that has not be updated
                data.Asset = asset;
                data.Created = created;
                
                data.Modified = DateTime.UtcNow;

                await context.SaveChangesAsync();

                return _mapper.Map<CashOperationsFee>(data);
            }
        }

        public async Task DeleteAsync(long id, string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var existed = await GetAsync(id, brokerId, context);

                if (existed == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"CashOperationsFee with id '{id}' does not exist.");

                context.Remove(existed);

                await context.SaveChangesAsync();
            }
        }

        private async Task<CashOperationsFeeEntity> GetAsync(long id, string brokerId, DataContext context)
        {
            IQueryable<CashOperationsFeeEntity> query = context.CashOperationsFees;

            var existed = await query
                .Where(x => x.Id == id)
                .Where(x => x.BrokerId == brokerId)
                .SingleOrDefaultAsync();

            return existed;
        }

        private async Task<CashOperationsFeeEntity> GetAsync(string brokerId, string asset, DataContext context)
        {
            IQueryable<CashOperationsFeeEntity> query = context.CashOperationsFees;

            var existed = await query
                .Where(x => x.BrokerId == brokerId)
                .Where(x => EF.Functions.ILike(x.Asset, $"{asset}"))
                .SingleOrDefaultAsync();

            return existed;
        }
    }
}
