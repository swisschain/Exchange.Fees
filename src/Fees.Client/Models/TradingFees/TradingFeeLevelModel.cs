using System;
using Swisschain.Exchange.Fees.Contract;

namespace Swisschain.Exchange.Fees.Client.Models.TradingFees
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

        public TradingFeeLevelModel()
        {
        }

        public TradingFeeLevelModel(TradingFeeLevel tradingFeeLevel)
        {
            Id = Guid.Parse(tradingFeeLevel.Id);
            TradingFeeId = Guid.Parse(tradingFeeLevel.TradingFeeId);
            Volume = decimal.Parse(tradingFeeLevel.Volume);
            MakerFee = decimal.Parse(tradingFeeLevel.MakerFee);
            TakerFee = decimal.Parse(tradingFeeLevel.TakerFee);
            Created = tradingFeeLevel.Created.ToDateTime();
            Modified = tradingFeeLevel.Modified.ToDateTime();
        }
    }
}
