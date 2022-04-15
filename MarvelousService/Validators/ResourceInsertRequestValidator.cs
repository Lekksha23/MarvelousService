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
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Price)
                //.Match(new Regex(@"[0-9]+.[0-9]{2}$"))
                .NotEmpty()
                .WithMessage("Birthday is empty");
            RuleFor(x => x.Type)
                //.Matches(new Regex(@"^[\+?(?:[0-9]?){6,14}[0-9]]+$"))
                .NotEmpty()
                .IsInEnum();

        }
    }
}
