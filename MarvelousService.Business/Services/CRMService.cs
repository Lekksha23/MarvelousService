using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Exceptions;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Clients
{
    public class CRMService : ICRMService
    {
        private readonly ICRMClient _crmClient;
        private readonly ILogger<CRMService> _logger;

        public CRMService(ICRMClient crmClient, ILogger<CRMService> logger)
        {
            _crmClient = crmClient;
            _logger = logger;
        }

        public async Task<int> GetIdOfRubLeadAccount(string jwtToken)
        {
            var count = 0;
            var accountId = 0;
            _logger.LogInformation("Query for getting all Accounts for Lead from CRM");
            var leadAccounts = await _crmClient.GetLeadAccounts(jwtToken);
            _logger.LogInformation("All Accounts for Lead from CRM were received");
            for (int i = 0; i < leadAccounts.Count; i++)
            {
                if (leadAccounts[i].CurrencyType == Currency.RUB)
                {
                    accountId = leadAccounts[i].Id;
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                throw new AccountNotFoundException(
                    $"There's no accounts with RUB CurrencyType was found in CRM for Lead with id {leadAccounts[0].Lead.Id}");
                _logger.LogError($"There's no accounts with RUB CurrencyType was found in CRM for Lead with id {leadAccounts[0].Lead.Id}");
            }
            return accountId;
        }
    }
}
