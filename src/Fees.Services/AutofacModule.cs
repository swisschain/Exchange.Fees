using Autofac;
using Fees.Domain.Services;
using Swisschain.Exchange.Accounts.Client;
using Swisschain.Exchange.Accounts.Client.Extensions;

namespace Fees.Services
{
    public class AutofacModule : Module
    {
        private readonly string _accountsServiceAddress;

        public AutofacModule(string accountsServiceAddress)
        {
            _accountsServiceAddress = accountsServiceAddress;
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
        }
    }
}
