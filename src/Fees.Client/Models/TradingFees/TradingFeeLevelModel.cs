using System;
using Swisschain.Exchange.Fees.Contract;

namespace Swisschain.Exchange.Fees.Client.Models.TradingFees
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

        public TradingFeeLevelModel()
        {
        }

        public TradingFeeLevelModel(TradingFeeLevel tradingFeeLevel)
        {
            Id = tradingFeeLevel.Id;
            TradingFeeId = tradingFeeLevel.TradingFeeId;
            Volume = decimal.Parse(tradingFeeLevel.Volume);
            MakerFee = decimal.Parse(tradingFeeLevel.MakerFee);
            TakerFee = decimal.Parse(tradingFeeLevel.TakerFee);
            Created = tradingFeeLevel.Created.ToDateTime();
            Modified = tradingFeeLevel.Modified.ToDateTime();
        }
    }
}
