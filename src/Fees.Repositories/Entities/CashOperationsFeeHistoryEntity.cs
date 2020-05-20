using System;
using Fees.Repositories.Entities.Enums;

namespace Fees.Repositories.Entities
{
    public class CashOperationsFeeHistoryEntity
    {
        public long Id { get; set; }

        public long CashOperationsFeeId { get; set; }

        public string BrokerId { get; set; }

        public string UserId { get; set; }

        public string Asset { get; set; }

        public decimal CashInValue { get; set; }
        
        public CashOperationsFeeTypeEntity CashInFeeType { get; set; }
        
        public decimal CashOutValue { get; set; }
        
        public CashOperationsFeeTypeEntity CashOutFeeType { get; set; }

        public decimal CashTransferValue { get; set; }
        
        public CashOperationsFeeTypeEntity CashTransferFeeType { get; set; }

        public HistoryOperationTypeEntity OperationType { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}
