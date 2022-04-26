using Microsoft.Extensions.Logging;
using Moq;
using System;


namespace MarvelousService.API.Tests
{
    public static class VerifyLoggerHelper
    {

        internal static void LoggerVerify<T>(Mock<ILogger<T>> logger,string message, LogLevel logLevel)
        {
            logger.Verify(
                x => x.Log(
                    logLevel,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals(message, o.ToString(),
                    StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }
    }
}
