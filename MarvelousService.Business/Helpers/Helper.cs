using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.DataLayer.Entities;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Services
{
    public class Helper : IHelper
    {
        private readonly ILogger<Helper> _logger;

        public Helper(ILogger<Helper> logger)
        {
            _logger = logger;
        }

        public void CheckResource(Resource resource)
        {
            if (resource is null)
            {
                _logger.LogError("Error in receiving resource by Id ");
                throw new NotFoundServiceException("This resource does not exist.");
            }
        }
    }
}
