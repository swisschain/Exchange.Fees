using System;

namespace Fees.WebApi.Models.Settings
{
    public class SettingsModel
    {
        public long Id { get; set; }

        public string BrokerId { get; set; }

        public long FeeWalletId { get; set; }
        
        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
