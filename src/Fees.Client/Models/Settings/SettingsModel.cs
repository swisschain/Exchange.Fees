using System;
using Swisschain.Exchange.Fees.Contract;

namespace Swisschain.Exchange.Fees.Client.Models.Settings
{
    public class SettingsModel
    {
        public long Id { get; set; }

        public string BrokerId { get; set; }

        public long FeeWalletId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public SettingsModel()
        {
        }

        public SettingsModel(FeesSettings settings)
        {
            Id = settings.Id;
            BrokerId = settings.BrokerId;
            FeeWalletId = settings.FeeWalletId;
            Created = settings.Created.ToDateTime();
            Modified = settings.Modified.ToDateTime();
        }
    }
}
