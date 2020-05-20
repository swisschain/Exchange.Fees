using System;
using Fees.Repositories.Entities.Enums;

namespace Fees.Repositories.Entities
{
    public class CashOperationsFeeEntity
    {
        public long Id { get; set; }

        public string BrokerId { get; set; }

        public string Asset { get; set; }

        public decimal CashInValue { get; set; }

        public CashOperationsFeeTypeEntity CashInFeeType { get; set; }
        
        public decimal CashOutValue { get; set; }

        public CashOperationsFeeTypeEntity CashOutFeeType { get; set; }

        public decimal CashTransferValue { get; set; }

        public CashOperationsFeeTypeEntity CashTransferFeeType { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}
