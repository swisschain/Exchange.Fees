using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Universe.Tenants.MessagingContract;

namespace Fees.Cunsumers
{
    public class SubscriptionAddedConsumer : IConsumer<SubscriptionAdded>
    {
        private readonly ILogger<SubscriptionAddedConsumer> _logger;

        public SubscriptionAddedConsumer(
            ILogger<SubscriptionAddedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SubscriptionAdded> context)
        {
            var evt = context.Message;

            // TODO: logic here

            _logger.LogInformation("BlockchainAdded command has been processed {@context}", evt);
        }
    }
}
