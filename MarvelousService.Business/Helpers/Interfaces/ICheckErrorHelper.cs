using MarvelousService.DataLayer.Entities;
using RestSharp;

namespace MarvelousService.BusinessLayer.Services
{
    public interface ICheckErrorHelper
    {
        void CheckIfEntityIsNull<T>(int id, T entity);
        void CheckMicroserviceResponse(RestResponse response);
        void CheckIfResourcePaymentsIsNull(List<ResourcePayment> resourcePayments);
    }
}