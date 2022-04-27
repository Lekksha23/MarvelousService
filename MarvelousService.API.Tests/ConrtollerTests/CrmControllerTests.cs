using AutoMapper;
using CRM.APILayer.Validation;
using FluentValidation;
using MarvelousService.API.Configuration;
using MarvelousService.API.Controllers;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Clients;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MarvelousService.API.Tests
{
    public class CrmControllerTests
    {
        private readonly CrmController _controller;
        private readonly Mock<ICrmClient> _crmClientMock;
        private readonly IMapper _autoMapper;
        private readonly IRequestHelper _requestHelper;
        private readonly Mock<ILogger<CrmController>> _loggerMock;
        private readonly IValidator<LeadInsertRequest> _leadInsertRequestValidator;

        public CrmControllerTests()
        {
            _crmClientMock = new Mock<ICrmClient>();
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperFromApi>()));
            _loggerMock = new Mock<ILogger<CrmController>>();
            _leadInsertRequestValidator = new LeadInsertRequestValidator();
            _controller = new CrmController(
                _crmClientMock.Object,
                _autoMapper, 
                _loggerMock.Object,
                _requestHelper, 
                _leadInsertRequestValidator);
        }

        [Test]
        public async Task RegistrateLead_ShouldAddNewLead()
        {
            // given
            var leadInsertRequest = new LeadInsertRequest 
            { 
                Name = "Borya",
                LastName = "Sokolov",
                Email = "reg@example.com", 
                BirthDate = DateTime.Now, 
                Password = "dslkjfjgh123",
                City = "Кракожия",
                Phone = "89823487623" 
            };
            var leadId = 23;
            _crmClientMock.Setup(m => m.AddLead(It.IsAny<LeadModel>())).ReturnsAsync(leadId);

            // when
            var actual = _controller.RegistrateLead(leadInsertRequest);

            // then
            _crmClientMock.Verify(m => m.AddLead(It.IsAny<LeadModel>()), Times.Once);
            VerifyLoggerInformation($"Query for registration new lead with name: {leadInsertRequest.Name} and email: {leadInsertRequest.Email}");
            VerifyLoggerInformation($"A new lead with email: {leadInsertRequest.Email} was successfully added.");
        }

        [Test]
        public async Task RegistrateLead_NameIsNull_ThrowsValidationException()
        {
            // given
            var leadInsertRequest = new LeadInsertRequest
            {
                Name = null,
                LastName = "Sokolov",
                Email = "reg@example.com",
                BirthDate = DateTime.Now,
                Password = "dslkjfjgh123",
                City = "Кракожия",
                Phone = "89823487623"
            };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.RegistrateLead(leadInsertRequest));
            VerifyLoggerError("Error: You must specify name");
        }

        [Test]
        public async Task RegistrateLead_LastNameIsNull_ThrowsValidationException()
        {
            // given
            var leadInsertRequest = new LeadInsertRequest
            {
                Name = "Mu",
                LastName = null,
                Email = "reg@example.com",
                BirthDate = DateTime.Now,
                Password = "dslkjfjgh123",
                City = "Кракожия",
                Phone = "89823487623"
            };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.RegistrateLead(leadInsertRequest));
            VerifyLoggerError("Error: You must specify last name");
        }

        [Test]
        public async Task RegistrateLead_EmailIsNull_ThrowsValidationException()
        {
            // given
            var leadInsertRequest = new LeadInsertRequest
            {
                Name = "Mu",
                LastName = "Nikalaiv",
                Email = null,
                BirthDate = DateTime.Now,
                Password = "dslkjfjgh123",
                City = "Кракожия",
                Phone = "89823487623"
            };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.RegistrateLead(leadInsertRequest));
            VerifyLoggerError("Error: You must specify email");
        }

        [Test]
        public async Task RegistrateLead_IncorrectPhoneNumber_ThrowsValidationException()
        {
            // given
            var leadInsertRequest = new LeadInsertRequest
            {
                Name = "Mu",
                LastName = "Nikalaiv",
                Email = "reg@example.com",
                BirthDate = DateTime.Now,
                Password = "dslkjfjgh123",
                City = "Кракожия",
                Phone = "89823487623456452"
            };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.RegistrateLead(leadInsertRequest));
            VerifyLoggerError("Error: Phone number is not valid"); 
        }

        [Test]
        public async Task RegistrateLead_PasswordHas6Symbols_ThrowsValidationException()
        {
            // given
            var leadInsertRequest = new LeadInsertRequest
            {
                Name = "Mu",
                LastName = "Nikalaiv",
                Email = "reg@example.com",
                BirthDate = DateTime.Now,
                Password = "123456",
                City = "Кракожия",
                Phone = "89817463857"
            };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.RegistrateLead(leadInsertRequest));
            VerifyLoggerError("Error: Minimum length of password is 8 symbols");
        }

        [Test]
        public async Task RegistrateLead_PasswordHas33Symbols_ThrowsValidationException()
        {
            // given
            var leadInsertRequest = new LeadInsertRequest
            {
                Name = "Mu",
                LastName = "Nikalaiv",
                Email = "reg@example.com",
                BirthDate = DateTime.Now,
                Password = "1234567891234567890912345jfidkger",
                City = "Кракожия",
                Phone = "89838573645"
            };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.RegistrateLead(leadInsertRequest));
            VerifyLoggerError("Error: Maximum length of password is 30 symbols");
        }

        [Test]
        public async Task RegistrateLead_IncorrectEmail_ThrowsValidationException()
        {
            // given
            var leadInsertRequest = new LeadInsertRequest
            {
                Name = "Mu",
                LastName = "Nikalaiv",
                Email = "regexplecom",
                BirthDate = DateTime.Now,
                Password = "12345678",
                City = "Кракожия",
                Phone = "89827462957"
            };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.RegistrateLead(leadInsertRequest));
            VerifyLoggerError("Error: Email address is not valid");
        }

        [Test]
        public async Task RegistrateLead_NameHas23Symbols_ThrowsValidationException()
        {
            // given
            var leadInsertRequest = new LeadInsertRequest
            {
                Name = "Mukdifjgnekrisoghdjtk",
                LastName = "Nikalaiv",
                Email = "reg@explecom",
                BirthDate = DateTime.Now,
                Password = "123456789123",
                City = "Кракожия",
                Phone = "89827846354"
            };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.RegistrateLead(leadInsertRequest));
            VerifyLoggerError("Error: Maximum length of the name is 20 symbols");
        }

        [Test]
        public async Task RegistrateLead_CityHas21Symbols_ThrowsValidationException()
        {
            // given
            var leadInsertRequest = new LeadInsertRequest
            {
                Name = "Mukdifjgnek",
                LastName = "Nikalaiv",
                Email = "reg@explecom",
                BirthDate = DateTime.Now,
                Password = "12345678912345",
                City = "Кракожияоудашыоагелпн",
                Phone = "89816745362"
            };

            // then
            Assert.ThrowsAsync<ValidationException>(async () => await _controller.RegistrateLead(leadInsertRequest));
            VerifyLoggerError("Error: Maximum length of city is 20 symbols");
        }

        private void VerifyLoggerError(string message)
        {
            _loggerMock.Verify(x => x.Log(LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals(message, o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
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
