using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fees.Repositories.DTOs
{
    [Table("trading_fee")]
    public class TradingFeeData
    {
        [Key]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; set; }

        [Required]
        [Column("broker_id", TypeName = "varchar(36)")]
        public string BrokerId { get; set; }

        [Column("assetPair", TypeName = "varchar(16)")]
        public string AssetPair { get; set; }

        [Column("asset", TypeName = "varchar(8)")]
        public string Asset { get; set; }

        public ICollection<TradingFeeLevelData> Levels { get; set; }

        [Required]
        [Column("created", TypeName = "timestamp with time zone")]
        public DateTime Created { get; set; }

        [Required]
        [Column("modified", TypeName = "timestamp with time zone")]
        public DateTime Modified { get; set; }
    }
}
