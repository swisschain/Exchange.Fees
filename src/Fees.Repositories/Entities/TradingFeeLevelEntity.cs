﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fees.Repositories.Entities
{
    [Table("trading_fee_level")]
    public class TradingFeeLevelEntity
    {
        [Key]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; set; }

        [Required]
        [Column("trading_fee_id", TypeName = "uuid")]
        public Guid TradingFeeId { get; set; }

        [Required]
        [Column("volume", TypeName = "decimal")]
        public decimal Volume { get; set; }

        [Required]
        [Column("maker_fee", TypeName = "decimal")]
        public decimal MakerFee { get; set; }

        [Required]
        [Column("taker_fee", TypeName = "decimal")]
        public decimal TakerFee { get; set; }

        [Required]
        [Column("created", TypeName = "timestamp with time zone")]
        public DateTimeOffset Created { get; set; }

        [Required]
        [Column("modified", TypeName = "timestamp with time zone")]
        public DateTimeOffset Modified { get; set; }
    }
}
