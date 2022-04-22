using FluentValidation;
using MarvelousService.API.Models;
using System.Text.RegularExpressions;

namespace MarvelousService.API.Validators
{
    public class ResourceInsertRequestValidator : AbstractValidator<ResourceInsertRequest>
    {
        public ResourceInsertRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches(new Regex(@"^[а-яА-ЯёЁa-zA-Z]+$"))
                .WithMessage("Unavailable name");
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Price)
                .NotEmpty()
                .ScalePrecision(0, 10)
                .WithMessage("Price must not be more than 10 digits in total, with allowance for 0 decimals.");
            RuleFor(x => x.Type)
                .NotEmpty();

        }
    }
}
