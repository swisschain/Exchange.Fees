using System;

namespace Fees.WebApi.Models.TradingFee
{
    public class TradingFeeLevelModel
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
