namespace Fees.WebApi.Models.TradingFee
{
    public class TradingFeeLevelEditModel
    {
        public long Id { get; set; }

        public decimal Volume { get; set; }

        public decimal MakerFee { get; set; }

        public decimal TakerFee { get; set; }
    }
}
