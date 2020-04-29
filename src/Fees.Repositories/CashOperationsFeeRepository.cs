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
using Fees.Repositories.DTOs;
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

        public async Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(IEnumerable<Guid> brokerIds)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<CashOperationsFeeData> query = context.CashOperationsFees;

                query = query.Where(x => brokerIds.Contains(x.BrokerId));

                var data = await query.ToListAsync();

                return _mapper.Map<List<CashOperationsFee>>(data);
            }
        }

        public async Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(Guid brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<CashOperationsFeeData> query = context.CashOperationsFees;

                query = query.Where(x => x.BrokerId == brokerId);

                var data = await query.ToListAsync();

                return _mapper.Map<List<CashOperationsFee>>(data);
            }
        }

        public async Task<IReadOnlyList<CashOperationsFee>> GetAllAsync(
            Guid brokerId,
            string asset,
            ListSortDirection sortOrder = ListSortDirection.Ascending,
            Guid? cursor = null,
            int limit = 50)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<CashOperationsFeeData> query = context.CashOperationsFees;

                query = query.Where(x => x.BrokerId == brokerId);

                if (!string.IsNullOrEmpty(asset))
                    query = query.Where(x => EF.Functions.ILike(x.Asset, $"{asset}"));

                if (sortOrder == ListSortDirection.Ascending)
                {
                    if (cursor != null)
                        query = query.Where(x => x.Id.ToString().CompareTo(cursor.Value.ToString()) >= 0);

                    query = query.OrderBy(x => x.Id);
                }
                else
                {
                    if (cursor != null)
                        query = query.Where(x => x.Id.ToString().CompareTo(cursor.Value.ToString()) < 0);

                    query = query.OrderByDescending(x => x.Id);
                }

                query = query.Take(limit);

                var data = await query.ToListAsync();

                return _mapper.Map<List<CashOperationsFee>>(data);
            }
        }

        public async Task<CashOperationsFee> GetAsync(Guid id, Guid brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<CashOperationsFeeData> query = context.CashOperationsFees;

                var data = await query
                    .Where(x => x.Id == id)
                    .Where(x => x.BrokerId == brokerId)
                    .SingleOrDefaultAsync();

                return _mapper.Map<CashOperationsFee>(data);
            }
        }

        public async Task<CashOperationsFee> InsertAsync(CashOperationsFee cashOperationsFee)
        {
            try
            {
                using (var context = _connectionFactory.CreateDataContext())
                {
                    var data = _mapper.Map<CashOperationsFeeData>(cashOperationsFee);

                    data.Created = DateTime.UtcNow;

                    context.CashOperationsFees.Add(data);

                    await context.SaveChangesAsync();

                    return _mapper.Map<CashOperationsFee>(data);
                }
            }
            catch (DbUpdateException e)
            {
                throw new DuplicatedEntityException((e.InnerException as Npgsql.PostgresException)?.Detail
                    ?? $"Something went wrong while trying to create a new {typeof(CashOperationsFee)}.", e);
            }
        }

        public async Task<CashOperationsFee> UpdateAsync(CashOperationsFee cashOperationsFee)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var data = await GetAsync(cashOperationsFee.Id, cashOperationsFee.BrokerId, context);

                if (data == null)
                    throw new EntityNotFoundException($"{typeof(CashOperationsFee)} with id '{cashOperationsFee.Id}' is not exist.");

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

        public async Task DeleteAsync(Guid id, Guid brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var existed = await GetAsync(id, brokerId, context);

                if (existed == null)
                    throw new EntityNotFoundException($"{typeof(CashOperationsFee)} with id '{id}' is not exist.");

                context.Remove(existed);

                await context.SaveChangesAsync();
            }
        }

        private async Task<CashOperationsFeeData> GetAsync(Guid id, Guid brokerId, DataContext context)
        {
            IQueryable<CashOperationsFeeData> query = context.CashOperationsFees;

            var existed = await query
                .Where(x => x.Id == id)
                .Where(x => x.BrokerId == brokerId)
                .SingleOrDefaultAsync();

            return existed;
        }
    }
}
