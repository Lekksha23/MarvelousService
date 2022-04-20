using AutoMapper;
using FluentValidation;
using MarvelousService.API.Controllers;
using MarvelousService.API.Models;
using MarvelousService.API.Validators;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarvelousService.API.Tests
{
    public class LeadResourcesControllerTests
    {
        private Mock<ILeadResourceService> _leadResourceServiceMock;
        private Mock<IResourceService> _resourceServiceMock;
        private LeadResourcesController _controller;
        private Mock<ILogger<LeadResourcesController>> _loggerMock;
        private Mock<IRequestHelper> _requestHelperMock;
        private readonly IValidator<LeadResourceInsertRequest> _leadResourceInsertRequestValidator;
        private readonly IMapper _autoMapper;

        public LeadResourcesControllerTests()
        {
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _leadResourceInsertRequestValidator = new LeadResourceInsertRequestValidator();
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
                _leadResourceInsertRequestValidator);
        }

        [Test]
        public async Task AddLeadResource_()
        {
            //given
            var token = "IndicativeToken";
            var leadResourceInsertRequest = new LeadResourceInsertRequest { Period = 1, ResourceId = 1 };
            var leadResourceModel = _autoMapper.Map<LeadResourceModel>(leadResourceInsertRequest);
            AddContext(token);

            //when
            var actualResult = await _controller.AddLeadResource(leadResourceInsertRequest);

            //then

        }

        private void AddContext(string token)
        {
            var context = new DefaultHttpContext();
            context.Request.Headers.Authorization = token;
            _controller.ControllerContext.HttpContext = context;
        }
    }
}
