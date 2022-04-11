using AutoMapper;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Clients;
using MarvelousService.BusinessLayer.Tests.TestCaseSource;
using MarvelousService.DataLayer.Repositories;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarvelousService.BusinessLayer.Tests
{
    public class LeadResourceTests
    {
        private ITransactionStoreClient _transactionStoreClient;
        private IResourceRepository _ResourceRepositoryMock;
        private Mock<ILeadResourceRepository> _leadResourceRepositoryMock;
        private readonly LeadResourceTestCaseSource _leadResourceTest;
        private readonly IMapper _autoMapper;
        private readonly Mock<ILogger<LeadResourceService>> _logger;

        public LeadResourceTests()
        {
            _leadResourceTest = new LeadResourceTestCaseSource();
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _logger = new Mock<ILogger<LeadResourceService>>();
        }

        [SetUp]
        public async Task Setup()
        {
            _leadResourceRepositoryMock = new Mock<ILeadResourceRepository>();
        }

    }
}
