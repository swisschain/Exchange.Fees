using System;
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

                var model = new TradingFee();
                
                model.Id = tradingFee.Id.ToString();
                model.BrokerId = tradingFee.BrokerId;
                model.AssetPair = tradingFee.AssetPair;
                model.Asset = tradingFee.Asset;
                model.Levels.AddRange(levels.Select(x =>
                {
                    x.Created = DateTime.SpecifyKind(x.Created, DateTimeKind.Utc);
                    x.Modified = DateTime.SpecifyKind(x.Modified, DateTimeKind.Utc);

                    var newLevel = new TradingFeeLevel
                    {
                        Id = x.Id.ToString(),
                        TradingFeeId = x.TradingFeeId.ToString(),
                        Volume = x.Volume.ToString(CultureInfo.InvariantCulture),
                        MakerFee = x.MakerFee.ToString(CultureInfo.InvariantCulture),
                        TakerFee = x.TakerFee.ToString(CultureInfo.InvariantCulture),
                        Created = x.Created.ToTimestamp(),
                        Modified = x.Modified.ToTimestamp()
                    };

                    return newLevel;
                }));
                tradingFee.Created = DateTime.SpecifyKind(tradingFee.Created, DateTimeKind.Utc);
                model.Created = Timestamp.FromDateTime(tradingFee.Created);
                tradingFee.Modified = DateTime.SpecifyKind(tradingFee.Modified, DateTimeKind.Utc);
                model.Modified = Timestamp.FromDateTime(tradingFee.Modified);

                result.TradingFees.Add(model);
            }

            return result;
        }
    }
}
