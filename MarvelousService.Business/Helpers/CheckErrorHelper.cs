using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.DataLayer.Entities;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Net;

namespace MarvelousService.BusinessLayer.Clients
{
    public class CheckErrorHelper : ICheckErrorHelper
    {
        private readonly ILogger<CheckErrorHelper> _logger;

        public CheckErrorHelper(ILogger<CheckErrorHelper> logger)
        {
            _logger = logger;
        }

        public void CheckIfEntityIsNull<T>(int id, T entity)
        {
            if (entity is null)
            {
                _logger.LogError($"Error in receiving {typeof(T).Name} by Id {id}");
                throw new NotFoundServiceException($"{typeof(T).Name} with Id {id} does not exist.");
            }
        }

        public void CheckIfResourcePaymentsIsNull(List<ResourcePayment> resourcePayments)
        {
            if (resourcePayments is null)
            {
                _logger.LogError("Error in receiving information about Resource payment");
                throw new NotFoundServiceException("Resource payment not found");
            }
        }
    }
}
