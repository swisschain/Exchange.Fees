using System;

namespace Fees.WebApi.Models.Settings
{
    public class SettingsModel
    {
        public Guid Id { get; set; }

        public string BrokerId { get; set; }

        public string FeeWalletId { get; set; }
        
        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
