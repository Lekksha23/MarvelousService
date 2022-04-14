using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.DataLayer.Entities;
using NLog;

namespace MarvelousService.BusinessLayer.Clients
{
    public static class CheckErrorHelper
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static void CheckIfEntityIsNull<T>(int id, T entity)
        {
            if (entity is null)
            {
                _logger.Error($"Error in receiving {typeof(T).Name} by Id {id}");
                throw new NotFoundServiceException($"{typeof(T).Name} with Id {id} does not exist.");
            }
        }

        public static void CheckIfResourcePaymentsIsNull(List<ResourcePayment> resourcePayments)
        {
            if (resourcePayments is null)
            {
                _logger.Error("Error in receiving information about Resource payment");
                throw new NotFoundServiceException("Resource payment not found");
            }
        }

        public static void CheckIfEntityIsNotNull<T>(int id, T entity)
        {
            if (entity != null)
            {
                _logger.Error($"Error in receiving {typeof(T).Name} by Id {id}");
                throw new DuplicationException($"{typeof(T).Name} with Id {id} already exists.");
            }
        }
    }
}
