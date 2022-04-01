namespace MarvelousService.BusinessLayer.Exceptions
{
    public class AccountException : BadGatewayException
    {
        public AccountException(string message) : base(message) { }
    }
}
