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
        private readonly ResourceServiceTestCaseSource _resourceTest;
        private readonly IMapper _autoMapper;
        private readonly Mock<ILogger<ResourceService>> _logger;
        private readonly Mock<ICheckErrorHelper> _helper;

        public ResourceServiceTests()
        {
            _resourceRepositoryMock = new Mock<IResourceRepository>();
            _resourceTest = new ResourceServiceTestCaseSource();
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _logger = new Mock<ILogger<ResourceService>>();
            _helper = new Mock<ICheckErrorHelper>();
        }

        [SetUp]
        public async Task Setup()
        {
            _resourceRepositoryMock = new Mock<IResourceRepository>();
        }

        [Test]
        public async Task AddServiceTest()
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
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object, _helper.Object);

            //when
            sut.AddResource( new ResourceModel
            {
                Id = 3,
                Name = "qwe",
                Description = "QWEQWE",
                Price = 1500,
                IsDeleted = false,
            });

            var actual = sut.GetResourceById(3);

            //then
            //Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Result.Id);
            Assert.IsNotNull(actual.Result.Name);
            Assert.IsNotNull(actual.Result.Price);
            Assert.IsNotNull(actual.Result.IsDeleted);
            Assert.IsNotNull(actual.Result.Description);
        }

        [Test]
        public async Task AddServiceNegativeTest()
        {

            //given
            var resourceId = 1;
            var resourceModel = _resourceTest.AddServiceModelTest();
            var resource = new Resource();
            _resourceRepositoryMock.Setup(m => m.GetResourceById(resourceId));
            
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object, _helper.Object);

            //then
            Assert.ThrowsAsync<DuplicationException>(async () => await sut.AddResource(resourceModel));
        }

        [Test]
        public async Task UpdateServiceTest()
        {
            //given
            var resourceId = 1;
            var resourceModel = _resourceTest.AddServiceTest();
            _resourceRepositoryMock.Setup(m => m.GetResourceById(resourceId));
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object, _helper.Object);

            //when
            await sut.UpdateResource(1, new ResourceModel());

            //then
            _resourceRepositoryMock.Verify(m => m.GetResourceById(It.IsAny<int>()), Times.Once());
            _resourceRepositoryMock.Verify(m => m.UpdateResource(It.IsAny<Resource>()), Times.Once());
        }

        [Test]
        public async Task DeletedServiceTest()
        {
            //given
            var resource = new Resource();
            _resourceRepositoryMock.Setup(m => m.GetResourceById(It.IsAny<int>())).ReturnsAsync(resource);
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object, _helper.Object);

            //when
            await sut.SoftDelete(1, new ResourceModel());

            //then
            _resourceRepositoryMock.Verify(m => m.GetResourceById(It.IsAny<int>()), Times.Once());
            _resourceRepositoryMock.Verify(m => m.SoftDelete(It.IsAny<Resource>()), Times.Once()); ;
        }


        [Test]
        public async Task GetByIdTest()
        {
            //given
            var resourceId = 1;
            var resource = _resourceTest.AddServiceTest();
            _resourceRepositoryMock.Setup(m => m.GetResourceById(It.IsAny<int>())).ReturnsAsync(resource);
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object, _helper.Object);

            //when
            var actual = sut.GetResourceById(It.IsAny<int>()).Result;

            //then
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Id);
            Assert.IsNotNull(actual.Name);
            Assert.IsNotNull(actual.Price);
            Assert.IsNotNull(actual.IsDeleted);
            Assert.IsNotNull(actual.Description);

        }

        [Test]
        public async Task GetAllResourseTest()
        {
            //given
            var resource = _resourceTest.AddAllServiceTest();
            _resourceRepositoryMock.Setup(m => m.GetAllResources()).ReturnsAsync(resource);
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object, _helper.Object);

            //when
            var actual =  sut.GetAllResources().Result;

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
        public async Task GetAllActiveResourseTest()
        {
            //given
            var resource = _resourceTest.AddAllServiceTest();
            _resourceRepositoryMock.Setup(m => m.GetAllResources()).ReturnsAsync(resource);
            var sut = new ResourceService(_resourceRepositoryMock.Object, _autoMapper, _logger.Object, _helper.Object);

            //when
            var actual = sut.GetActiveResourceService().Result;

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