using System;
using Fees.Domain.Entities.Enums;

namespace Fees.Domain.Entities
{
    public class CashOperationsFeeHistory
    {
        public long Id { get; set; }

        public long CashOperationsFeeId { get; set; }

        public string BrokerId { get; set; }

        public string UserId { get; set; }

        public string Asset { get; set; }

        public decimal CashInValue { get; set; }

        public CashOperationsFeeType CashInFeeType { get; set; }

        public decimal CashOutValue { get; set; }

        public CashOperationsFeeType CashOutFeeType { get; set; }

        public decimal CashTransferValue { get; set; }

        public CashOperationsFeeType CashTransferFeeType { get; set; }

        public HistoryOperationType OperationType { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
