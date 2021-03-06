﻿using System;
using Fees.WebApi.Models.CashOperationsFee;

namespace Fees.WebApi.Models.Audit.CashOperationsFee
{
    public class CashOperationsFeeHistoryModel
    {
        public long Id { get; set; }

        public long CashOperationsFeeId { get; set; }

        public string BrokerId { get; set; }

        public string UserId { get; set; }

        public string Asset { get; set; }

        public decimal CashInValue { get; set; }

        public CashOperationsFeeTypeModel CashInFeeType { get; set; }

        public decimal CashOutValue { get; set; }

        public CashOperationsFeeTypeModel CashOutFeeType { get; set; }

        public decimal CashTransferValue { get; set; }

        public CashOperationsFeeTypeModel CashTransferFeeType { get; set; }

        public HistoryOperationTypeModel OperationType { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
