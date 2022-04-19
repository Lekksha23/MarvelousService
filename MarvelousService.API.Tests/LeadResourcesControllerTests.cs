using AutoMapper;
using MarvelousService.API.Controllers;
using MarvelousService.BusinessLayer.Clients.Interfaces;
using MarvelousService.BusinessLayer.Configurations;
using MarvelousService.BusinessLayer.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarvelousService.API.Tests
{
    public class LeadResourcesControllerTests
    {
        private readonly Mock<ILeadResourceService> _leadResourceServiceMock;
        private readonly Mock<IResourceService> _resourceServiceMock;
        private readonly Mock<ILogger<LeadResourcesController>> _loggerMock;
        private readonly Mock<IRequestHelper> _requestHelperMock;
        private readonly IMapper _autoMapper;

        public LeadResourcesControllerTests()
        {
            _leadResourceServiceMock = new Mock<ILeadResourceService>();
            _resourceServiceMock = new Mock<IResourceService>();
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperToData>()));
            _requestHelperMock = new Mock<IRequestHelper>();
            _loggerMock = new Mock<ILogger<LeadResourcesController>>();
        }

        [Test]
        public async Task InvalidModelTest()
        {
            //// given
            //var model = new AccountInsertRequest { Name = "" }; // Invalid model
            //var controller = new AccountsController(_accountService.Object,
            //    _autoMapper,
            //    _logger.Object,
            //    _crmProducers.Object,
            //    _requestHelper.Object,
            //    _configuration.Object,
            //    _validatorAccountInsertRequest,
            //    _validatorAccountUpdateRequest);

            //// then
            //Assert.ThrowsAsync<ValidationException>(async () => await controller.AddAccount(model));
        }
    }
}
