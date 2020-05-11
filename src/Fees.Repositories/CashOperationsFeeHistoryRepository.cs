using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fees.Domain.Entities;
using Fees.Domain.Repositories;
using Fees.Repositories.Context;
using Fees.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fees.Repositories
{
    public class CashOperationsFeeHistoryRepository : ICashOperationsFeeHistoryRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IMapper _mapper;

        public CashOperationsFeeHistoryRepository(ConnectionFactory connectionFactory, IMapper mapper)
        {
            _connectionFactory = connectionFactory;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CashOperationsFeeHistory>> GetAllAsync(Guid? cashOperationFeeId, string brokerId, string userId, string asset,
            ListSortDirection sortOrder = ListSortDirection.Ascending, Guid? cursor = null, int limit = 50)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<CashOperationsFeeHistoryEntity> query = context.CashOperationsFeeHistories;

                if (cashOperationFeeId.HasValue)
                    query = query.Where(x => x.CashOperationsFeeId == cashOperationFeeId);

                query = query.Where(x => x.BrokerId == brokerId);

                if (!string.IsNullOrEmpty(userId))
                    query = query.Where(x => x.UserId == userId);

                if (!string.IsNullOrEmpty(asset))
                    query = query.Where(x => EF.Functions.ILike(x.Asset, $"{asset}"));

                if (sortOrder == ListSortDirection.Ascending)
                {
                    if (cursor != null)
                        query = query.Where(x => x.Id.ToString().CompareTo(cursor.ToString()) >= 0);

                    query = query.OrderBy(x => x.Id);
                }
                else
                {
                    if (cursor != null)
                        query = query.Where(x => x.Id.ToString().CompareTo(cursor.ToString()) < 0);

                    query = query.OrderByDescending(x => x.Id);
                }

                query = query.Take(limit);

                var data = await query.ToListAsync();

                return _mapper.Map<List<CashOperationsFeeHistory>>(data);
            }
        }

        public async Task<CashOperationsFeeHistory> InsertAsync(CashOperationsFeeHistory cashOperationsFeeHistory)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var data = _mapper.Map<CashOperationsFeeHistoryEntity>(cashOperationsFeeHistory);

                data.Id = Guid.NewGuid();
                data.Timestamp = DateTime.UtcNow;

                context.CashOperationsFeeHistories.Add(data);

                await context.SaveChangesAsync();

                return _mapper.Map<CashOperationsFeeHistory>(data);
            }
        }
    }
}
