using AutoMapper;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Clients
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

        public async Task<List<ResourcePaymentModel>> GetResourcePaymentsById(int leadResourceId)
        {
            _logger.LogInformation("Query for receiving Resource payments by id");
            var resourcePayments = await _resourcePaymentRepository.GetResourcePaymentsByLeadResourceId(leadResourceId);
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
