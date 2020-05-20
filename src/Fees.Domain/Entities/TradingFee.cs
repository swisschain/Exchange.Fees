using System;
using System.Collections.Generic;

namespace Fees.Domain.Entities
{
    public class TradingFee
    {
        public long Id { get; set; }

        public string BrokerId { get; set; }

        public string AssetPair { get; set; }

        public string Asset { get; set; }

        public ICollection<TradingFeeLevel> Levels { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
