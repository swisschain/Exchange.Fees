using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fees.Repositories.Entities.Enums;

namespace Fees.Repositories.Entities
{
    [Table("cash_operations_fee")]
    public class CashOperationsFeeEntity
    {
        [Key]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; set; }

        [Required]
        [Column("broker_id", TypeName = "varchar(36)")]
        public string BrokerId { get; set; }

        [Required]
        [Column("asset", TypeName = "varchar(8)")]
        public string Asset { get; set; }

        [Required]
        [Column("cash_in_value", TypeName = "decimal(48,16)")]
        public decimal CashInValue { get; set; }
        [Required]
        [Column("cash_in_type", TypeName = "varchar(16)")]
        public CashOperationsFeeTypeEntity CashInFeeType { get; set; }
        
        [Required]
        [Column("cash_out_value", TypeName = "decimal(48,16)")]
        public decimal CashOutValue { get; set; }
        [Required]
        [Column("cash_out_type", TypeName = "varchar(16)")]
        public CashOperationsFeeTypeEntity CashOutFeeType { get; set; }

        [Required]
        [Column("cash_transfer_value", TypeName = "decimal(48,16)")]
        public decimal CashTransferValue { get; set; }
        [Required]
        [Column("cash_transfer_type", TypeName = "varchar(16)")]
        public CashOperationsFeeTypeEntity CashTransferFeeType { get; set; }

        [Required]
        [Column("created", TypeName = "timestamp with time zone")]
        public DateTimeOffset Created { get; set; }

        [Required]
        [Column("modified", TypeName = "timestamp with time zone")]
        public DateTimeOffset Modified { get; set; }
    }
}
