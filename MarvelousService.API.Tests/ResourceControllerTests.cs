using AutoMapper;
using FluentValidation;
using Marvelous.Contracts.Enums;
using Marvelous.Contracts.ResponseModels;
using MarvelousService.API.Controllers;
using MarvelousService.API.Models;
using MarvelousService.API.Producer.Interface;
using MarvelousService.API.Validators;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Threading.Tasks;

namespace MarvelousService.API.Tests
{
    public class ResourceControllerTests
    {
        private Mock<IResourceService> _resourceService;
        private  Mock<ILogger<ResourcesController>> _logger;
        private  Mock<IConfiguration> _configuration;
        private  IMapper _autoMapper;
        private  Mock<IRequestHelper> _requestHelper;
        private  IValidator<ResourceInsertRequest> _validatorResourceInsertRequest;
        private  IValidator<ResourceSoftDeleteRequest> _validatorResourceSoftDeletetRequest;
        private  IValidator<ResourceUpdateRequest> _validatorResourceUpdatetRequest;
        private  Mock<IResourceProducer> _resourceProducer;
        private  ResourcesController _resourceController;



        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<ResourcesController>>();
            _resourceService = new Mock<IResourceService>();                     
            _configuration = new Mock<IConfiguration>();
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
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
                _validatorResourceInsertRequest);
        }

        [Test]
        public async Task ResourceTestIssueAnDuplicationException()
        {
            //// Arrange
            //var resourceId = 1;
            //var resourceNew = new ResourceModel
            //{
            //    Id = 1,
            //    Name = "ewq",
            //    Description = "EWQEWQ",
            //    Price = 3000,
            //    Type = DataLayer.Enums.ServiceType.OneTime,
            //    IsDeleted = false,
            //};
            //var resoerceRequestModel = new ResourceInsertRequest
            //{
                
            //    Name = "qwe",
            //    Description = "EWQEWQ",
            //    Price = 1500,
            //    Type = 1
            //};

            //var identityResponseModel = new IdentityResponseModel()
            //{
            //    Id = 1,
            //    Role = "Admin",
            //    IssuerMicroservice = Microservice.MarvelousCrm.ToString()
            //};

            //await _requestHelper.Setup(x => x.SendRequestToValidateToken(It.IsAny<string>())).ReturnsAsync(identityResponseModel);
            //_resourceService.Setup(t => t.AddResource(resourceNew)).ReturnsAsync(resourceId);
            //var resourseModel = _autoMapper.Map<ResourceModel>(resoerceRequestModel);

            ////when
            //var actual = await _resourceController.AddResource(resoerceRequestModel);

            ////then
            //Assert.IsInstanceOf<CreatedResult> (actual.Result);
            //_resourceService.Verify(m => m.AddResource(It.IsAny<ResourceModel>()), Times.Once());
            //_resourceProducer.Verify(t => t.NotifyResourceAdded(It.IsAny<int>()), Times.Once);



           
        }


     



    }

}