using Swisschain.Exchange.Fees.ApiClient.Common;
using Swisschain.Exchange.Fees.ApiContract;

namespace Swisschain.Exchange.Fees.ApiClient
{
    public class FeesClient : BaseGrpcClient, IFeesClient
    {
        public FeesClient(string serverGrpcUrl) : base(serverGrpcUrl)
        {
            Monitoring = new Monitoring.MonitoringClient(Channel);
        }

        public Monitoring.MonitoringClient Monitoring { get; }
    }
}
