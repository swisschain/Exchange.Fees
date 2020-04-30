using Autofac;
using AutoMapper;
using Fees.Configuration;
using Fees.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Fees.GrpcServices;
using Fees.Repositories.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
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
            builder.RegisterModule(new Services.AutofacModule());
        }

        protected override void RegisterEndpoints(IEndpointRouteBuilder endpoints)
        {
            base.RegisterEndpoints(endpoints);

            endpoints.MapGrpcService<MonitoringService>();
        }
    }
}
