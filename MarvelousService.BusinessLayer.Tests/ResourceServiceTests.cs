using AutoMapper;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Clients;
using MarvelousService.BusinessLayer.Tests.TestCaseSource;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using MarvelousService.BusinessLayer.Exceptions;
using System.Collections.Generic;

namespace MarvelousService.BusinessLayer.Tests
{
    public class ResourceServiceTests
    {
        private Mock<IResourceRepository> _resourceRepositoryMock;
        private readonly ResourceServiceTestCaseSource _resourceTestData;
        private readonly IMapper _autoMapper;
        private readonly Mock<ILogger<ResourceService>> _logger;

        public ResourceServiceTests()
        {
            _resourceRepositoryMock = new Mock<IResourceRepository>();
            _resourceTestData = new ResourceServiceTestCaseSource();
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _logger = new Mock<ILogger<ResourceService>>();
        }

        [SetUp]
        public void Setup()
        {
            _resourceRepositoryMock = new Mock<IResourceRepository>();
        }

        [Test]
        public async Task TestingServiceAddons()
        {
            //given
            var resourceId = 3;
            var resourceModel = new ResourceModel
            {
                Id = 3,
                Name = "qwe",
                Description = "QWEQWE",
                Price = 1500,
                IsDeleted = false
            };

            var resource = new Resource
            {
                Id = 3,
                Name = "qwe",
                Description = "QWEQWE",
                Price = 1500,
                IsDeleted = false,
            };
            _resourceRepositoryMock.Setup(m => m.GetResourceById(resourceId)).ReturnsAsync(resource);
            var resourceService = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            resourceService.AddResource(resourceModel); 

            var actual = await resourceService.GetResourceById(3);

            //then
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Id);
            Assert.IsNotNull(actual.Name);
            Assert.IsNotNull(actual.Price);
            Assert.IsNotNull(actual.IsDeleted);
            Assert.IsNotNull(actual.Description);
            
        }

        [Test]
        public void ResourceNegativeTestIssueAnDuplicationException()
        {
            //given
            var resourceId = 1;
            var resourceModel = _resourceTestData.AddResourceModelTest();
            var resource = new Resource();
            _resourceRepositoryMock.Setup(m => m.GetResourceById(resourceId)).ReturnsAsync(resource);
            //when
            var resourceService = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //then
            Assert.ThrowsAsync<DuplicationException>(async () => await resourceService.AddResource(resourceModel));
        }

        [Test]
        public async Task TestingServiceChange()
        {
            //given
            var resourceId = 3;
            var resource = new Resource
            {
                Id = 3,
                Name = "qwe",
                Description = "QWEQWE",
                Price = 1500,
                IsDeleted = false,
            };
            _resourceRepositoryMock.Setup(m => m.GetResourceById(resourceId)).ReturnsAsync(resource);
            var resourceService = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            var resourceNew = new ResourceModel
            {
                Id = 3,
                Name = "ewq",
                Description = "EWQEWQ",
                Price = 3000,
                IsDeleted = false,
            };
            await resourceService.UpdateResource(resourceId, resourceNew);

            //then
            _resourceRepositoryMock.Verify(m => m.GetResourceById(It.IsAny<int>()), Times.Once());
            _resourceRepositoryMock.Verify(m => m.UpdateResource(It.IsAny<Resource>()), Times.Once());
            VerifyLoggerHelperService.LoggerVerify(_logger, "Request for updating a status of Resource by id 3", LogLevel.Information);
        }

        [Test]
        public async Task ResourceNegativeTest_FromUpdateIssueAnNotFoundServiceException()
        {
            //given
            var resourceId = 1;
            var resource = new Resource();
            resource = null;
            _resourceRepositoryMock.Setup(m => m.GetResourceById(resourceId)).ReturnsAsync(resource);

            var resourceService = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when

            var resourceNew = new ResourceModel
            {
                Id = 1,
                Name = "ewq",
                Description = "EWQEWQ",
                Price = 3000,
                IsDeleted = false,
            };
            resourceService.UpdateResource(resourceId, resourceNew);

            //then
            Assert.ThrowsAsync<NotFoundServiceException>(async () => await resourceService.UpdateResource(resourceId, resourceNew));
            
        }

        [Test]
        public async Task TestingSoftDeleteService()
        {
            //given
            var resourceId = 1;
            var resource = new Resource
            {
                Id = 1,
                Name = "ewq",
                Description = "EWQEWQ",
                Price = 3000,
                IsDeleted = false,
            };
            _resourceRepositoryMock.Setup(m => m.GetResourceById(resourceId)).ReturnsAsync(resource);
            var resourceService = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            var resourceNew = new ResourceModel
            {
                Id = 1,
                Name = "ewq",
                Description = "EWQEWQ",
                Price = 3000,
                IsDeleted = true,
            };
             await resourceService.SoftDelete(resourceId, resourceNew);

            //then
            _resourceRepositoryMock.Verify(m => m.GetResourceById(resourceId), Times.Once());
            _resourceRepositoryMock.Verify(m => m.SoftDelete(It.IsAny<Resource>()), Times.Once());
            
        }

