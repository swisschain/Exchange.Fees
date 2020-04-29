using Fees.Configuration.Service;
using Fees.Configuration.Service.Jwt;

namespace Fees.Configuration
{
    public class AppConfig
    {
        public FeesServiceSettings FeesService { get; set; }

        public JwtSettings Jwt { get; set; }
    }
}
