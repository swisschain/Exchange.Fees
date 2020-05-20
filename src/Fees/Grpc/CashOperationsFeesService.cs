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
                var contract = Map(cashOperationsFee);

                result.CashOperationsFees.Add(contract);
            }

            return result;
        }

        public override async Task<GetCashOperationsFeeByBrokerIdAndAssetResponse> GetByBrokerIdAndAsset(GetCashOperationsFeeByBrokerIdAndAssetRequest request, ServerCallContext context)
        {
            var cashOperationsFee = await _cashOperationsFeeService.GetAsync(request.BrokerId, request.Asset);

            var result = new GetCashOperationsFeeByBrokerIdAndAssetResponse();

            if (cashOperationsFee == null)
                return result;

            var contract = Map(cashOperationsFee);

            result.CashOperationsFee = contract;

            return result;
        }

        private CashOperationsFee Map(Domain.Entities.CashOperationsFee domain)
        {
            if (domain == null)
                return null;

            var model = new CashOperationsFee();

            model.Id = domain.Id;
            model.BrokerId = domain.BrokerId;
            model.Asset = domain.Asset;
            model.CashInValue = domain.CashInValue.ToString(CultureInfo.InvariantCulture);
            model.CashInFeeType = (CashOperationsFeeType)domain.CashInFeeType;
            model.CashOutValue = domain.CashOutValue.ToString(CultureInfo.InvariantCulture);
            model.CashOutFeeType = (CashOperationsFeeType)domain.CashOutFeeType;
            model.CashTransferValue = domain.CashTransferValue.ToString(CultureInfo.InvariantCulture);
            model.CashTransferFeeType = (CashOperationsFeeType)domain.CashTransferFeeType;
            model.Created = Timestamp.FromDateTime(domain.Created);
            model.Modified = Timestamp.FromDateTime(domain.Modified);

            return model;
        }
    }
}
