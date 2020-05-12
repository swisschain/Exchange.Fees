using System;
using Swisschain.Exchange.Fees.Contract;

namespace Swisschain.Exchange.Fees.Client.Models.CashOperationsFees
{
    public class CashOperationsFeeModel
    {
        public Guid Id { get; set; }

        public string BrokerId { get; set; }

        public string Asset { get; set; }

        public decimal CashInValue { get; set; }

        public CashOperationsFeeTypeModel CashInFeeType { get; set; }

        public decimal CashOutValue { get; set; }

        public CashOperationsFeeTypeModel CashOutFeeType { get; set; }

        public decimal CashTransferValue { get; set; }

        public CashOperationsFeeTypeModel CashTransferFeeType { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public CashOperationsFeeModel()
        {
        }

        public CashOperationsFeeModel(CashOperationsFee cashOperationsFee)
        {
            Id = Guid.Parse(cashOperationsFee.Id);
            BrokerId = cashOperationsFee.BrokerId;
            Asset = cashOperationsFee.Asset;
            CashInValue = decimal.Parse(cashOperationsFee.CashInValue);
            CashInFeeType = (CashOperationsFeeTypeModel) cashOperationsFee.CashInFeeType;
            CashOutValue = decimal.Parse(cashOperationsFee.CashOutValue);
            CashOutFeeType = (CashOperationsFeeTypeModel)cashOperationsFee.CashOutFeeType;
            CashTransferValue = decimal.Parse(cashOperationsFee.CashTransferValue);
            CashTransferFeeType = (CashOperationsFeeTypeModel)cashOperationsFee.CashTransferFeeType;
            Created = cashOperationsFee.Created.ToDateTime();
            Modified = cashOperationsFee.Modified.ToDateTime();
        }
    }
}
