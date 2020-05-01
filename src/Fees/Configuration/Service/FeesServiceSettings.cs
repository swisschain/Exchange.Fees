using Fees.Configuration.Service.AssetsService;
using Fees.Configuration.Service.Db;

namespace Fees.Configuration.Service
{
    public class FeesServiceSettings
    {
        public DbSettings Db { get; set; }

        public AssetsServiceSettings AssetsService { get; set; }
    }
}
