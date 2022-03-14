using MarvelousService.BusinessLayer.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MarvelousService.API.ExceptionResponse
{
    public class ValidationExceptionResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<ValidationError> Errors { get; set; }

        private const int ValidationCode = 1000;
        private const string MessageValidation = "Validation exception";

        public ValidationExceptionResponse(ValidationException exception)
        {
            Errors = new List<ValidationError>
            {
                new(){Code = ValidationCode, Field = exception.Field, Message = exception.Message}
            };
        }


        public ValidationExceptionResponse(ModelStateDictionary modelState)
        {
            Code = ValidationCode;
            Message = MessageValidation;

            foreach (var state in modelState)
            {
                if (state.Value.Errors.Count == 0)
                    continue;
                Errors.Add(new ValidationError
                {
                    Code = GetValidationCode(state.Value.Errors[0].ErrorMessage),
                    Field = state.Key,
                    Message = state.Value.Errors[0].ErrorMessage
                });
            }
        }

        private int GetValidationCode(string errorMessage)
        {
            throw new NotImplementedException(errorMessage);
        }
    }
}
