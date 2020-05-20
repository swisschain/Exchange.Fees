using System;

namespace Fees.Domain.Entities
{
    public class TradingFeeLevel
    {
        public long Id { get; set; }

        public long TradingFeeId { get; set; }

        public decimal Volume { get; set; }

        public decimal MakerFee { get; set; }

        public decimal TakerFee { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
