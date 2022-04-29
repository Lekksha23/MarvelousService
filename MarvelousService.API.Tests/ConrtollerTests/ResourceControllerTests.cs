using AutoMapper;
using FluentValidation;
using Marvelous.Contracts.Enums;
using Marvelous.Contracts.ResponseModels;
using MarvelousService.API.Configuration;
using MarvelousService.API.Controllers;
using MarvelousService.API.Extensions;
using MarvelousService.API.Models;
using MarvelousService.API.Producer.Interface;
using MarvelousService.API.Validators;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MarvelousService.API.Tests
{
    public class ResourceControllerTests
    {
        private Mock<IResourceService> _resourceService;
        private Mock<ILogger<ResourcesController>> _logger;
        private IMapper _autoMapper;
        private Mock<IRequestHelper> _requestHelper;
        private IValidator<ResourceInsertRequest> _validatorResourceInsertRequest;
        private IValidator<ResourceSoftDeleteRequest> _validatorResourceSoftDeletetRequest;
        private IValidator<ResourceUpdateRequest> _validatorResourceUpdatetRequest;
        private Mock<IResourceProducer> _resourceProducer;
        private ResourcesController _resourceController;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<ResourcesController>>();
            _resourceService = new Mock<IResourceService>();
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperFromApi>()));
            _validatorResourceInsertRequest = new ResourceInsertRequestValidator();
            _validatorResourceSoftDeletetRequest = new ResourceSoftDeleteRequestValidator();
            _validatorResourceUpdatetRequest = new ResourceUpdateRequestValidator();
            _resourceProducer = new Mock<IResourceProducer>();
            _requestHelper = new Mock<IRequestHelper>();
            _resourceController = new ResourcesController(
                _resourceService.Object,
                _autoMapper,
                _logger.Object,
                _resourceProducer.Object,
                _requestHelper.Object,
                _validatorResourceSoftDeletetRequest,
                _validatorResourceInsertRequest);
        }

        private void AddContext(string token)
        {
            var context = new DefaultHttpContext();
            context.Request.Headers.Authorization = token;
            _resourceController.ControllerContext.HttpContext = context;
        }


        [Test]
        public async Task AddResourseTest_ShouldReturnStatusCode200()
        {
            // Arrange
            var resourceNew = new ResourceModel
            {
                Id = 1,
                Name = "ewq",
                Description = "EWQEWQ",
                Price = 3000,
                Type = DataLayer.Enums.ServiceType.OneTime,
                IsDeleted = false,
            };
            var resoerceRequestModel = new ResourceInsertRequest
            {

                Name = "qwe",
                Description = "EWQEWQ",
                Price = 1500,
                Type = 1,
            };

            var token = "token";
            AddContext(token);
            _requestHelper
                .Setup(m => m.SendRequestToValidateToken(token))
                .ReturnsAsync(new IdentityResponseModel { Id = 1, IssuerMicroservice = Microservice.MarvelousCrm.ToString(), Role = "Admin" });

            //when
            var actual = await _resourceController.AddResource(resoerceRequestModel);


            //then
            Assert.IsInstanceOf<ObjectResult>(actual.Result);
            _resourceService.Verify(r => r.AddResource(It.IsAny<ResourceModel>()), Times.Once);
            _requestHelper.Verify(r => r.SendRequestToValidateToken(token), Times.Once());
            _resourceProducer.Verify(r => r.NotifyResourceAdded(It.IsAny<int>()));
            VerifyLoggerHelper.LoggerVerify(_logger, "Received a request to add a new resource.", LogLevel.Information);
            

        }

        [Test]
        public void AddResourseTest_Forbidden_ShouldThrowForbiddenException()
        {
            //given
            var resourceNew = new ResourceModel
            {
                Id = 1,
                Name = "ewq",
                Description = "EWQEWQ",
                Price = 3000,
                Type = DataLayer.Enums.ServiceType.OneTime,
                IsDeleted = false,
            };
            var resoerceRequestModel = new ResourceInsertRequest();
            resoerceRequestModel = null;

            var token = "token";
            AddContext(token);
            _requestHelper
                .Setup(r => r.SendRequestToValidateToken(token))
                .ReturnsAsync(new IdentityResponseModel { Id = 1, IssuerMicroservice = Microservice.MarvelousCrm.ToString(), Role = "Admin" });
            var expectedMessage = "Cannot pass null model to Validate. (Parameter 'instanceToValidate')";

            //when
            ArgumentNullException? exception = Assert.ThrowsAsync<ArgumentNullException>(() =>
            _resourceController.AddResource(resoerceRequestModel));

            //then
            Assert.That(exception?.Message, Is.EqualTo(expectedMessage));
            VerifyLoggerHelper.LoggerVerify(_logger, "Received a request to add a new resource.", LogLevel.Information);
        }

        [Test]
        public void AddResourseTest_NotValidModelReceived_ShouldThrowValidationException()
        {
            //given
            var resourceId = 1;
            var resourceNew = new ResourceModel
            {
                Id = 1,
                Name = "ewq",
                Description = "EWQEWQ",
                Price = 3000,
                Type = DataLayer.Enums.ServiceType.OneTime,
                IsDeleted = false,
            };
            var resoerceRequestModel = new ResourceInsertRequest
            {

                Name = "qwe",
                Description = null,
                Price = 1500,
                Type = 1,
            };
            var token = "token";
            AddContext(token);
            _requestHelper
                .Setup(m => m.SendRequestToValidateToken(token))
                .ReturnsAsync(new IdentityResponseModel { Id = 1, IssuerMicroservice = Microservice.MarvelousCrm.ToString(), Role = "Admin" });
            _resourceService.Setup(r => r.AddResource(It.IsAny<ResourceModel>())).ReturnsAsync(resourceId);
            var expectedMessage = "ResourceInsertRequest isn't valid";

            //when
            ValidationException? exception = Assert.ThrowsAsync<ValidationException>(() =>
           _resourceController.AddResource(resoerceRequestModel));

            //then
            Assert.That(exception?.Message, Is.EqualTo(expectedMessage));
            VerifyLoggerHelper.LoggerVerify(_logger, "Received a request to add a new resource.", LogLevel.Information);
        }


        [Test]

        public async Task UpdateResourseTest_ShouldReturnStatusCode200()
        {
            // Arrange

            var resourceId = 1;

            var resourceNew = new ResourceModel
            {
                Id = 1,
                Name = "ewq",
                Description = "EWQEWQ",
                Price = 3000,
                Type = DataLayer.Enums.ServiceType.OneTime,
                IsDeleted = false,
            };
            var resoerceRequestModel = new ResourceUpdateRequest
            {

                Name = "qwe",
                Description = "EWQEWQ",
                Price = 1500,
                Type = 1,
            };

            var token = "token";
            AddContext(token);
            _requestHelper
                .Setup(m => m.SendRequestToValidateToken(token))
                .ReturnsAsync(new IdentityResponseModel { Id = 1, IssuerMicroservice = Microservice.MarvelousCrm.ToString(), Role = "Admin" });

            //when
            var actual = await _resourceController.UpdateResource(resourceId, resoerceRequestModel);


            //then
            Assert.IsInstanceOf<ObjectResult>(actual.Result);
            _resourceService.Verify(r => r.UpdateResource(resourceId, It.IsAny<ResourceModel>()), Times.Once);
            _requestHelper.Verify(r => r.SendRequestToValidateToken(token), Times.Once());
            _resourceProducer.Verify(r => r.NotifyResourceAdded(It.IsAny<int>()));
            VerifyLoggerHelper.LoggerVerify(_logger, "Request for updating a resource with id 1.", LogLevel.Information);

        }

        [Test]
        public void UpdateResourseTest_NotValidModelReceived_ShouldThrowValidationException()
        {
            //given
            var resourceId = 1;
            var resourceNew = new ResourceModel
            {
                Id = 1,
                Name = "ewq",
                Description = null,
                Price = 3000,
                Type = DataLayer.Enums.ServiceType.OneTime,
                IsDeleted = false,
            };
            var resoerceRequestModel = new ResourceUpdateRequest
            {

                Name = "qwe",
                Description = null,
                Price = 1500,
                Type = 1,
            };
            var token = "token";
            AddContext(token);
            _requestHelper
                .Setup(m => m.SendRequestToValidateToken(token))
                .ReturnsAsync(new IdentityResponseModel { Id = 1, IssuerMicroservice = Microservice.MarvelousCrm.ToString(), Role = "Admin" });
            _resourceService.Setup(r => r.UpdateResource(resourceId,It.IsAny<ResourceModel>()));
            var expectedMessage = "ResourceUpdatetRequest isn't valid";

            //when
            ValidationException? exception = Assert.ThrowsAsync<ValidationException>(() =>
           _resourceController.UpdateResource(resourceId,resoerceRequestModel));

            //then
            Assert.That(exception?.Message, Is.EqualTo(expectedMessage));
            VerifyLoggerHelper.LoggerVerify(_logger, "Request for updating a resource with id 1.", LogLevel.Information);
        }

        [Test]
        public async Task SoftDeleteResourseTest_ShouldReturnStatusCode200()
        {
            // Arrange
            var resourceId = 1;

            var resourceNew = new ResourceModel
            {
                Id = 1,
                Name = "ewq",
                Description = "EWQEWQ",
                Price = 3000,
                Type = DataLayer.Enums.ServiceType.OneTime,
                IsDeleted = false,
            };
            var resoerceRequestModel = new ResourceSoftDeleteRequest
            {
                IsDeleted = true,
            };

            var token = "token";
            AddContext(token);
            _requestHelper
                .Setup(m => m.SendRequestToValidateToken(token))
                .ReturnsAsync(new IdentityResponseModel { Id = 1, IssuerMicroservice = Microservice.MarvelousCrm.ToString(), Role = "Admin" });

            //when
            var actual = await _resourceController.SoftDelete(resourceId, resoerceRequestModel);


            //then
            Assert.IsInstanceOf<ObjectResult>(actual.Result);
            _resourceService.Verify(r => r.SoftDelete(resourceId, It.IsAny<ResourceModel>()), Times.Once);
            _requestHelper.Verify(r => r.SendRequestToValidateToken(token), Times.Once());
            _resourceProducer.Verify(r => r.NotifyResourceAdded(It.IsAny<int>()));
            VerifyLoggerHelper.LoggerVerify(_logger, "Request for deletion a resource with id 1.", LogLevel.Information);

        }

        [Test]
        public void SoftDeleteResourseTest_NotValidModelReceived_ShouldThrowValidationException()
        {
            //given
            var resourceId = 1;
            var resourceNew = new ResourceModel
            {
                Id = 1,
                Name = "ewq",
                Description = null,
                Price = 3000,
                Type = DataLayer.Enums.ServiceType.OneTime,
                IsDeleted = false,
            };
            var resoerceRequestModel = new ResourceSoftDeleteRequest
            {
                IsDeleted = false,
            };
            var token = "token";
            AddContext(token);
            _requestHelper
                .Setup(m => m.SendRequestToValidateToken(token))
                .ReturnsAsync(new IdentityResponseModel { Id = 1, IssuerMicroservice = Microservice.MarvelousCrm.ToString(), Role = "Admin" });
            _resourceService.Setup(r => r.SoftDelete(resourceId, It.IsAny<ResourceModel>()));
            var expectedMessage = "ResourceSoftDeleteRequest isn't valid";

            //when
            ValidationException? exception = Assert.ThrowsAsync<ValidationException>(() =>
           _resourceController.SoftDelete(resourceId, resoerceRequestModel));

            //then
            Assert.That(exception?.Message, Is.EqualTo(expectedMessage));
            VerifyLoggerHelper.LoggerVerify(_logger, "Request for deletion a resource with id 1.", LogLevel.Information);
        }



        [Test]
        public async Task GetResourceById_ShouldReturnStatusCode200()
        {
            // Arrange
            var resourceId = 1;

            var token = "token";
            AddContext(token);
            _requestHelper
                .Setup(m => m.SendRequestToValidateToken(token))
                .ReturnsAsync(new IdentityResponseModel { Id = 1, IssuerMicroservice = Microservice.MarvelousCrm.ToString(), Role = "Admin" });
            
            //when
            var actual = await _resourceController.GetResourceById(resourceId);


            //then
            Assert.IsInstanceOf<ObjectResult>(actual.Result);
            _resourceService.Verify(r => r.GetResourceById(It.IsAny<int>()), Times.Once);
            _requestHelper.Verify(r => r.SendRequestToValidateToken(token), Times.Once());
            _resourceProducer.Verify(r => r.NotifyResourceAdded(It.IsAny<int>()));
            VerifyLoggerHelper.LoggerVerify(_logger, "Request for getting a resource by id 1", LogLevel.Information);
        }


        [Test]
        public async Task GetActiveResource_ShouldReturnStatusCode200()
        {
            // Arrange
            var token = "token";
            AddContext(token);
            _requestHelper
                .Setup(m => m.SendRequestToValidateToken(token))
                .ReturnsAsync(new IdentityResponseModel { Id = 1, IssuerMicroservice = Microservice.MarvelousCrm.ToString(), Role = "Admin" });

            //when
            var actual = await _resourceController.GetActiveResource();


            //then
            Assert.IsInstanceOf<ObjectResult>(actual.Result);
            _resourceService.Verify(r => r.GetActiveResourceService(), Times.Once);
            _requestHelper.Verify(r => r.SendRequestToValidateToken(token), Times.Once());
            VerifyLoggerHelper.LoggerVerify(_logger, "Request for receiving all resources", LogLevel.Information);

        }

        [Test]
        public async Task GetAllResource_ShouldReturnStatusCode200()
        {
            // Arrange
            var token = "token";
            AddContext(token);
            _requestHelper
                .Setup(m => m.SendRequestToValidateToken(token))
                .ReturnsAsync(new IdentityResponseModel { Id = 1, IssuerMicroservice = Microservice.MarvelousCrm.ToString(), Role = "Admin" });

            //when
            var actual = await _resourceController.GetAllResources();

            //then
            Assert.IsInstanceOf<ObjectResult>(actual.Result);
            _resourceService.Verify(r => r.GetAllResources(), Times.Once);
            _requestHelper.Verify(r => r.SendRequestToValidateToken(token), Times.Once());
            VerifyLoggerHelper.LoggerVerify(_logger, "Request for receiving all resources", LogLevel.Information);
        }


    }

}
