using System;
using System.Collections.Generic;
using System.Linq;
using Swisschain.Exchange.Fees.Contract;

namespace Swisschain.Exchange.Fees.Client.Models.TradingFees
{
    public class TradingFeeModel
    {
        public Guid Id { get; set; }

        public string AssetPair { get; set; }

        public string Asset { get; set; }

        public IReadOnlyList<TradingFeeLevelModel> Levels { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public TradingFeeModel()
        {
        }

        public TradingFeeModel(TradingFee tradingFee, IEnumerable<TradingFeeLevel> levels)
        {
            Id = Guid.Parse(tradingFee.Id);
            AssetPair = tradingFee.AssetPair;
            Asset = tradingFee.Asset;
            Levels = levels.Select(x => new TradingFeeLevelModel(x)).ToList();
            Created = tradingFee.Created.ToDateTime();
            Modified = tradingFee.Modified.ToDateTime();
        }
    }
}
