﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fees.Repositories.DTOs.Enums;

namespace Fees.Repositories.DTOs
{
    [Table("cash_operations_fee")]
    public class CashOperationsFeeData
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
        [Column("created", TypeName = "timestamp with time zone")]
        public DateTime Created { get; set; }

        [Required]
        [Column("modified", TypeName = "timestamp with time zone")]
        public DateTime Modified { get; set; }
    }
}
