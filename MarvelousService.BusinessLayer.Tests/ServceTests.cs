using AutoMapper;
using CRM.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services;
using MarvelousService.BusinessLayer.Tests.TestCaseSource;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarvelousService.BusinessLayer.Tests
{
    public class ServiceTests
    {
        private ServiceService _service;
        private Mock<IServiceRepository> _serviceRepositoryMock;
        private ServiceRepository _serviceRepository;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _serviceRepositoryMock = new Mock<IServiceRepository>();
            _service = new ServiceService(_serviceRepositoryMock.Object, _mapper);
        }

        [TestCaseSource(typeof(GetServiceByIdTestCaseSource))]
        public void GetServiceByIdTest(Service services, ServiceModel expected)
        {
            var id =  1;
            //given
            _serviceRepositoryMock.Setup(w => w.GetServiceById(id)).Returns(services);

            //when
            var actual = _service.GetServiceById(id);

            //then
            Assert.AreEqual(actual.Id, expected.Id);
            Assert.AreEqual(actual.Name, expected.Name);
            Assert.AreEqual(actual.Description, expected.Description);
            Assert.AreEqual(actual.OneTimePrice, expected.OneTimePrice);
            
            _serviceRepositoryMock.Verify(s => s.GetServiceById(id), Times.Once);
        }
    }
}