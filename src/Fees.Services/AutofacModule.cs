using Assets.Client;
using Assets.Client.Extensions;
using Autofac;
using Fees.Domain.Services;

namespace Fees.Services
{
    public class AutofacModule : Module
    {
        private readonly string _assetsServiceAddress;

        public AutofacModule(string assetsServiceAddress)
        {
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

            builder.RegisterAssetsClient(new AssetsClientSettings {ServiceAddress = _assetsServiceAddress });
        }
    }
}
