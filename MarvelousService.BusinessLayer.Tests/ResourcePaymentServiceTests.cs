using AutoMapper;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services;
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
        private readonly IMapper _autoMapper;
        private readonly Mock<ILogger<ResourcePaymentService>> _logger;

        public ResourcePaymentServiceTests()
        {
            _resourcePaymentTestData = new ResourcePaymentTestData();
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _logger = new Mock<ILogger<ResourcePaymentService>>();
        }

        [SetUp]
        public void Setup()
        {
            _resourcePaymentRepositoryMock = new Mock<IResourcePaymentRepository>();
        }

        [Test]
        public async Task AddResourcePayment()
        {
            // given
            _resourcePaymentRepositoryMock.Setup(m => m.AddResourcePayment(It.IsAny<ResourcePayment>())).ReturnsAsync(23);
            var sut = new ResourcePaymentService(_resourcePaymentRepositoryMock.Object, _autoMapper, _logger.Object);

            // when
            sut.AddResourcePayment(new ResourcePaymentModel());

            // then
            _resourcePaymentRepositoryMock.Verify(m => m.AddResourcePayment(It.IsAny<ResourcePayment>()), Times.Once());
        }

        [Test]
        public async Task GetServicePaymentsById_ShouldReturnListOfPayments()
        {
            // given
            var listOfResourcePayments = _resourcePaymentTestData.GetListOfServicePaymentsForTests();
            _resourcePaymentRepositoryMock.Setup(m => m.GetResourcePaymentsByLeadResourceId(It.IsAny<int>())).ReturnsAsync(listOfResourcePayments);
            var sut = new ResourcePaymentService(_resourcePaymentRepositoryMock.Object, _autoMapper, _logger.Object);

            // when
            var actual = await sut.GetResourcePaymentsById(23);

            // then
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count > 0);
            Assert.IsTrue(actual.Count == listOfResourcePayments.Count);
            _resourcePaymentRepositoryMock.Verify(m => m.GetResourcePaymentsByLeadResourceId(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public async Task GetServicePaymentsById_ShouldThrowNotFoundServiceException()
        {
            // given 
            _resourcePaymentRepositoryMock.Setup(m => m.GetResourcePaymentsByLeadResourceId(It.IsAny<int>())).ReturnsAsync((List<ResourcePayment>)null);
            var sut = new ResourcePaymentService(_resourcePaymentRepositoryMock.Object, _autoMapper, _logger.Object);

            // when

            // then
            Assert.ThrowsAsync<NotFoundServiceException>(async () => await sut.GetResourcePaymentsById(It.IsAny<int>()));
        }
    }
}
