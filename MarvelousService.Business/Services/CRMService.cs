using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Exceptions;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Services
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

        public async Task<int> GetIdOfRubLeadAccount()
        {
            var count = 0;
            var accountId = 0;
            var leadAccounts = await _crmClient.GetLeadAccounts();

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
                throw new AccountException(
                    $"There's no accounts with RUB CurrencyType was found in CRM for Lead with id {leadAccounts[0].Lead.Id}");
            }
            return accountId;
        }
    }
}
