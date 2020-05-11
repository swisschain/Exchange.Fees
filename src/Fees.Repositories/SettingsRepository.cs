
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
    public class SettingsRepository : ISettingsRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IMapper _mapper;

        public SettingsRepository(ConnectionFactory connectionFactory, IMapper mapper)
        {
            _connectionFactory = connectionFactory;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<Settings>> GetAllAsync(IEnumerable<string> brokerIds)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<SettingsEntity> query = context.Settings;

                query = query.Where(x => brokerIds.Contains(x.BrokerId));

                var data = await query.ToListAsync();

                return _mapper.Map<List<Settings>>(data);
            }
        }

        public async Task<Settings> GetAsync(string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<SettingsEntity> query = context.Settings;

                query = query.Where(x => x.BrokerId == brokerId);

                var data = await query.SingleOrDefaultAsync();

                return _mapper.Map<Settings>(data);
            }
        }

        public async Task<Settings> GetAsync(Guid id, string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<SettingsEntity> query = context.Settings;

                var data = await query
                    .Where(x => x.Id == id)
                    .Where(x => x.BrokerId == brokerId)
                    .SingleOrDefaultAsync();

                return _mapper.Map<Settings>(data);
            }
        }

        public async Task<Settings> InsertAsync(Settings settings)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var existedSettings = await GetAsync(settings.Id, settings.BrokerId, context);

                if (existedSettings != null)
                    throw new DuplicatedEntityException(ErrorCode.DuplicateItem, $"Settings with id '{settings.Id}' already exists.");

                var data = _mapper.Map<SettingsEntity>(settings);

                data.Created = DateTime.UtcNow;
                data.Modified = data.Created;

                context.Settings.Add(data);

                await context.SaveChangesAsync();

                return _mapper.Map<Settings>(data);
            }
        }

        public async Task<Settings> UpdateAsync(Settings tradingFee)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var data = await GetAsync(tradingFee.Id, tradingFee.BrokerId, context);

                if (data == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"Settings with id '{tradingFee.Id}' does not exist.");

                // save fields that has not be updated
                var feeWalletId = data.FeeWalletId;
                var created = data.Created;

                _mapper.Map(tradingFee, data);

                // restore fields that has not be updated
                data.FeeWalletId= feeWalletId;
                data.Created = created;
                data.Modified = DateTime.UtcNow;

                await context.SaveChangesAsync();

                return _mapper.Map<Settings>(data);
            }
        }

        public async Task DeleteAsync(Guid id, string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var existed = await GetAsync(id, brokerId, context);

                if (existed == null)
                    throw new EntityNotFoundException(ErrorCode.ItemNotFound, $"Settings with id '{id}' does not exist.");

                context.Remove(existed);

                await context.SaveChangesAsync();
            }
        }

        private async Task<SettingsEntity> GetAsync(Guid id, string brokerId, DataContext context)
        {
            IQueryable<SettingsEntity> query = context.Settings;

            var existed = await query
                .Where(x => x.Id == id)
                .Where(x => x.BrokerId == brokerId)
                .SingleOrDefaultAsync();

            return existed;
        }
    }
}
