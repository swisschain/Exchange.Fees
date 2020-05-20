using System;
using System.Collections.Generic;

namespace Fees.Repositories.Entities
{
    public class TradingFeeEntity
    {
        public long Id { get; set; }

        public string BrokerId { get; set; }

        public string AssetPair { get; set; }

        public string Asset { get; set; }

        public ICollection<TradingFeeLevelEntity> Levels { get; set; } = new HashSet<TradingFeeLevelEntity>();

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}
