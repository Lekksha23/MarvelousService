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
using MarvelousService.DataLayer.Enums;
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
            var resourceModel = LeadResourceControllerTestData.GetResourceModelForTests();
            var response = LeadResourceControllerTestData.GetIdentityResponseModel();
            var leadResourceId = 23;
            var token = "IndicativeToken";
            AddContext(token);
            _requestHelperMock.Setup(m => m.SendRequestToValidateToken(token)).ReturnsAsync(response);
            _resourceServiceMock.Setup(m => m.GetResourceById(leadResourceInsertRequest.ResourceId)).ReturnsAsync(resourceModel);
            _leadResourceServiceMock.Setup(m => m.AddLeadResource(It.IsAny<LeadResourceModel>(), Role.Regular, token)).ReturnsAsync(leadResourceId);
            _resourceProducerMock.Setup(m => m.NotifyLeadResourceAdded(leadResourceId));

            // when
            var actual = await _controller.AddLeadResource(leadResourceInsertRequest);

            // then
            Assert.IsInstanceOf<ObjectResult>(actual.Result); 
            _requestHelperMock.Verify(m => m.SendRequestToValidateToken(token), Times.Once);
            _resourceServiceMock.Verify(m => m.GetResourceById(leadResourceInsertRequest.ResourceId), Times.Once);
            _leadResourceServiceMock.Verify(m => m.AddLeadResource(It.IsAny<LeadResourceModel>(), Role.Regular, token), Times.Once);
            _resourceProducerMock.Verify(m => m.NotifyLeadResourceAdded(leadResourceId), Times.Once);
            VerifyLoggerInformation($"Access to the method for lead {response.Id} granted");
            VerifyLoggerInformation($"Request for adding a Resource {leadResourceInsertRequest.ResourceId} to Lead {response.Id}.");
        }

        [Test]
        public async Task AddLeadResource_WeekPeriodWithResourceIdEquals1_ThrowsValidationException()
        {
            // given
            var leadResourceInsertRequest = new LeadResourceInsertRequest { Period = (int)Period.Week, ResourceId = 1 };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.AddLeadResource(leadResourceInsertRequest));
            VerifyLoggerError("Error: You can't order onetime resource as subscription. Choose Period 1 for ResourceId 1 or 2");
        }

        [Test]
        public async Task AddLeadResource_WeekPeriodWithResourceIdEquals2_ThrowsValidationException()
        {
            // given
            var leadResourceInsertRequest = new LeadResourceInsertRequest { Period = (int)Period.Week, ResourceId = 2 };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.AddLeadResource(leadResourceInsertRequest));
            VerifyLoggerError("Error: You can't order onetime resource as subscription. Choose Period 1 for ResourceId 1 or 2");
        }

        [Test]
        public async Task AddLeadResource_MonthPeriodWithResourceIdEquals1_ThrowsValidationException()
        {
            // given
            var leadResourceInsertRequest = new LeadResourceInsertRequest { Period = (int)Period.Month, ResourceId = 1 };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.AddLeadResource(leadResourceInsertRequest));
            VerifyLoggerError("Error: You can't order onetime resource as subscription. Choose Period 1 for ResourceId 1 or 2");
        }

        [Test]
        public async Task AddLeadResource_MonthPeriodWithResourceIdEquals2_ThrowsValidationException()
        {
            // given
            var leadResourceInsertRequest = new LeadResourceInsertRequest { Period = (int)Period.Month, ResourceId = 2 };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.AddLeadResource(leadResourceInsertRequest));
            VerifyLoggerError("Error: You can't order onetime resource as subscription. Choose Period 1 for ResourceId 1 or 2");
        }

        [Test]
        public async Task AddLeadResource_YearPeriodWithResourceIdEquals1_ThrowsValidationException()
        {
            // given
            var leadResourceInsertRequest = new LeadResourceInsertRequest { Period = (int)Period.Year, ResourceId = 1 };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.AddLeadResource(leadResourceInsertRequest));
            VerifyLoggerError("Error: You can't order onetime resource as subscription. Choose Period 1 for ResourceId 1 or 2");
        }

        [Test]
        public async Task AddLeadResource_YearPeriodWithResourceIdEquals2_ThrowsValidationException()
        {
            // given
            var leadResourceInsertRequest = new LeadResourceInsertRequest { Period = (int)Period.Year, ResourceId = 2 };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.AddLeadResource(leadResourceInsertRequest));
            VerifyLoggerError("Error: You can't order onetime resource as subscription. Choose Period 1 for ResourceId 1 or 2");
        }

        [Test]
        public async Task GetLeadResourceById_ReturnsStatus200OK()
        {
            // given
            var leadResourceModel = LeadResourceControllerTestData.GetLeadResourceModelForTests();
            var response = LeadResourceControllerTestData.GetIdentityResponseModel();
            var leadResourceId = 23;
            var token = "IndicativeToken";
            AddContext(token);
            _requestHelperMock.Setup(m => m.SendRequestToValidateToken(token)).ReturnsAsync(response);
            _leadResourceServiceMock.Setup(m => m.GetById(leadResourceId)).ReturnsAsync(leadResourceModel);

            // when
            var actual = await _controller.GetLeadResourceById(leadResourceId);

            // then
            Assert.IsInstanceOf<ObjectResult>(actual.Result);
            _requestHelperMock.Verify(m => m.SendRequestToValidateToken(token), Times.Once);
            _leadResourceServiceMock.Verify(m => m.GetById(leadResourceId), Times.Once);
            VerifyLoggerInformation($"Request for getting lead resource with id {leadResourceId}");
            VerifyLoggerInformation($"Lead resource was received by id {leadResourceId}");
        }

        [Test]
        public async Task GetLeadResourceByLeadId_ReturnsStatus200OK()
        {
            // given
            var leadResourceModelList = LeadResourceControllerTestData.GetLeadResourceModelListForTests();
            var response = LeadResourceControllerTestData.GetIdentityResponseModel();
            var token = "IndicativeToken";
            AddContext(token);
            _requestHelperMock.Setup(m => m.SendRequestToValidateToken(token)).ReturnsAsync(response);
            _leadResourceServiceMock.Setup(m => m.GetByLeadId((int)response.Id)).ReturnsAsync(leadResourceModelList);

            // when
            var actual = await _controller.GetLeadResourcesByLeadId();

            // then
            Assert.IsInstanceOf<ObjectResult>(actual.Result);
            _requestHelperMock.Verify(m => m.SendRequestToValidateToken(token), Times.Once);
            _leadResourceServiceMock.Verify(m => m.GetByLeadId((int)response.Id), Times.Once);
            VerifyLoggerInformation($"Request for getting all lead resources with LeadId {response.Id}");
            VerifyLoggerInformation($"Lead resources were received by LeadId {response.Id}");
        }

        [Test]
        public async Task GetLeadResourceByPayDate_ReturnsStatus200OK()
        {
            // given
            var leadResourceModelList = LeadResourceControllerTestData.GetLeadResourceModelListForTests();
            var response = LeadResourceControllerTestData.GetIdentityResponseModel();
            var token = "IndicativeToken";
            DateTime payDate = default;
            AddContext(token);
            _requestHelperMock.Setup(m => m.SendRequestToValidateToken(token)).ReturnsAsync(response);
            _leadResourceServiceMock.Setup(m => m.GetByPayDate(payDate)).ReturnsAsync(leadResourceModelList);

            // when
            var actual = await _controller.GetLeadResourcesByPayDate(payDate);

            // then
            Assert.IsInstanceOf<ObjectResult>(actual.Result);
            _requestHelperMock.Verify(m => m.SendRequestToValidateToken(token), Times.Once);
            _leadResourceServiceMock.Verify(m => m.GetByPayDate(payDate), Times.Once);
            VerifyLoggerInformation($"Request for getting all lead resources with pay date {payDate}");
            VerifyLoggerInformation($"Lead resources were received by pay date {payDate}");
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

        private void VerifyLoggerError(string message)
        {
            _loggerMock.Verify(x => x.Log(LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals(message, o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }
    }
}
