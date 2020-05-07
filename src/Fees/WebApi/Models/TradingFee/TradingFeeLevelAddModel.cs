﻿using System;

namespace Fees.WebApi.Models.TradingFee
{
    public class TradingFeeLevelAddModel
    {
        public Guid TradingFeeId { get; set; }

        public decimal Volume { get; set; }

        public decimal MakerFee { get; set; }

        public decimal TakerFee { get; set; }
    }
}
