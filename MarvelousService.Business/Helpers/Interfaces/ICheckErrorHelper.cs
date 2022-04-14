using MarvelousService.DataLayer.Entities;
using RestSharp;

namespace MarvelousService.BusinessLayer.Clients
{
    public interface ICheckErrorHelper
    {
        void CheckIfEntityIsNull<T>(int id, T entity);
        void CheckIfResourcePaymentsIsNull(List<ResourcePayment> resourcePayments);
        void CheckIfEntityIsNotNull<T>(int id, T entity);
    }
}