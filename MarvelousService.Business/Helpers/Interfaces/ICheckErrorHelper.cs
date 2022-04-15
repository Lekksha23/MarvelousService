using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;


namespace MarvelousService.BusinessLayer.Clients
{
    public interface ICheckErrorHelper
    {
        void CheckIfEntityIsNull<T>(int id, T entity);
        void CheckIfResourceModelCountIsZero(List<ResourceModel> resource);
        void CheckIfResourcePaymentsIsNull(List<ResourcePayment> resourcePayments);
        void CheckIfEntityIsNotNull<T>(int id, T entity);
    }
}