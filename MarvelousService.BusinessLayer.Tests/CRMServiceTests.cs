using MarvelousService.BusinessLayer.Clients;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Tests.TestData;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MarvelousService.BusinessLayer.Tests
{
    public class CrmServiceTests
    {
        private Mock<ICrmClient> _crmClientMock;
        private readonly Mock<ILogger<CrmService>> _loggerMock;

        public CrmServiceTests()
        {
            _loggerMock = new Mock<ILogger<CrmService>>();
        }

        [SetUp]
        public void Setup()
        {
            _crmClientMock = new Mock<ICrmClient>();
        }

        [Test]
        public async Task GetIdOfRubLeadAccount_ShouldCheckIfListContainsRubLeadAccount()
        {
            // given
            var leadAccounts = CRMServiceTestData.GetAccountModelListWithRubAccountForTests();
            var token = "IndicativeToken";
            _crmClientMock.Setup(m => m.GetLeadAccounts(token)).ReturnsAsync(leadAccounts);
            var sut = new CrmService(_crmClientMock.Object, _loggerMock.Object);

            // when
            var actual = await sut.GetIdOfRubLeadAccount(token);

            // then
            Assert.NotNull(actual);
            Assert.AreEqual(leadAccounts[2].Id, actual);
            _crmClientMock.Verify(m => m.GetLeadAccounts(token), Times.Once);
            VerifyLoggerInformation("All Accounts for Lead from CRM were received.");
            VerifyLoggerInformation("Query for getting all Accounts for Lead from CRM.");
        }

        [Test]
        public async Task GetIdOfRubLeadAccount_ShouldThrowAccountNotFoundException()
        {
            // given
            var leadAccounts = CRMServiceTestData.GetAccountModelListWithoutRubAccountForTests();
            var token = "IndicativeToken";
            _crmClientMock.Setup(m => m.GetLeadAccounts(token)).ReturnsAsync(leadAccounts);
            var sut = new CrmService(_crmClientMock.Object, _loggerMock.Object);

            // then
            Assert.ThrowsAsync<AccountNotFoundException>(async () => await sut.GetIdOfRubLeadAccount(token));
            _crmClientMock.Verify(m => m.GetLeadAccounts(token), Times.Once);
            _loggerMock.Verify(x => x.Log(LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("There's no accounts with RUB CurrencyType was found in CRM for Lead with id 1.", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
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
