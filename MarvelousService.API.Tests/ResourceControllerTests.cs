using AutoMapper;
using FluentValidation;
using MarvelousService.API.Controllers;
using MarvelousService.API.Models;
using MarvelousService.API.Producer.Interface;
using MarvelousService.API.Validators;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarvelousService.API.Tests
{
    public class Tests
    {
        private Mock<IResourceService> _resourceService;
        private readonly Mock<ILogger<ResourcesController>> _logger;
        private readonly Mock<IConfiguration> _configuration;
        private readonly IMapper _autoMapper;
        private readonly Mock<IRequestHelper> _requestHelper;
        private readonly IValidator<ResourceInsertRequest> _validatorResourceInsertRequest;
        private readonly IValidator<ResourceSoftDeleteRequest> _validatorResourceSoftDeletetRequest;
        private readonly IValidator<ResourceUpdateRequest> _validatorResourceUpdatetRequest;
        private readonly Mock<IResourceProducer> _resourceProducer;
        private readonly ResourcesController _controller;

        public Tests()
        {
            _resourceService = new Mock<IResourceService>();
            _controller = new ResourcesController(
                _resourceService.Object,
                _autoMapper,_logger.Object,
                _resourceProducer.Object,
                _requestHelper.Object,
                _validatorResourceInsertRequest);
            _logger = new Mock<ILogger<ResourcesController>>();
            _configuration = new Mock<IConfiguration>();
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _validatorResourceInsertRequest = new ResourceInsertRequestValidator();
            _validatorResourceSoftDeletetRequest = new ResourceSoftDeleteRequestValidator();
            _validatorResourceUpdatetRequest = new ResourceUpdateRequestValidator();
            _resourceProducer = new Mock<IResourceProducer>();
            _requestHelper = new Mock<IRequestHelper>();
        }

        [Test]
        public async Task ResourceTestIssueAnDuplicationException()
        {
            // Arrange
            var resourceId = 1;
            var resourceNew = new ResourceModel
            {
                Id = 1,
                Name = "ewq",
                Description = "EWQEWQ",
                Price = 3000,
                IsDeleted = false,
            };
            var resoerceRequestModel = new ResourceInsertRequest
            {
                
                Name = "qwe",
                Description = "EWQEWQ",
                Price = 1500,
                Type = 0
            };
            
            
            _resourceService.Setup(t => t.GetResourceById(resourceId)).ReturnsAsync(resourceNew);
            var resourseModel = _autoMapper.Map<ResourceModel>(resoerceRequestModel);

            //when
            var actual =  _controller.AddResource(resoerceRequestModel);

            //then
            _resourceService.Verify(m => m.AddResource(It.IsAny<ResourceModel>()), Times.Once());

        }


     



    }

}