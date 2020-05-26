using System;
using Autofac;
using AutoMapper;
using Fees.Configuration;
using Fees.Consumers;
using Fees.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Fees.Grpc;
using Fees.HostedServices;
using Fees.Repositories.Context;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swisschain.Sdk.Server.Common;

namespace Fees
{
    public sealed class Startup : SwisschainStartup<AppConfig>
    {
        public Startup(IConfiguration configuration) 
            : base(configuration)
        {
            AddJwtAuth(Config.Jwt.Secret, "exchange.swisschain.io");

            AddExceptionHandlingMiddleware<UnhandledExceptionsMiddleware>();
        }

        protected override void ConfigureServicesExt(IServiceCollection services)
        {
            services
                .AddAutoMapper(typeof(AutoMapperProfile), typeof(Repositories.AutoMapperProfile), typeof(Services.AutoMapperProfile))
                .AddControllersWithViews();

            services.AddTransient<SubscriptionAddedConsumer>();

            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(Config.FeesService.RabbitMq.HostUrl, host =>
                    {
                        host.Username(Config.FeesService.RabbitMq.Username);
                        host.Password(Config.FeesService.RabbitMq.Password);
                    });
            
                    cfg.UseMessageRetry(y =>
                        y.Exponential(5, 
                            TimeSpan.FromMilliseconds(100),
                            TimeSpan.FromMilliseconds(10_000), 
                            TimeSpan.FromMilliseconds(100)));
            
                    cfg.SetLoggerFactory(provider.GetRequiredService<ILoggerFactory>());
                    
                    cfg.ReceiveEndpoint("universe-tenants-subscription-added", e =>
                    {
                        e.Consumer(provider.GetRequiredService<SubscriptionAddedConsumer>);
                    });
                }));
            
                services.AddHostedService<BusHost>();
            });
        }

        protected override void ConfigureExt(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.GetRequiredService<AutoMapper.IConfigurationProvider>()
                .AssertConfigurationIsValid();

            app.ApplicationServices.GetRequiredService<ConnectionFactory>()
                .EnsureMigration();
        }

        protected override void ConfigureContainerExt(ContainerBuilder builder)
        {
            builder.RegisterModule(new Repositories.AutofacModule(Config.FeesService.Db.ConnectionString));
            builder.RegisterModule(new Services.AutofacModule(
                Config.FeesService.AccountsService.GrpcUrl,
                Config.FeesService.AssetsService.GrpcUrl));
        }

        protected override void RegisterEndpoints(IEndpointRouteBuilder endpoints)
        {
            base.RegisterEndpoints(endpoints);

            endpoints.MapGrpcService<MonitoringService>();
            endpoints.MapGrpcService<CashOperationsFeesService>();
            endpoints.MapGrpcService<TradingFeesService>();
            endpoints.MapGrpcService<SettingsService>();
        }
    }
}
