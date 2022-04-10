using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.DataLayer.Entities;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Net;

namespace MarvelousService.BusinessLayer.Helpers
{
    public class Helper : IHelper
    {
        private readonly ILogger<Helper> _logger;

        public Helper(ILogger<Helper> logger)
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

        public void CheckMicroserviceResponse(RestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.RequestTimeout:
                    _logger.LogError($"Request Timeout {response.ErrorException.Message}");
                    throw new RequestTimeoutException(response.ErrorException.Message);
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    _logger.LogError($"Service Unavailable {response.ErrorException.Message}");
                    throw new ServiceUnavailableException(response.ErrorException.Message);
                    break;
                case HttpStatusCode.BadGateway:
                    _logger.LogError($"Bad gatеway {response.ErrorException.Message}");
                    throw new BadGatewayException(response.ErrorException.Message);
                    break;
                case HttpStatusCode.BadRequest:
                    _logger.LogError($"Bad request {response.ErrorException.Message}");
                    throw new BadRequestException(response.ErrorException.Message);
                    break;
                case HttpStatusCode.Unauthorized:
                    _logger.LogError($"Unauthorized {response.ErrorException.Message}");
                    throw new BadRequestException(response.ErrorException.Message);
                    break;
            }

            if (response.Content == null)
            {
                _logger.LogError($"Response content equals null!");
                throw new BadGatewayException("Response content equals null!");
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError($"Incorrect response from Microservice {response.ErrorException.Message}");
                throw new InternalServerError(response.ErrorException.Message);
            }
        }
    }
}
