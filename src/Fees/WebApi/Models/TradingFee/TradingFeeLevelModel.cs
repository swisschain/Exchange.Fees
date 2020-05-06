using System;

namespace Fees.WebApi.Models.TradingFee
{
    public class TradingFeeLevelModel
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
