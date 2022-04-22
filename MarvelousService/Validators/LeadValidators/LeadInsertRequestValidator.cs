using FluentValidation;
using MarvelousService.API.Models;
using System.Text.RegularExpressions;

namespace CRM.APILayer.Validation
{
    public class LeadInsertRequestValidator : AbstractValidator<LeadInsertRequest>
    {
        public LeadInsertRequestValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("you must specify password")
                .MinimumLength(8)
                .WithMessage("Minimum length of password is 8 symbols")
                .MaximumLength(30)
                .WithMessage("Maximum length of password is 30 symbols");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("You must specify email")
                .EmailAddress()
                .WithMessage("Email address is not valid");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("You must specify name")
                .MaximumLength(20)
                .WithMessage("Maximum length of name is 20 symbols");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("You must specify last name")
                .MaximumLength(20)
                .WithMessage("Maximum length of last name is 20 symbols");
            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("You must specify birthday");
            RuleFor(x => x.Phone)
                .Matches(new Regex(@"^\+?(?:[0-9]?){6,14}[0-9]$"))
                .WithMessage("Phone number is not valid");
            RuleFor(x => x.City)
                .MaximumLength(20)
                .WithMessage("Maximum length of city is 20 symbols");
        }
    }
}