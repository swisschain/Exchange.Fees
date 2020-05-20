using System;
using System.Collections.Generic;

namespace Fees.WebApi.Models.TradingFee
{
    public class TradingFeeModel
    {
        public long Id { get; set; }

        public string AssetPair { get; set; }

        public string Asset { get; set; }

        public IReadOnlyList<TradingFeeLevelModel> Levels { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
