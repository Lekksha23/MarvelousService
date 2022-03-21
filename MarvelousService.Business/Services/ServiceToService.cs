﻿using AutoMapper;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Services
{
    public class ServiceToService : IServiceToService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceToService> _logger;

        public ServiceToService(IServiceRepository serviceRepository, IMapper mapper, ILogger<ServiceToService> logger)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<long> AddService(ServiceModel serviceModel)
        {
            _logger.LogInformation("запрос на добавление услуги");

            var service = _mapper.Map<Service>(serviceModel);          

            return await _serviceRepository.AddService(service);
        }

        public async Task<ServiceModel> GetServiceById(long id)
        {
            _logger.LogInformation("запрос на получение услуги по id");
           
            var service = await _serviceRepository.GetServiceById(id);

            if (service == null)
            {
                _logger.LogError("Ошибка в получении услуги по Id ");

                throw new NotFoundServiceException("Такой услуги не существует.");
            }
           
            return _mapper.Map<ServiceModel>(service);
        }

        public async Task<ServicePaymentModel> GetTransactionByServiceToLeadId(long id)
        {
            _logger.LogInformation("запрос на получение услуги по id");

            var service = await _serviceRepository.GetTransactionByServiceToleadId(id);

            if (service == null)
            {
                _logger.LogError("Ошибка в получении услуги по Id ");

                throw new NotFoundServiceException("Такой услуги не существует.");
            }

            return _mapper.Map<ServicePaymentModel>(service);
        }

        public async Task SoftDelete(long id, ServiceModel serviceModel)
        {
            _logger.LogInformation("запрос на удаление услуги");

            var oldService = await _serviceRepository.GetServiceById(id);

            if (oldService == null)
            {
                _logger.LogError("Ошибка в получении услуги по Id ");

                throw new NotFoundServiceException("Такой услуги не существует.");
            }           

            var service = _mapper.Map<Service>(serviceModel);

            await _serviceRepository.SoftDelete(id, service);
        }

        public async Task UpdateService(long id, ServiceModel serviceModel)
        {
            _logger.LogInformation("запрос на изменение услуги");

            var oldService = await _serviceRepository.GetServiceById(id);

            if (oldService == null)
            {
                _logger.LogError("Ошибка в получении услуги по Id ");

                throw new NotFoundServiceException("Такой услуги не существует.");
            }

            _logger.LogInformation("запрос на изменение услуги прошел успешно");

            var service = _mapper.Map<Service>(serviceModel);

            await _serviceRepository.UpdateService(id, service);
        }
    }
}
