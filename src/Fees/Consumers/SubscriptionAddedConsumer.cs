using System.Linq;
using System.Threading.Tasks;
using Fees.Domain.Entities;
using Fees.Domain.Services;
using MassTransit;
using Microsoft.Extensions.Logging;
using Swisschain.Exchange.Accounts.Client;
using Swisschain.Exchange.Accounts.Client.Models.Account;
using Universe.Tenants.MessagingContract;

namespace Fees.Consumers
{
    public class SubscriptionAddedConsumer : IConsumer<SubscriptionAdded>
    {
        private readonly ISettingsService _settingsService;
        private readonly IAccountsClient _accountsClient;
        private readonly ILogger<SubscriptionAddedConsumer> _logger;

        public SubscriptionAddedConsumer(ISettingsService settingsService,
            IAccountsClient accountsClient, ILogger<SubscriptionAddedConsumer> logger)
        {
            _settingsService = settingsService;
            _accountsClient = accountsClient;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SubscriptionAdded> context)
        {
            var message = context.Message;

            var brokerId = message.SubscriptionId.ToString();

            var account = new AccountAddModel();
            account.BrokerId = brokerId;
            account.Name = "Fee Account";
            var newAccount = await _accountsClient.Account.AddAsync(account);

            var settings = new Settings();
            settings.BrokerId = message.SubscriptionId.ToString();
            settings.FeeWalletId = newAccount.Wallets.Single().Id;

            await _settingsService.AddAsync(settings);

            _logger.LogInformation("SubscriptionAdded command has been processed {@context}", message);
        }
    }
}