        [Test]
        public async Task ResourceNegativeTest_FromSoftDeleteIssueAnNotFoundServiceException()
        {
            //given
            var resourceId = 1;
            var resource = new Resource();
            resource = null;
            _resourceRepositoryMock.Setup(m => m.GetResourceById(resourceId)).ReturnsAsync(resource);

            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when

            var resourceNew = new ResourceModel
            {
                Id = 1,
                Name = "ewq",
                Description = "EWQEWQ",
                Price = 3000,
                IsDeleted = true,
            };
            sut.UpdateResource(resourceId, resourceNew);

            //then
            Assert.ThrowsAsync<NotFoundServiceException>(async () => await sut.SoftDelete(resourceId, resourceNew));
            VerifyLoggerHelperService.LoggerVerify(_logger, "Request for soft deletion of resource by id 1", LogLevel.Information);
        }


        [Test]
        public async Task TestingReceivingServiceById()
        {
            //given
            var resourceId = 1;
            var resource = _resourceTestData.AddResourceTest();
            _resourceRepositoryMock.Setup(m => m.GetResourceById(resourceId)).ReturnsAsync(resource);
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            var actual = await sut.GetResourceById(resourceId);

            //then
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Id);
            Assert.IsNotNull(actual.Name);
            Assert.IsNotNull(actual.Price);
            Assert.IsNotNull(actual.IsDeleted);
            Assert.IsNotNull(actual.Description);
        }

        [Test]
        public async Task ResourceNegativeTest_FromReceivingServiceByIdIssueAnNotFoundServiceException()
        {
            //given
            var resourceId = 1;
            var resource = new Resource();
            resource = null;
            _resourceRepositoryMock.Setup(m => m.GetResourceById(resourceId)).ReturnsAsync(resource);
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when

            sut.GetResourceById(resourceId);

            //then
            Assert.ThrowsAsync<NotFoundServiceException>(async () => await sut.GetResourceById(resourceId));
        }

        

        [Test]
        public async Task TestingGettingAllServices()
        {
            //given
            var resource = _resourceTestData.AddAllServiceTest();
            _resourceRepositoryMock.Setup(m => m.GetAllResources()).ReturnsAsync(resource);
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            var actual = await sut.GetAllResources();

            //then
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count > 0);

            for(int i = 0; i< actual.Count;i++)
            {
                Assert.IsNotNull(actual[i].Id);
                Assert.IsNotNull(actual[i].Name);
                Assert.IsNotNull(actual[i].Price);
                Assert.IsNotNull(actual[i].IsDeleted);
                Assert.IsNotNull(actual[i].Description);
                VerifyLoggerHelperService.LoggerVerify(_logger, "Request for getting all resources", LogLevel.Information);
            }
        }
      

        [Test]
        public async Task TestingGettingAllActivelServices()
        {
            //given
            var resource = _resourceTestData.AddAllServiceTest();
            _resourceRepositoryMock.Setup(m => m.GetAllResources()).ReturnsAsync(resource);
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            var actual = await sut.GetActiveResourceService();

            //then
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count > 0);

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.IsNotNull(actual[i].Id);
                Assert.IsNotNull(actual[i].Name);
                Assert.IsNotNull(actual[i].Price);
                Assert.IsNotNull(actual[i].IsDeleted);
                Assert.IsNotNull(actual[i].Description);
            }
        }

        [Test]
        public void ResourceNegativeTest_FromGettingAllActiveServicesIssueAnNotFoundServiceException()
        {
            //given

            List<Resource> resource = new List<Resource>
            {
                new Resource() {Id = 1, Name = "qwe", Description = "QWEQWE", Price = 1500, IsDeleted = true,},
                new Resource() {Id = 2, Name = "ewq", Description = "ewqewq", Price = 2000, IsDeleted = true,},
                new Resource() {Id = 3, Name = "tmp", Description = "tmptmp", Price = 3000, IsDeleted = true,},
                new Resource() {Id = 4, Name = "qwerty", Description = "qwerty", Price = 8000, IsDeleted = true,},
            };

            _resourceRepositoryMock.Setup(m => m.GetAllResources()).ReturnsAsync(resource);
            var resourceService = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            resourceService.GetActiveResourceService();

            //then
            Assert.ThrowsAsync<NotFoundServiceException>(async () => await resourceService.GetActiveResourceService());
        }
    }
}