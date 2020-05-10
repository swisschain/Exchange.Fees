using System;
using System.Collections.Generic;

namespace Fees.Domain.Entities
{
    public class TradingFee
    {
        public Guid Id { get; set; }

        public string BrokerId { get; set; }

        public string AssetPair { get; set; }

        public string Asset { get; set; }

        public IReadOnlyList<TradingFeeLevel> Levels { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
