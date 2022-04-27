using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
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

        public static void CheckIfResourceModelCountIsZero(List<ResourceModel> resource)
        {
            if (resource.Count == 0)
            {
                _logger.Error("Error! No active resources found");
                throw new NotFoundServiceException("No active resources found");
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

        public static void CheckIfRoleStrategyIsNull(IRoleStrategy roleStrategy)
        {
            if (roleStrategy == null)
            {
                _logger.Error("Unknown role was trying to get access to AddLeadResource method.");
                throw new RoleException("Unknown role was trying to get access to AddLeadResource method.");
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
