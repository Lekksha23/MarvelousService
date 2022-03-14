
namespace MarvelousService.BusinessLayer.Exceptions
{
    public class ValidationException : Exception
    {
        public string Field { get; set; }

        public ValidationException(string field, string message) : base(message)
        {
            Field = field;
        }
    }
}
