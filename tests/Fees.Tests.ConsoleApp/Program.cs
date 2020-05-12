using System;
using System.Threading.Tasks;
using Swisschain.Exchange.Fees.Client;

namespace Fees.Tests.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Test console started.");

            var client = new FeesClient(new FeesClientSettings { ServiceAddress = "http://localhost:5001" });

            var cashOperationsFees = await client.CashOperationsFees.GetAllByBrokerId("83829aa1-5888-45e4-997c-b133e58b7ab8");
            var cashOperationsFee = await client.CashOperationsFees.GetByBrokerIdAndAsset("83829aa1-5888-45e4-997c-b133e58b7ab8", "BTC");

            var tradingFees = await client.TradingFees.GetAllByBrokerId("83829aa1-5888-45e4-997c-b133e58b7ab8");
            var tradingFee = await client.TradingFees.GetByBrokerIdAndAssetPair("83829aa1-5888-45e4-997c-b133e58b7ab8", "BTCUSD");
            tradingFee = await client.TradingFees.GetByBrokerIdAndAssetPair("83829aa1-5888-45e4-997c-b133e58b7ab8", null);

            var settings = await client.Settings.GetByBrokerId("83829aa1-5888-45e4-997c-b133e58b7ab8");

            Console.WriteLine("Finished!");
            Console.ReadLine();
        }
    }
}
