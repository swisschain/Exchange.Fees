using System;

namespace Fees.Repositories.Entities
{
    public class TradingFeeLevelEntity
    {
        public long Id { get; set; }

        public long TradingFeeId { get; set; }

        public decimal Volume { get; set; }

        public decimal MakerFee { get; set; }

        public decimal TakerFee { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}
