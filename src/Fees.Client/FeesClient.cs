using Swisschain.Exchange.Fees.Client.Common;
using Swisschain.Exchange.Fees.Contract;

namespace Swisschain.Exchange.Fees.Client
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
