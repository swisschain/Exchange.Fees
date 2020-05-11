using System;

namespace Fees.Domain.Entities
{
    public class Settings
    {
        public Guid Id { get; set; }

        public string BrokerId { get; set; }

        public string FeeWalletId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
