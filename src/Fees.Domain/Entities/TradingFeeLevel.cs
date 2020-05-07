using System;

namespace Fees.Domain.Entities
{
    public class TradingFeeLevel
    {
        public Guid Id { get; set; }

        public Guid TradingFeeId { get; set; }

        public decimal Volume { get; set; }

        public decimal MakerFee { get; set; }

        public decimal TakerFee { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
