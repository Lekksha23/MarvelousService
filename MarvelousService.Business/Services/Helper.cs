using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Entities;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Services
{
    public class Helper
    {
        private readonly ILogger<Helper> _logger;

        public Helper(ILogger<Helper> logger)
        {
            _logger = logger;
        }
        public void CheckService(Service service)
        {
            if (service is null)
            {
                _logger.LogError("Error in receiving service by Id ");
                throw new NotFoundServiceException("This service does not exist.");
            }
        }
    }
}
