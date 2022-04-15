using AutoMapper;
using FluentValidation;
using MarvelousService.API.Controllers;
using MarvelousService.API.Models;
using MarvelousService.API.Producer.Interface;
using MarvelousService.API.Validators;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Helpers;
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
        private readonly Mock<IResourceProducer> _resourceProducer;


        public Tests()
        {
            _resourceService = new Mock<IResourceService>();
            _logger = new Mock<ILogger<ResourcesController>>();
            _configuration = new Mock<IConfiguration>();
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _validatorResourceInsertRequest = new ResourceInsertRequestValidator();
            _resourceProducer = new Mock<IResourceProducer>();
            _requestHelper = new Mock<IRequestHelper>();
        }

        [Test]
        public async Task ResourceTestIssueAnDuplicationException()
        {

           


        }






    }

}