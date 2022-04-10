﻿using MarvelousService.DataLayer.Entities;
using RestSharp;

namespace MarvelousService.BusinessLayer.Helpers
{
    public interface IHelper
    {
        void CheckIfEntityIsNull<T>(int id, T entity);
        void CheckMicroserviceResponse(RestResponse response);
        void CheckIfResourcePaymentsIsNull(List<ResourcePayment> resourcePayments);
    }
}