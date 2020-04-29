using System;

namespace Fees.WebApi.Models.CashOperationsFee
{
    public class CashOperationsFeeModel
    {
        public Guid Id { get; set; }

        public string Asset { get; set; }

        public decimal CashInValue { get; set; }

        public CashOperationsFeeTypeModel CashInFeeType { get; set; }

        public decimal CashOutValue { get; set; }

        public CashOperationsFeeTypeModel CashOutFeeType { get; set; }

        public decimal CashTransferValue { get; set; }

        public CashOperationsFeeTypeModel CashTransferFeeType { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
