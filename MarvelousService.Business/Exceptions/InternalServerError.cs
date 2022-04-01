namespace MarvelousService.BusinessLayer.Exceptions
{
    public class InternalServerError : Exception
    {
        public InternalServerError(string message) : base(message) { }
    }
}
