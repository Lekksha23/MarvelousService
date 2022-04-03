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
    public class ServicePaymentServiceTests
    {
        private Mock<IResourcePaymentRepository> _servicePaymentRepositoryMock;
        private readonly ServicePaymentTestData _servicePaymentTestData;
        private readonly IMapper _autoMapper;
        private readonly Mock<ILogger<ResourcePaymentService>> _logger;

        public ServicePaymentServiceTests()
        {
            _servicePaymentTestData = new ServicePaymentTestData();
            _autoMapper = new Mapper(
                new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _logger = new Mock<ILogger<ResourcePaymentService>>();
        }

        [SetUp]
        public void Setup()
        {
            _servicePaymentRepositoryMock = new Mock<IResourcePaymentRepository>();
        }

        [Test]
        public async Task AddServicePayment()
        {
            // given
            _servicePaymentRepositoryMock.Setup(m => m.AddServicePayment(It.IsAny<ResourcePayment>())).ReturnsAsync(23);
            var sut = new ResourcePaymentService(_servicePaymentRepositoryMock.Object, _autoMapper, _logger.Object);

            // when
            sut.AddResourcePayment(new ResourcePaymentModel());

            // then
            _servicePaymentRepositoryMock.Verify(m => m.AddServicePayment(It.IsAny<ResourcePayment>()), Times.Once());
        }

        [Test]
        public async Task GetServicePaymentsById_ShouldReturnListOfPayments()
        {
            // given
            var listOfServicePayments = _servicePaymentTestData.GetListOfServicePaymentsForTests();
            _servicePaymentRepositoryMock.Setup(m => m.GetServicePaymentsByServiceToLeadId(It.IsAny<int>())).ReturnsAsync(listOfServicePayments);
            var sut = new ResourcePaymentService(_servicePaymentRepositoryMock.Object, _autoMapper, _logger.Object);

            // when
            var actual = await sut.GetResourcePaymentsById(23);

            // then
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count > 0);
            Assert.IsTrue(actual.Count == listOfServicePayments.Count);
            _servicePaymentRepositoryMock.Verify(m => m.GetServicePaymentsByServiceToLeadId(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public async Task GetServicePaymentsById_ShouldThrowNotFoundServiceException()
        {
            // given 
            _servicePaymentRepositoryMock.Setup(m => m.GetServicePaymentsByServiceToLeadId(It.IsAny<int>())).ReturnsAsync((List<ResourcePayment>)null);
            var sut = new ResourcePaymentService(_servicePaymentRepositoryMock.Object, _autoMapper, _logger.Object);

            // when

            // then
            Assert.ThrowsAsync<NotFoundServiceException>(async () => await sut.GetResourcePaymentsById(It.IsAny<int>()));
        }
    }
}
