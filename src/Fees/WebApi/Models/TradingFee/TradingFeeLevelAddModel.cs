using System;
using Microsoft.AspNetCore.Mvc;

namespace Fees.WebApi.Models.TradingFee
{
    public class TradingFeeLevelAddModel
    {
        [FromRoute]
        public Guid TradingFeeId { get; set; }

        public decimal Volume { get; set; }

        public decimal MakerFee { get; set; }

        public decimal TakerFee { get; set; }
    }
}
