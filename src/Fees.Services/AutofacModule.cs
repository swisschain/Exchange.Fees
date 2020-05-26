using Assets.Client;
using Assets.Client.Extensions;
using Autofac;
using Fees.Domain.Services;
using Swisschain.Exchange.Accounts.Client;
using Swisschain.Exchange.Accounts.Client.Extensions;

namespace Fees.Services
{
    public class AutofacModule : Module
    {
        private readonly string _accountsServiceAddress;
        private readonly string _assetsServiceAddress;

        public AutofacModule(string accountsServiceAddress, string assetsServiceAddress)
        {
            _accountsServiceAddress = accountsServiceAddress;
            _assetsServiceAddress = assetsServiceAddress;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CashOperationsFeeService>()
                .As<ICashOperationsFeeService>()
                .SingleInstance();

            builder.RegisterType<TradingFeeService>()
                .As<ITradingFeeService>()
                .SingleInstance();

            builder.RegisterType<TradingFeeLevelService>()
                .As<ITradingFeeLevelService>()
                .SingleInstance();

            builder.RegisterType<SettingsService>()
                .As<ISettingsService>()
                .SingleInstance();

            builder.RegisterAccountsClient(new AccountsClientSettings {ServiceAddress = _accountsServiceAddress });

            builder.RegisterAssetsClient(new AssetsClientSettings { ServiceAddress = _assetsServiceAddress });
        }
    }
}
