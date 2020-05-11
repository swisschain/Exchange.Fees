using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fees.Repositories.Entities
{
    public class SettingsEntity
    {
        [Key]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; set; }

        [Required]
        [Column("broker_id", TypeName = "varchar(36)")]
        public string BrokerId { get; set; }

        [Required]
        [Column("fee_wallet_id", TypeName = "varchar(64)")]
        public string FeeWalletId { get; set; }

        [Required]
        [Column("created", TypeName = "timestamp with time zone")]
        public DateTimeOffset Created { get; set; }

        [Required]
        [Column("modified", TypeName = "timestamp with time zone")]
        public DateTimeOffset Modified { get; set; }
    }
}
