using System.Globalization;
using System.Threading.Tasks;
using Fees.Domain.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Swisschain.Exchange.Fees.Contract;

namespace Fees.Grpc
{
    public class CashOperationsFeesService : CashOperationsFees.CashOperationsFeesBase
    {
        private readonly ICashOperationsFeeService _cashOperationsFeeService;

        public CashOperationsFeesService(ICashOperationsFeeService cashOperationsFeeService)
        {
            _cashOperationsFeeService = cashOperationsFeeService;
        }

        public override async Task<GetAllCashOperationsFeesResponse> GetAllByBrokerId(GetAllCashOperationsFeesByBrokerIdRequest request, ServerCallContext context)
        {
            var cashOperationsFees = await _cashOperationsFeeService.GetAllAsync(request.BrokerId);

            var result = new GetAllCashOperationsFeesResponse();

            foreach (var cashOperationsFee in cashOperationsFees)
            {
                var model = new CashOperationsFee();
                
                model.Id = cashOperationsFee.Id.ToString();
                model.BrokerId = cashOperationsFee.BrokerId;
                model.Asset = cashOperationsFee.Asset;
                model.CashInValue = cashOperationsFee.CashInValue.ToString(CultureInfo.InvariantCulture);
                model.CashInFeeType = (CashOperationsFeeType) cashOperationsFee.CashInFeeType;
                model.CashOutValue = cashOperationsFee.CashOutValue.ToString(CultureInfo.InvariantCulture);
                model.CashOutFeeType = (CashOperationsFeeType)cashOperationsFee.CashOutFeeType;
                model.CashTransferValue = cashOperationsFee.CashTransferValue.ToString(CultureInfo.InvariantCulture);
                model.CashTransferFeeType = (CashOperationsFeeType)cashOperationsFee.CashTransferFeeType;
                model.Created = Timestamp.FromDateTime(cashOperationsFee.Created);
                model.Modified = Timestamp.FromDateTime(cashOperationsFee.Modified);

                result.CashOperationsFees.Add(model);
            }

            return result;
        }
    }
}
