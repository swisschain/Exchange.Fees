using Swisschain.Exchange.Fees.Contract;

namespace Swisschain.Exchange.Fees.Client
{
    public interface IFeesClient
    {
        Monitoring.MonitoringClient Monitoring { get; }
    }
}
