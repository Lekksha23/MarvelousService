using AutoMapper;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services;
using MarvelousService.BusinessLayer.Tests.TestCaseSource;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MarvelousService.BusinessLayer.Tests
{
    public class ServiceTests
    {
        private ServiceToService _service;
        private Mock<IServiceRepository> _serviceRepositoryMock;
        private ServiceRepository _serviceRepository;
        private IMapper _mapper;
        private ILogger<ServiceToService> _logger;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _serviceRepositoryMock = new Mock<IServiceRepository>();
            _service = new ServiceToService(_serviceRepositoryMock.Object, _mapper, _logger);
        }

        [TestCaseSource(typeof(GetServiceByIdTestCaseSource))]
        public void GetServiceByIdTest(Service services, ServiceModel expected)
        {
            var id =  1;
            //given
            _serviceRepositoryMock.Setup(g => g.GetServiceById(id)).Returns(services);

            //when
            var actual = _service.GetServiceById(id);

            //then
            Assert.AreEqual(actual.Id, expected.Id);
            Assert.AreEqual(actual.Name, expected.Name);
            Assert.AreEqual(actual.Description, expected.Description);
            Assert.AreEqual(actual.Price, expected.Price);
            
            _serviceRepositoryMock.Verify(g => g.GetServiceById(id), Times.Once);
        }

        [TestCaseSource(typeof(AddServiceTestCaseSourse))]
        public void AddServiceTest(ServiceModel services, int expected)
        {
            //given
            _serviceRepositoryMock.Setup(a => a.AddService(It.IsAny<Service>())).Returns(expected);

            //when
            var actual = _service.AddService(services);

            //then
            _serviceRepositoryMock.Verify(a => a.AddService(It.IsAny<Service>()), Times.Once);
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(typeof(UpdateServiceTestCaseSourse))]
        public void UpdateServiceTest(ServiceModel services, ServiceModel updateService , ServiceModel expected)
        {
            //given
            _serviceRepositoryMock.Setup(a => a.AddService(It.IsAny<Service>())).Returns(expected.Id);

            var newService = _service.AddService(services);

            //when
            _service.UpdateService(updateService.Id, updateService);

            var actual = _service.GetServiceById(newService);
            //then

            _serviceRepositoryMock.Verify(a => a.AddService(It.IsAny<Service>()), Times.Once);
            Assert.AreEqual(expected, actual);

        }


    }
}