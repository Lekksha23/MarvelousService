

namespace MarvelousService.BusinessLayer.Exceptions
{
    public class ForbiddenException : BadGatewayException
    {
        public ForbiddenException(string message) : base(message) { }
    }
}
