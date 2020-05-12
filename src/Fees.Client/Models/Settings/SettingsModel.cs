using System;
using Swisschain.Exchange.Fees.Contract;

namespace Swisschain.Exchange.Fees.Client.Models.Settings
{
    public class SettingsModel
    {
        public Guid Id { get; set; }

        public string BrokerId { get; set; }

        public string FeeWalletId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public SettingsModel()
        {
        }

        public SettingsModel(FeesSettings settings)
        {
            Id = Guid.Parse(settings.Id);
            BrokerId = settings.BrokerId;
            FeeWalletId = settings.FeeWalletId;
            Created = settings.Created.ToDateTime();
            Modified = settings.Modified.ToDateTime();
        }
    }
}
