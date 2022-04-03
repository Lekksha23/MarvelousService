using AutoMapper;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Services
{
    public class ResourcePaymentService : IResourcePaymentService
    {
        private readonly IResourcePaymentRepository _resourcePaymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ResourcePaymentService> _logger;

        public ResourcePaymentService(IResourcePaymentRepository servicePaymentRepository, IMapper mapper, ILogger<ResourcePaymentService> logger)
        {
            _resourcePaymentRepository = servicePaymentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddResourcePayment(ResourcePaymentModel servicePaymentModel)
        {
            _logger.LogInformation("Query for adding Resource payment");
            var resourcePayment = _mapper.Map<ResourcePayment>(servicePaymentModel);
            var id = await _resourcePaymentRepository.AddResourcePayment(resourcePayment);
            _logger.LogInformation($"Resource payment was added. LeadResourceId = {resourcePayment.LeadResource.Id}");
            return id;
        }

        public async Task<List<ResourcePaymentModel>> GetResourcePaymentsById(int serviceToLeadId)
        {
            _logger.LogInformation("Query for receiving Resource payments by id");
            var resourcePayments = await _resourcePaymentRepository.GetResourcePaymentsByLeadResourceId(serviceToLeadId);
            CheckResourcePayments(resourcePayments);
            return _mapper.Map<List<ResourcePaymentModel>>(resourcePayments);
        }

        private void CheckResourcePayments(List<ResourcePayment> resourcePayments)
        {
            if (resourcePayments is null)
            {
                _logger.LogError("Error in receiving information about Resource payment");
                throw new NotFoundServiceException("Resource payment not found");
            }
        }
    }
}
