using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Fees.Domain.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Swisschain.Exchange.Fees.Contract;

namespace Fees.Grpc
{
    public class TradingFeesService : TradingFees.TradingFeesBase
    {
        private readonly ITradingFeeService _tradingFeeService;
        private readonly ITradingFeeLevelService _tradingFeeLevelService;

        public TradingFeesService(ITradingFeeService tradingFeeService,
            ITradingFeeLevelService tradingFeeLevelService)
        {
            _tradingFeeService = tradingFeeService;
            _tradingFeeLevelService = tradingFeeLevelService;
        }

        public override async Task<GetAllTradingFeesResponse> GetAllByBrokerId(GetAllTradingFeesByBrokerIdRequest request, ServerCallContext context)
        {
            var tradingFees = await _tradingFeeService.GetAllAsync(request.BrokerId);

            var result = new GetAllTradingFeesResponse();

            foreach (var tradingFee in tradingFees)
            {
                var levels = await _tradingFeeLevelService.GetAllAsync(tradingFee.Id, tradingFee.BrokerId);

                var model = Map(tradingFee, levels);

                result.TradingFees.Add(model);
            }

            return result;
        }

        public override async Task<GetTradingFeeByBrokerIdAndAssetPairResponse> GetByBrokerIdAndAssetPair(GetTradingFeeByBrokerIdAndAssetPairRequest request, ServerCallContext context)
        {
            var tradingFee = await _tradingFeeService.GetAsync(request.BrokerId, request.AssetPair);

            var result = new GetTradingFeeByBrokerIdAndAssetPairResponse();

            if (tradingFee == null)
            {
                tradingFee = await _tradingFeeService.GetAsync(request.BrokerId, null);

                if (tradingFee == null)
                    return null;
            }

            var levels = await _tradingFeeLevelService.GetAllAsync(tradingFee.Id, tradingFee.BrokerId);

            var contract = Map(tradingFee, levels);

            result.TradingFee = contract;

            return result;
        }

        private TradingFee Map(Domain.Entities.TradingFee domain, IReadOnlyList<Domain.Entities.TradingFeeLevel> levels)
        {
            if (domain == null)
                return null;

            var model = new TradingFee();

            model.Id = domain.Id;
            model.BrokerId = domain.BrokerId;
            model.AssetPair = domain.AssetPair;
            model.Asset = domain.Asset;
            model.Levels.AddRange(levels.Select(x =>
            {
                var newLevel = new TradingFeeLevel
                {
                    Id = x.Id,
                    TradingFeeId = x.TradingFeeId,
                    Volume = x.Volume.ToString(CultureInfo.InvariantCulture),
                    MakerFee = x.MakerFee.ToString(CultureInfo.InvariantCulture),
                    TakerFee = x.TakerFee.ToString(CultureInfo.InvariantCulture),
                    Created = x.Created.ToTimestamp(),
                    Modified = x.Modified.ToTimestamp()
                };

                return newLevel;
            }));
            model.Created = Timestamp.FromDateTime(domain.Created);
            model.Modified = Timestamp.FromDateTime(domain.Modified);

            return model;
        }
    }
}
