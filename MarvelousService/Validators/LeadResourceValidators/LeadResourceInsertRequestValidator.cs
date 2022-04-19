using FluentValidation;
using MarvelousService.API.Models;

namespace MarvelousService.API.Validators
{
    public class LeadResourceInsertRequestValidator : AbstractValidator<LeadResourceInsertRequest>
    {
        public LeadResourceInsertRequestValidator()
        {
            RuleFor(x => x.Period)
                .NotEmpty()
                .InclusiveBetween(1, 4)
                .WithMessage("Please ensure you have entered Period inclusive between 1 and 4.");
            RuleFor(x => x.ResourceId)
                .NotEmpty()
                .InclusiveBetween(1, 5)
                .WithMessage("Please ensure you have entered ResourceId inclusive between 1 and 5.")
                .LessThan(3)
                .When(x => x.Period == 1)
                .WithMessage("You can't order first or second resources with 2 - week, 3 - month and 4 - year periods.");
        }
    }
}
