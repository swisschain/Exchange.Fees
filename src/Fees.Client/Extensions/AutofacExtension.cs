using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;

namespace Swisschain.Exchange.Fees.Client.Extensions
{
    public static class AutofacExtension
    {
        public static void RegisterFeesClient(
            [NotNull] this ContainerBuilder builder,
            [NotNull] FeesClientSettings settings)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            builder.RegisterInstance(new FeesClient(settings))
                .As<IFeesClient>()
                .SingleInstance();
        }
    }
}
