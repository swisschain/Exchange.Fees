using System;

namespace Fees.WebApi.Models.CashOperationsFee
{
    public class CashOperationsFeeEditModel
    {
        public long Id { get; set; }

        public decimal CashInValue { get; set; }

        public CashOperationsFeeTypeModel CashInFeeType { get; set; }

        public decimal CashOutValue { get; set; }

        public CashOperationsFeeTypeModel CashOutFeeType { get; set; }

        public decimal CashTransferValue { get; set; }

        public CashOperationsFeeTypeModel CashTransferFeeType { get; set; }
    }
}
