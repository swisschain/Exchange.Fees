using System;

namespace Fees.WebApi.Models.TradingFee
{
    public class TradingFeeModel
    {
        public Guid Id { get; set; }

        public string AssetPair { get; set; }

        public string Asset { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
