using AutoMapper;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Repositories;
using Microsoft.Extensions.Logging;

namespace MarvelousService.BusinessLayer.Services
{
    public class ResourcePaymentService : IResourcePaymentService
    {
        private readonly IResourcePaymentRepository _resourcePaymentRepository;
        private readonly IMapper _mapper;
        private readonly IHelper _helper;
        private readonly ILogger<ResourcePaymentService> _logger;

        public ResourcePaymentService(
            IResourcePaymentRepository servicePaymentRepository,
            ILogger<ResourcePaymentService> logger,
            IMapper mapper,
            IHelper helper)
        {
            _resourcePaymentRepository = servicePaymentRepository;
            _mapper = mapper;
            _logger = logger;
            _helper = helper;
        }

        public async Task<List<ResourcePaymentModel>> GetResourcePaymentsById(int leadResourceId)
        {
            _logger.LogInformation("Query for receiving Resource payments by id");
            var resourcePayments = await _resourcePaymentRepository.GetResourcePaymentsByLeadResourceId(leadResourceId);
            _helper.CheckIfResourcePaymentsIsNull(resourcePayments);
            return _mapper.Map<List<ResourcePaymentModel>>(resourcePayments);
        }
    }
}
