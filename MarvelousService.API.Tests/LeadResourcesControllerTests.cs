using AutoMapper;
using FluentValidation;
using MarvelousService.API.Configuration;
using MarvelousService.API.Controllers;
using MarvelousService.API.Models;
using MarvelousService.API.Producer.Interface;
using MarvelousService.API.Validators;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using MarvelousService.BusinessLayer.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

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

        //[Test]
        //public async Task AddLeadResource_()
        //{
        //    // given
        //    var token = "IndicativeToken";
        //    var leadResourceInsertRequest = new LeadResourceInsertRequest { Period = 1, ResourceId = 1 };
        //    var leadResourceModel = _autoMapper.Map<LeadResourceModel>(leadResourceInsertRequest);
        //    var context = new DefaultHttpContext();
        //    context.Request.Headers.Authorization = token;
        //    _controller.ControllerContext.HttpContext = context;

        //    // when
        //    //var actualResult = await _controller.AddLeadResource(leadResourceInsertRequest);

        //    // then

        //}

    }
}
