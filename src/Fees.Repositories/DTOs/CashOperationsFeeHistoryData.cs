using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fees.Repositories.DTOs.Enums;

namespace Fees.Repositories.DTOs
{
    [Table("cash_operations_fee_history")]
    public class CashOperationsFeeHistoryData
    {
        [Key]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; set; }

        [Required]
        [Column("cash_operations_fee_id", TypeName = "uuid")]
        public Guid CashOperationsFeeId { get; set; }

        [Required]
        [Column("broker_id", TypeName = "varchar(36)")]
        public string BrokerId { get; set; }

        [Required]
        [Column("user_id", TypeName = "varchar(36)")]
        public string UserId { get; set; }

        [Required]
        [Column("asset", TypeName = "varchar(8)")]
        public string Asset { get; set; }

        [Required]
        [Column("cash_in_value", TypeName = "decimal(48,16)")]
        public decimal CashInValue { get; set; }
        [Required]
        [Column("cash_in_type", TypeName = "varchar(16)")]
        public CashOperationsFeeTypeData CashInFeeType { get; set; }
        
        [Required]
        [Column("cash_out_value", TypeName = "decimal(48,16)")]
        public decimal CashOutValue { get; set; }
        [Required]
        [Column("cash_out_type", TypeName = "varchar(16)")]
        public CashOperationsFeeTypeData CashOutFeeType { get; set; }

        [Required]
        [Column("cash_transfer_value", TypeName = "decimal(48,16)")]
        public decimal CashTransferValue { get; set; }
        [Required]
        [Column("cash_transfer_type", TypeName = "varchar(16)")]
        public CashOperationsFeeTypeData CashTransferFeeType { get; set; }

        [Required]
        [Column("history_operation_type", TypeName = "varchar(16)")]
        public HistoryOperationTypeData OperationType { get; set; }

        [Required]
        [Column("timestamp", TypeName = "timestamp with time zone")]
        public DateTime Timestamp { get; set; }
    }
}
