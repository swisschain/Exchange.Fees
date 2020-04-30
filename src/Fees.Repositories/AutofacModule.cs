using Autofac;
using Fees.Domain.Repositories;
using Fees.Repositories.Context;

namespace Fees.Repositories
{
    public class AutofacModule : Module
    {
        private readonly string _connectionString;

        public AutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConnectionFactory>()
                .AsSelf()
                .WithParameter(TypedParameter.From(_connectionString))
                .SingleInstance();

            builder.RegisterType<CashOperationsFeeRepository>()
                .As<ICashOperationsFeeRepository>()
                .SingleInstance();

            builder.RegisterType<CashOperationsFeeHistoryRepository>()
                .As<ICashOperationsFeeHistoryRepository>()
                .SingleInstance();
        }
    }
}
