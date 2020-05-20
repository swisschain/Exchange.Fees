using System;

namespace Fees.Repositories.Entities
{
    public class SettingsEntity
    {
        public long Id { get; set; }

        public string BrokerId { get; set; }

        public long FeeWalletId { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}
