using System.Threading.Tasks;
using Fees.Domain.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Swisschain.Exchange.Fees.Contract;

namespace Fees.Grpc
{
    public class SettingsService : Settings.SettingsBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public override async Task<GetSettingsResponse> GetByBrokerId(GetSettingsByBrokerIdRequest request, ServerCallContext context)
        {
            var settings = await _settingsService.GetAsync(request.BrokerId);

            var result = new GetSettingsResponse();

            if (settings == null)
                return result;
            
            var feesSettings = new FeesSettings();

            feesSettings.Id = settings.Id.ToString();
            feesSettings.BrokerId = settings.BrokerId;
            feesSettings.FeeWalletId = settings.FeeWalletId;
            feesSettings.Created = settings.Created.ToTimestamp();
            feesSettings.Modified = settings.Modified.ToTimestamp();

            result.Settings = feesSettings;
            
            return result;
        }
    }
}
