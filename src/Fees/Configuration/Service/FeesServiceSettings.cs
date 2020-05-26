using Fees.Configuration.Service.AccountsService;
using Fees.Configuration.Service.AssetsService;
using Fees.Configuration.Service.Db;
using Fees.Configuration.Service.RabbitMq;

namespace Fees.Configuration.Service
{
    public class FeesServiceSettings
    {
        public DbSettings Db { get; set; }

        public RabbitMqSettings RabbitMq { get; set; }

        public AccountsServiceSettings AccountsService { get; set; }

        public AssetsServiceSettings AssetsService { get; set; }
    }
}
