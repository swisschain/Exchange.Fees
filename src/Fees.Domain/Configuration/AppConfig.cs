using Fees.Domain.Configuration.Service;
using Fees.Domain.Configuration.Service.Jwt;

namespace Fees.Domain.Configuration
{
    public class AppConfig
    {
        public FeesServiceSettings FeesService { get; set; }

        public JwtSettings Jwt { get; set; }
    }
}
