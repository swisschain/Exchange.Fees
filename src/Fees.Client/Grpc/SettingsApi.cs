using System.Threading.Tasks;
using Swisschain.Exchange.Fees.Client.Api;
using Swisschain.Exchange.Fees.Client.Common;
using Swisschain.Exchange.Fees.Client.Models.Settings;
using Swisschain.Exchange.Fees.Contract;

namespace Swisschain.Exchange.Fees.Client.Grpc
{
    internal class SettingsApi : BaseGrpcClient, ISettingsApi
    {
        private readonly Settings.SettingsClient _client;

        public SettingsApi(string address) : base(address)
        {
            _client = new Settings.SettingsClient(Channel);
        }

        public async Task<SettingsModel> GetByBrokerId(string brokerId)
        {
            var response = await _client.GetByBrokerIdAsync(new GetSettingsByBrokerIdRequest { BrokerId = brokerId });

            if (response.Settings == null)
                return null;

            return new SettingsModel(response.Settings);
        }
    }
}
