using Autofac;
using Fees.Domain.Services;

namespace Fees.Services
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CashOperationsFeeService>()
                .As<ICashOperationsFeeService>()
                .SingleInstance();
        }
    }
}
