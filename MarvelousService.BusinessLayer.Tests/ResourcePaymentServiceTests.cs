using AutoMapper;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Clients;
using MarvelousService.BusinessLayer.Tests.TestData;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvelousService.BusinessLayer.Tests
{
    public class ResourcePaymentServiceTests
    {
        private Mock<IResourcePaymentRepository> _resourcePaymentRepositoryMock;
        private readonly ResourcePaymentTestData _resourcePaymentTestData;
        private readonly Mock<ILogger<ResourcePaymentService>> _logger;
        private readonly Mock<ICheckErrorHelper> _helperMock;
        private readonly IMapper _autoMapper;

        public ResourcePaymentServiceTests()
        {
            _resourcePaymentTestData = new ResourcePaymentTestData();
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _logger = new Mock<ILogger<ResourcePaymentService>>();
            _helperMock = new Mock<ICheckErrorHelper>();
        }

        [SetUp]
        public void Setup()
        {
            _resourcePaymentRepositoryMock = new Mock<IResourcePaymentRepository>();
        }

        [Test]
        public async Task GetResourcePaymentsById_ShouldReturnListOfPayments()
        {
            // given
            var leadResourceId = 23;
            var listOfResourcePayments = _resourcePaymentTestData.GetListOfServicePaymentsForTests();
            _resourcePaymentRepositoryMock.Setup(m => m.GetResourcePaymentsByLeadResourceId(leadResourceId)).ReturnsAsync(listOfResourcePayments);
            var sut = new ResourcePaymentService(_resourcePaymentRepositoryMock.Object, _autoMapper, _logger.Object);

            // when
            var actual = await sut.GetResourcePaymentsById(leadResourceId);

            // then
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count > 0);
            Assert.IsTrue(actual.Count == listOfResourcePayments.Count);
            _resourcePaymentRepositoryMock.Verify(m => m.GetResourcePaymentsByLeadResourceId(leadResourceId), Times.Once());
        }

        [Test]
        public async Task GetResourcePaymentsById_ShouldThrowNotFoundServiceException()
        {
            // given 
            var leadResourceId = 23;
            _resourcePaymentRepositoryMock.Setup(m => m.GetResourcePaymentsByLeadResourceId(leadResourceId)).ReturnsAsync((List<ResourcePayment>)null);
            var sut = new ResourcePaymentService(_resourcePaymentRepositoryMock.Object, _autoMapper, _logger.Object);

            // then
            Assert.ThrowsAsync<NotFoundServiceException>(async () => await sut.GetResourcePaymentsById(leadResourceId));
        }
    }
}
