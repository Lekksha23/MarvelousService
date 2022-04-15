using Marvelous.Contracts.Enums;
using Marvelous.Contracts.RequestModels;
using MarvelousService.BusinessLayer.Clients;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MarvelousService.BusinessLayer.Tests
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransactionStoreClient> _transactionClientMock;
        private readonly Mock<ILogger<TransactionService>> _loggerMock;

        public TransactionServiceTests()
        {
            _loggerMock = new Mock<ILogger<TransactionService>>();
            _transactionClientMock = new Mock<ITransactionStoreClient>();
        }

        [Test]
        public async Task AddResourceTransaction_ReturnsTransactionId()
        {
            // given
            var accountId = 23;
            var totalPrice = 3000.0M;
            var transactionId = 40000000;
            var resourceTransaction = new TransactionRequestModel
            {
                Amount = totalPrice,
                Currency = Currency.RUB,
                AccountId = accountId
            };
            _transactionClientMock.Setup(m => m.AddResourceTransaction(resourceTransaction)).ReturnsAsync(transactionId);
            var sut = new TransactionService(_transactionClientMock.Object, _loggerMock.Object);

            // when
            var actual = await sut.AddResourceTransaction(accountId, totalPrice);

            // then
            Assert.NotNull(actual);
            Assert.AreEqual(transactionId, actual);
            _transactionClientMock.Verify(m => m.AddResourceTransaction(It.IsAny<TransactionRequestModel>()), Times.Once);
            _loggerMock.Verify(x => x.Log(LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("Query for adding a resource transaction in TransactionStore", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }
    }
}
