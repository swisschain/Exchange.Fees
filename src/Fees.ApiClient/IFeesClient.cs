using Swisschain.Exchange.Fees.ApiContract;

namespace Swisschain.Exchange.Fees.ApiClient
{
    public interface IFeesClient
    {
        Monitoring.MonitoringClient Monitoring { get; }
    }
}
