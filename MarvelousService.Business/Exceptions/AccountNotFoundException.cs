namespace MarvelousService.BusinessLayer.Exceptions
{
    public class AccountNotFoundException : BadGatewayException
    {
        public AccountNotFoundException(string message) : base(message) { }
    }
}
