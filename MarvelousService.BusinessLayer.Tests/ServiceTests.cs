using AutoMapper;
using Marvelous.Contracts;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services;
using MarvelousService.BusinessLayer.Tests.TestCaseSource;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvelousService.BusinessLayer.Tests
{
    public class ServiceTests
    {

        private Mock<IServiceRepository> _serviceRepositoryMock;
        private readonly ServiceToServiceTestCaseSource _serviceTest;
        private readonly IMapper _autoMapper;
        private readonly Mock<ILogger<ServiceToService>> _logger;


        public ServiceTests()
        {
            _serviceRepositoryMock = new Mock<IServiceRepository>();
            _serviceTest = new ServiceToServiceTestCaseSource();
            _autoMapper = new Mapper(
                new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _logger = new Mock<ILogger<ServiceToService>>();
        }

        [SetUp]
        public async Task Setup()
        {
            _serviceRepositoryMock = new Mock<IServiceRepository>();
        }

        [Test]
        public async Task AddServiceTest()
        {
            //given
            var serviceModel = _serviceTest.AddServiceModelTest();
            var service = new Service();
            _serviceRepositoryMock.Setup(m => m.GetServiceById(It.IsAny<int>())).ReturnsAsync(service);
            var sut = new ServiceToService( _serviceRepositoryMock.Object, _autoMapper, _logger.Object);
            int role = 3;
            //when
            sut.AddService(serviceModel, role);

            //then
            _serviceRepositoryMock.Verify(m => m.AddService(It.IsAny<Service>()), Times.Once());
        }
        [Test]
        public async Task UpdateServiceTest()
        {
            //given
            var serviceModel = _serviceTest.AddServiceTest();
            _serviceRepositoryMock.Setup(m => m.GetServiceById(It.IsAny<int>())).ReturnsAsync(serviceModel);
            var sut = new ServiceToService(_serviceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            sut.UpdateService(1, new ServiceModel());

            //then
            _serviceRepositoryMock.Verify(m => m.GetServiceById(It.IsAny<int>()), Times.Once());
            _serviceRepositoryMock.Verify(m => m.UpdateService(It.IsAny<Service>()), Times.Once());
        }

        [Test]
        public async Task DeletedServiceTest()
        {
            //given
            var service = new Service();
            _serviceRepositoryMock.Setup(m => m.GetServiceById(It.IsAny<int>())).ReturnsAsync(service);
            var sut = new ServiceToService(_serviceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            sut.SoftDelete(1, new ServiceModel());

            //then
            _serviceRepositoryMock.Verify(m => m.GetServiceById(It.IsAny<int>()), Times.Once());
            _serviceRepositoryMock.Verify(m => m.SoftDelete(It.IsAny<Service>()), Times.Once()); ;
        }


        [Test]
        public async Task GetByIdTest()
        {
            //given
            var service = _serviceTest.AddServiceTest();
            _serviceRepositoryMock.Setup(m => m.GetServiceById(It.IsAny<int>())).ReturnsAsync(service);
            var sut = new ServiceToService(_serviceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            var actual = sut.GetServiceById(It.IsAny<int>()).Result;

            //then
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Id);
            Assert.IsNotNull(actual.Name);
            Assert.IsNotNull(actual.Price);
            Assert.IsNotNull(actual.IsDeleted);
            Assert.IsNotNull(actual.Description);

        }

    }
}