using Swisschain.Exchange.Fees.Client.Api;
using Swisschain.Exchange.Fees.Client.Common;
using Swisschain.Exchange.Fees.Client.Grpc;

namespace Swisschain.Exchange.Fees.Client
{
    public class FeesClient : BaseGrpcClient, IFeesClient
    {
        public FeesClient(FeesClientSettings settings) : base(settings.ServiceAddress)
        {
            CashOperationsFees = new CashOperationsFeesApi(settings.ServiceAddress);
            TradingFees = new TradingFeesApi(settings.ServiceAddress);
            Settings = new SettingsApi(settings.ServiceAddress);
        }

        public ICashOperationsFeesApi CashOperationsFees { get; }

        public ITradingFeesApi TradingFees { get; }

        public ISettingsApi Settings { get; }
    }
}
