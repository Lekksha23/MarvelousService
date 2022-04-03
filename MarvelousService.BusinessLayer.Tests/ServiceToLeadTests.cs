using AutoMapper;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Services;
using MarvelousService.BusinessLayer.Tests.TestCaseSource;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarvelousService.BusinessLayer.Tests
{
    public class ServiceToLeadTests
    {
        private ITransactionStoreClient _transactionStoreClient;
        private IResourceRepository _serviceRepositoryMock;
        private Mock<ILeadResourceRepository> _serviceToLeadRepositoryMock;
        private readonly ServiceToLeadTestCaseSource _serviceToLeadTest;
        private readonly IMapper _autoMapper;
        private readonly Mock<ILogger<LeadResourceService>> _logger;

        public ServiceToLeadTests()
        {
            
            _serviceToLeadRepositoryMock = new Mock<ILeadResourceRepository>();
            _serviceToLeadTest = new ServiceToLeadTestCaseSource();
            _autoMapper = new Mapper(
                new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _logger = new Mock<ILogger<LeadResourceService>>();
        }

        [SetUp]
        public async Task Setup()
        {
            _serviceToLeadRepositoryMock = new Mock<ILeadResourceRepository>();
        }




    }
}
