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
        public async Task AddResource()
        {
            //given
            var resource = new Resource
            {
                Id = 3,
                Name = "qwe",
                Description = "QWEQWE",
                Price = 1500,
                IsDeleted = false,
            }; 
            _resourceRepositoryMock.Setup(m => m.GetResourceById(It.IsAny<int>())).ReturnsAsync(resource);
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            await sut.AddResource( new ResourceModel
            {
                Id = 3,
                Name = "qwe",
                Description = "QWEQWE",
                Price = 1500,
                IsDeleted = false,
            });

            var actual = await sut.GetResourceById(3);

            //then
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Id);
            Assert.IsNotNull(actual.Name);
            Assert.IsNotNull(actual.Price);
            Assert.IsNotNull(actual.IsDeleted);
            Assert.IsNotNull(actual.Description);
        }

        [Test]
        public void AddResourceNegativeTest()
        {
            //given
            var resourceId = 1;
            var resourceModel = _resourceTestData.AddResourceModelTest();
            var resource = new Resource();
            _resourceRepositoryMock.Setup(m => m.GetResourceById(resourceId)).ReturnsAsync(resource); 

            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //then
            Assert.ThrowsAsync<DuplicationException>(async () => await sut.AddResource(resourceModel));
        }

        [Test]
        public async Task UpdateResource()
        {
            //given
            var resourceId = 1;
            var resourceModel = _resourceTestData.AddResourceTest();
            _resourceRepositoryMock.Setup(m => m.GetResourceById(It.IsAny<int>())).ReturnsAsync(resourceModel);
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            await sut.UpdateResource(1, new ResourceModel());

            //then
            _resourceRepositoryMock.Verify(m => m.GetResourceById(It.IsAny<int>()), Times.Once());
            _resourceRepositoryMock.Verify(m => m.UpdateResource(It.IsAny<Resource>()), Times.Once());
        }

        [Test]
        public async Task SoftDelete()
        {
            //given
            var resource = new Resource();
            _resourceRepositoryMock.Setup(m => m.GetResourceById(It.IsAny<int>())).ReturnsAsync(resource);
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            await sut.SoftDelete(1, new ResourceModel());

            //then
            _resourceRepositoryMock.Verify(m => m.GetResourceById(It.IsAny<int>()), Times.Once());
            _resourceRepositoryMock.Verify(m => m.SoftDelete(It.IsAny<Resource>()), Times.Once()); ;
        }


        [Test]
        public async Task GetById()
        {
            //given
            var resourceId = 1;
            var resource = _resourceTestData.AddResourceTest();
            _resourceRepositoryMock.Setup(m => m.GetResourceById(It.IsAny<int>())).ReturnsAsync(resource);
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object);

            //when
            var actual = await sut.GetResourceById(It.IsAny<int>());

            //then
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Id);
            Assert.IsNotNull(actual.Name);
            Assert.IsNotNull(actual.Price);
            Assert.IsNotNull(actual.IsDeleted);
            Assert.IsNotNull(actual.Description);
        }

        [Test]
        public async Task GetAllResources()
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
            }
        }

        [Test]
        public async Task GetAllActiveResources()
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
    }
}