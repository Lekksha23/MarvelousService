namespace MarvelousService.API.ExceptionResponse
{
    public class ValidationError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Field { get; set; }
    }
}
