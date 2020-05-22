using System;

namespace Fees.Domain.Entities
{
    public class Settings
    {
        public long Id { get; set; }

        public string BrokerId { get; set; }

        public long FeeAccountId { get; set; }

        public long FeeWalletId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
