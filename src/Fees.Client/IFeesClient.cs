using Swisschain.Exchange.Fees.Client.Api;

namespace Swisschain.Exchange.Fees.Client
{
    public interface IFeesClient
    {
        ICashOperationsFeesApi CashOperationsFees { get; }

        ITradingFeesApi TradingFees { get; }

        ISettingsApi Settings { get; }
    }
}
