using AutoMapper;
using FluentValidation;
using Marvelous.Contracts.Enums;
using MarvelousService.API.Configuration;
using MarvelousService.API.Controllers;
using MarvelousService.API.Models;
using MarvelousService.API.Producer.Interface;
using MarvelousService.API.Tests.TestData;
using MarvelousService.API.Validators;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MarvelousService.API.Tests
{
    public class LeadResourcesControllerTests
    {
        private readonly Mock<IResourceProducer> _resourceProducerMock;
        private Mock<ILeadResourceService> _leadResourceServiceMock;
        private Mock<IResourceService> _resourceServiceMock;
        private LeadResourcesController _controller;
        private Mock<ILogger<LeadResourcesController>> _loggerMock;
        private Mock<IRequestHelper> _requestHelperMock;
        private readonly IValidator<LeadResourceInsertRequest> _leadResourceInsertRequestValidator;
        private readonly IMapper _autoMapper;

        public LeadResourcesControllerTests()
        {
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperFromApi>()));
            _leadResourceInsertRequestValidator = new LeadResourceInsertRequestValidator();
            _resourceProducerMock = new Mock<IResourceProducer>();
        }

        [SetUp]
        public void Setup()
        {
            _leadResourceServiceMock = new Mock<ILeadResourceService>();
            _resourceServiceMock = new Mock<IResourceService>();
            _requestHelperMock = new Mock<IRequestHelper>();
            _loggerMock = new Mock<ILogger<LeadResourcesController>>();
            _controller = new LeadResourcesController(
                _autoMapper,
                _leadResourceServiceMock.Object,
                _resourceServiceMock.Object,
                _requestHelperMock.Object,
                _loggerMock.Object,
                _resourceProducerMock.Object,
                _leadResourceInsertRequestValidator);
        }

        [Test]
        public async Task AddLeadResource_OnetimePeriodWithResourceIdEquals1_ShouldReturnStatusCode201AndLeadResourceId()
        {
            // given
            var leadResourceInsertRequest = new LeadResourceInsertRequest { Period = 1, ResourceId = 1 };
            var resourceModel = LeadResourceControllerTestData.GetResourceForTests();
            var response = LeadResourceControllerTestData.GetIdentityResponseModel();
            var leadResourceId = 23;
            var token = "IndicativeToken";
            AddContext(token);
            _requestHelperMock.Setup(m => m.SendRequestToValidateToken(token)).ReturnsAsync(response);
            _resourceServiceMock.Setup(m => m.GetResourceById(leadResourceInsertRequest.ResourceId)).ReturnsAsync(resourceModel);
            _leadResourceServiceMock.Setup(m => m.AddLeadResource(It.IsAny<LeadResourceModel>(), Role.Regular, token)).ReturnsAsync(leadResourceId);
            _resourceProducerMock.Setup(m => m.NotifyLeadResourceAdded(leadResourceId));

            // when
            var result = await _controller.AddLeadResource(leadResourceInsertRequest);
            var actualResult = result.Result as OkObjectResult;

            // then
            Assert.AreEqual(leadResourceId, actualResult!.Value);
            Assert.IsInstanceOf<OkObjectResult>(actualResult);
            Assert.AreEqual(StatusCodes.Status201Created, actualResult.StatusCode);
            _requestHelperMock.Verify(m => m.SendRequestToValidateToken(token), Times.Once);
            _resourceServiceMock.Verify(m => m.GetResourceById(leadResourceInsertRequest.ResourceId), Times.Once);
            _leadResourceServiceMock.Verify(m => m.AddLeadResource(It.IsAny<LeadResourceModel>(), Role.Regular, token), Times.Once);
            _resourceProducerMock.Verify(m => m.NotifyLeadResourceAdded(leadResourceId), Times.Once);
            VerifyLoggerInformation($"Access to the method for lead {response.Id} granted");
            VerifyLoggerInformation($"Request for adding a Resource {leadResourceInsertRequest.ResourceId} to Lead {response.Id}.");
        }

        private void AddContext(string token)
        {
            var context = new DefaultHttpContext();
            context.Request.Headers.Authorization = token;
            _controller.ControllerContext.HttpContext = context;
        }

        private void VerifyLoggerInformation(string message)
        {
            _loggerMock.Verify(x => x.Log(LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals(message, o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }
    }
}
