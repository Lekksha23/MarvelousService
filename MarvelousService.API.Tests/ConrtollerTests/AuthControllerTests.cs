using Marvelous.Contracts.Enums;
using Marvelous.Contracts.RequestModels;
using MarvelousService.API.Controllers;
using MarvelousService.BusinessLayer.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MarvelousService.API.Tests
{
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly Mock<ILogger<AuthController>> _loggerMock;
        private readonly Mock<IRequestHelper> _requestHelperMock;

        public AuthControllerTests()
        {
            _loggerMock = new Mock<ILogger<AuthController>>();
            _requestHelperMock = new Mock<IRequestHelper>();
            _controller = new AuthController(_loggerMock.Object, _requestHelperMock.Object);
        }

        [Test]
        public async Task Login()
        {
            // given
            var authModel = new AuthRequestModel { Email = "blablabla", Password = "Grivja" };
            var token = "IndicativeToken";
            AddContext(token);
            _requestHelperMock.Setup(m => m.GetTokenForFront(Microservice.MarvelousAuth, authModel)).ReturnsAsync(token);
            
            // when
            var actual = await _controller.Login(authModel);

            // then
            _requestHelperMock.Verify(m => m.GetTokenForFront(Microservice.MarvelousAuth, authModel), Times.Once);
            VerifyLoggerInformation($"Query for authentication user with email:{ authModel.Email}.");
            VerifyLoggerInformation($"Authentication for user with email:{ authModel.Email} successfully completed.");
        }

        private void VerifyLoggerInformation(string message)
        {
            _loggerMock.Verify(x => x.Log(LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals(message, o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }

        private void AddContext(string token)
        {
            var context = new DefaultHttpContext();
            context.Request.Headers.Authorization = token;
            _controller.ControllerContext.HttpContext = context;
        }
    }
}
