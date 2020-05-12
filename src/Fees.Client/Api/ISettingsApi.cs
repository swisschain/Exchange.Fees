using System.Threading.Tasks;
using Swisschain.Exchange.Fees.Client.Models.Settings;

namespace Swisschain.Exchange.Fees.Client.Api
{
    public interface ISettingsApi
    {
        Task<SettingsModel> GetByBrokerId(string brokerId);
    }
}
