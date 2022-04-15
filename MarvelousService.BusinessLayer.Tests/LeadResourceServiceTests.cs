using AutoMapper;
using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Clients;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Tests.TestCaseSource;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using MarvelousService.DataLayer.Repositories;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarvelousService.BusinessLayer.Tests
{
    public class LeadResourceServiceTests
    {
        private Mock<ILeadResourceRepository> _leadResourceRepositoryMock;
        private Mock<IResourcePaymentRepository> _resourcePaymentRepositoryMock;
        private Mock<ITransactionService> _transactionServiceMock;
        private Mock<ICRMService> _crmServiceMock;
        private readonly IRoleStrategy _roleStrategy;
        private readonly IRoleStrategyProvider _roleStrategyProvider;
        private readonly IMapper _autoMapper;

        public LeadResourceServiceTests()
        {
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _roleStrategyProvider = new RoleStrategyProvider();
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
        public async Task AddLeadResource_WeekPeriodAndVipLead_ShouldCountPriceForWeekAndGiveDiscount()
        {
            // given
            var expectedPrice = 3240.0M;

            var leadResource = LeadResourceTestData.GetLeadResourceWithWeekPeriodForTests();
            var leadResourceModel = LeadResourceTestData.GetLeadResourceModelWithWeekPeriodForTests();
            leadResource.Price = expectedPrice;
            var token = "IndicativeToken";
            var leadResourceId = 23;
            var resourcePaymentId = 42;
            var transactionId = 40000000;
            var accountId = 4444444;
            _crmServiceMock.Setup(m => m.GetIdOfRubLeadAccount(token)).ReturnsAsync(accountId);
            _transactionServiceMock.Setup(m => m.AddResourceTransaction(accountId, expectedPrice)).ReturnsAsync(transactionId);
            _leadResourceRepositoryMock.Setup(m => m.AddLeadResource(leadResource)).ReturnsAsync(leadResourceId);
            _resourcePaymentRepositoryMock.Setup(m => m.AddResourcePayment(leadResourceId, transactionId)).ReturnsAsync(resourcePaymentId);

            var sut = new LeadResourceService(
                _leadResourceRepositoryMock.Object, 
                _resourcePaymentRepositoryMock.Object, 
                _transactionServiceMock.Object, 
                _crmServiceMock.Object,
                _roleStrategy, 
                _roleStrategyProvider,
                _autoMapper);

            // when
            var actual = await sut.AddLeadResource(leadResourceModel, Role.Vip, token);

            // then
            Assert.NotNull(actual);
            Assert.AreEqual(expectedPrice, leadResourceModel.Price);
            _crmServiceMock.Verify(m => m.GetIdOfRubLeadAccount(token), Times.Once());
            _transactionServiceMock.Verify(m => m.AddResourceTransaction(accountId, leadResourceModel.Price), Times.Once());
            _leadResourceRepositoryMock.Verify(m => m.AddLeadResource(leadResource), Times.Once());
            _resourcePaymentRepositoryMock.Verify(m => m.AddResourcePayment(leadResourceId, transactionId), Times.Once());
        }

        [Test]
        public async Task AddLeadResource_WeekPeriodAndRegularLead_ShouldCountPriceForWeek()
        {
            // given
            var expectedPrice = 3600.0M;

            var leadResource = LeadResourceTestData.GetLeadResourceWithWeekPeriodForTests();
            var leadResourceModel = LeadResourceTestData.GetLeadResourceModelWithWeekPeriodForTests();
            leadResource.Price = expectedPrice;
            leadResource.Status = Status.Active;
            var token = "IndicativeToken";
            var leadResourceId = 23;
            var resourcePaymentId = 42;
            var transactionId = 40000000;
            var accountId = 4444444;
            _crmServiceMock.Setup(m => m.GetIdOfRubLeadAccount(token)).ReturnsAsync(accountId);
            _transactionServiceMock.Setup(m => m.AddResourceTransaction(accountId, expectedPrice)).ReturnsAsync(transactionId);
            _leadResourceRepositoryMock.Setup(m => m.AddLeadResource(leadResource)).ReturnsAsync(leadResourceId);
            _resourcePaymentRepositoryMock.Setup(m => m.AddResourcePayment(leadResourceId, transactionId)).ReturnsAsync(resourcePaymentId);

            var sut = new LeadResourceService(
                _leadResourceRepositoryMock.Object,
                _resourcePaymentRepositoryMock.Object,
                _transactionServiceMock.Object,
                _crmServiceMock.Object,
                _roleStrategy,
                _roleStrategyProvider,
                _autoMapper);

            // when
            var actual = await sut.AddLeadResource(leadResourceModel, Role.Regular, token);

            // then
            Assert.NotNull(actual);
            Assert.AreEqual(expectedPrice, leadResourceModel.Price);
            _crmServiceMock.Verify(m => m.GetIdOfRubLeadAccount(token), Times.Once());
            _transactionServiceMock.Verify(m => m.AddResourceTransaction(accountId, leadResourceModel.Price), Times.Once());
            _leadResourceRepositoryMock.Verify(m => m.AddLeadResource(It.IsAny<LeadResource>()), Times.Once());
            _resourcePaymentRepositoryMock.Verify(m => m.AddResourcePayment(0, transactionId), Times.Once());
        }

        [Test]
        public async Task AddLeadResource_OneTimePeriodAndVipLead_ShouldCountPriceForOneTimeAndGiveDiscount()
        {
            // given
            var expectedPrice = 1620.0M;

            var leadResource = LeadResourceTestData.GetLeadResourceWithOneTimePeriodForTests();
            var leadResourceModel = LeadResourceTestData.GetLeadResourceModelWithOneTimePeriodForTests();
            leadResource.Price = expectedPrice;
            leadResource.Status = Status.Active;
            var token = "IndicativeToken";
            var leadResourceId = 23;
            var resourcePaymentId = 42;
            var transactionId = 40000000;
            var accountId = 4444444;
            _crmServiceMock.Setup(m => m.GetIdOfRubLeadAccount(token)).ReturnsAsync(accountId);
            _transactionServiceMock.Setup(m => m.AddResourceTransaction(accountId, expectedPrice)).ReturnsAsync(transactionId);
            _leadResourceRepositoryMock.Setup(m => m.AddLeadResource(leadResource)).ReturnsAsync(leadResourceId);
            _resourcePaymentRepositoryMock.Setup(m => m.AddResourcePayment(leadResourceId, transactionId)).ReturnsAsync(resourcePaymentId);

            var sut = new LeadResourceService(
                _leadResourceRepositoryMock.Object,
                _resourcePaymentRepositoryMock.Object,
                _transactionServiceMock.Object,
                _crmServiceMock.Object,
                _roleStrategy,
                _roleStrategyProvider,
                _autoMapper);

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
        public async Task AddLeadResource_OneTimePeriodAndRegularLead_ShouldCountPriceForOneTime()
        {
            // given
            var expectedPrice = 1800.0M;

            var leadResource = LeadResourceTestData.GetLeadResourceWithOneTimePeriodForTests();
            var leadResourceModel = LeadResourceTestData.GetLeadResourceModelWithOneTimePeriodForTests();
            leadResource.Price = expectedPrice;
            leadResource.Status = Status.Active;
            var token = "IndicativeToken";
            var leadResourceId = 23;
            var resourcePaymentId = 42;
            var transactionId = 40000000;
            var accountId = 4444444;
            _crmServiceMock.Setup(m => m.GetIdOfRubLeadAccount(token)).ReturnsAsync(accountId);
            _transactionServiceMock.Setup(m => m.AddResourceTransaction(accountId, expectedPrice)).ReturnsAsync(transactionId);
            _leadResourceRepositoryMock.Setup(m => m.AddLeadResource(leadResource)).ReturnsAsync(leadResourceId);
            _resourcePaymentRepositoryMock.Setup(m => m.AddResourcePayment(leadResourceId, transactionId)).ReturnsAsync(resourcePaymentId);

            var sut = new LeadResourceService(
                _leadResourceRepositoryMock.Object,
                _resourcePaymentRepositoryMock.Object,
                _transactionServiceMock.Object,
                _crmServiceMock.Object,
                _roleStrategy,
                _roleStrategyProvider,
                _autoMapper);

            // when
            var actual = await sut.AddLeadResource(leadResourceModel, Role.Regular, token);

            // then
            Assert.NotNull(actual);
            Assert.AreEqual(expectedPrice, leadResourceModel.Price);
            _crmServiceMock.Verify(m => m.GetIdOfRubLeadAccount(token), Times.Once());
            _transactionServiceMock.Verify(m => m.AddResourceTransaction(accountId, leadResourceModel.Price), Times.Once());
            _leadResourceRepositoryMock.Verify(m => m.AddLeadResource(It.IsAny<LeadResource>()), Times.Once());
            _resourcePaymentRepositoryMock.Verify(m => m.AddResourcePayment(0, transactionId), Times.Once());
        }

        [Test]
        public void AddLeadResource_LeadWithRoleAdmin_ShouldThrowRoleException()
        {
            // given
            var leadResourceModel = LeadResourceTestData.GetLeadResourceModelWithOneTimePeriodForTests();
            var token = "IndicativeToken";

            var sut = new LeadResourceService(
                _leadResourceRepositoryMock.Object,
                _resourcePaymentRepositoryMock.Object,
                _transactionServiceMock.Object,
                _crmServiceMock.Object,
                _roleStrategy,
                _roleStrategyProvider,
                _autoMapper);

            // then
            Assert.ThrowsAsync<RoleException>(async () => await sut.AddLeadResource(leadResourceModel, Role.Admin, token));
        }

        [Test]
        public async Task GetByLeadId_ShouldReturnListOfLeadResourceModels()
        {
            // given
            var leadId= 23;
            var leadResources = LeadResourceTestData.GetLeadResourceListForTests();
            _leadResourceRepositoryMock.Setup(m => m.GetByLeadId(leadId)).ReturnsAsync(leadResources);

            var sut = new LeadResourceService(
                _leadResourceRepositoryMock.Object,
                _resourcePaymentRepositoryMock.Object,
                _transactionServiceMock.Object,
                _crmServiceMock.Object,
                _roleStrategy,
                _roleStrategyProvider,
                _autoMapper);

            // when
            var actual = await sut.GetByLeadId(leadId);

            // then
            Assert.NotNull(actual);
            Assert.AreEqual(leadResources.Count, actual.Count);
            Assert.IsTrue(actual[0].GetType() == typeof(LeadResourceModel));
            _leadResourceRepositoryMock.Verify(m => m.GetByLeadId(leadId), Times.Once());
        }
    }
}
