using AutoMapper;
using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Clients;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Tests.TestCaseSource;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using MarvelousService.DataLayer.Repositories;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarvelousService.BusinessLayer.Tests
{
    public class LeadResourceTests
    {
        private Mock<ILeadResourceRepository> _leadResourceRepositoryMock;
        private Mock<IResourcePaymentRepository> _resourcePaymentRepositoryMock;
        private Mock<ITransactionService> _transactionServiceMock;
        private Mock<ICRMService> _crmServiceMock;
        private readonly IRoleStrategy _roleStrategy;
        private readonly IRoleStrategyProvider _roleStrategyProvider;
        private readonly Mock<ICheckErrorHelper> _helper;
        private readonly LeadResourceTestData _leadResourceTestData;
        private readonly IMapper _autoMapper;

        public LeadResourceTests()
        {
            _leadResourceTestData = new LeadResourceTestData();
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _roleStrategyProvider = new RoleStrategyProvider();
            _helper = new Mock<ICheckErrorHelper>();
        }

        [SetUp]
        public void Setup()
        {
            _leadResourceRepositoryMock = new Mock<ILeadResourceRepository>();
            _resourcePaymentRepositoryMock = new Mock<IResourcePaymentRepository>();
            _transactionServiceMock = new Mock<ITransactionService>();
            _crmServiceMock = new Mock<ICRMService>();
        }

        [Test]
        public async Task AddLeadResource_ShouldCountPriceForWeekAndGiveDiscountToVipLead_ShouldCallCRMAndTransactionServices()
        {
            // given
            var expectedPrice = 3240.0M;

            var leadResource = _leadResourceTestData.GetLeadResourceWithWeekPeriodForTests();
            var leadResourceModel = _leadResourceTestData.GetLeadResourceModelWithWeekPeriodForTests();
            var mappedLeadResource = _autoMapper.Map<LeadResource>(leadResourceModel);
            mappedLeadResource.Price = expectedPrice;
            mappedLeadResource.Status = Status.Active;
            var token = "IndicativeToken";
            var leadResourceId = 23;
            var resourcePaymentId = 42;
            var transactionId = 40000000;
            var accountId = 4444444;
            _crmServiceMock.Setup(m => m.GetIdOfRubLeadAccount(token)).ReturnsAsync(accountId);
            _transactionServiceMock.Setup(m => m.AddResourceTransaction(accountId, expectedPrice)).ReturnsAsync(transactionId);
            _leadResourceRepositoryMock.Setup(m => m.AddLeadResource(mappedLeadResource)).ReturnsAsync(leadResourceId);
            _resourcePaymentRepositoryMock.Setup(m => m.AddResourcePayment(leadResourceId, transactionId)).ReturnsAsync(resourcePaymentId);

            var sut = new LeadResourceService(
                _leadResourceRepositoryMock.Object, 
                _resourcePaymentRepositoryMock.Object, 
                _transactionServiceMock.Object, 
                _crmServiceMock.Object,
                _roleStrategy, 
                _roleStrategyProvider,
                _autoMapper,
                _helper.Object);

            // when
            var actual = await sut.AddLeadResource(leadResourceModel, Role.Vip, token);

            // then
            Assert.NotNull(actual);
            Assert.AreEqual(expectedPrice, leadResourceModel.Price);
            _crmServiceMock.Verify(m => m.GetIdOfRubLeadAccount(token), Times.Once());
            _transactionServiceMock.Verify(m => m.AddResourceTransaction(accountId, leadResourceModel.Price), Times.Once());
            _leadResourceRepositoryMock.Verify(m => m.AddLeadResource(It.IsAny<LeadResource>()), Times.Once());
            _resourcePaymentRepositoryMock.Verify(m => m.AddResourcePayment(0, transactionId), Times.Once());
        }

        [Test]
        public async Task GetByLeadId_ShouldReturnListOfLeadResourceModels()
        {
            // given
            var leadId= 23;
            var leadResources = _leadResourceTestData.GetLeadResourceListForTests();
            _leadResourceRepositoryMock.Setup(m => m.GetByLeadId(leadId)).ReturnsAsync(leadResources);

            var sut = new LeadResourceService(
                _leadResourceRepositoryMock.Object,
                _resourcePaymentRepositoryMock.Object,
                _transactionServiceMock.Object,
                _crmServiceMock.Object,
                _roleStrategy,
                _roleStrategyProvider,
                _autoMapper,
                _helper.Object);

            // when
            var actual = await sut.GetByLeadId(leadId);

            // then
            Assert.NotNull(actual);
            Assert.AreEqual(leadResources.Count, actual.Count);
            _leadResourceRepositoryMock.Verify(m => m.GetByLeadId(leadId), Times.Once());
        }
    }
}
