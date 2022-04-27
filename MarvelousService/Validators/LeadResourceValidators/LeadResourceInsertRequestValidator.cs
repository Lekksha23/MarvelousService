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
                .WithMessage("Value cannot be null")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Please ensure you have entered Period greater or equal to 1.")
                .LessThanOrEqualTo(4)
                .WithMessage("Please ensure you have entered Period less or equal to 4.");
            RuleFor(x => x.ResourceId)
                .NotEmpty()
                .WithMessage("Value cannot be null")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Please ensure you have entered ResourceId greater or equal to 1.")
                .LessThanOrEqualTo(5)
                .WithMessage("Please ensure you have entered ResourceId less or equal to 5.");

            When(x => x.ResourceId == 1, () =>
            {
                RuleFor(x => x.Period)
                .Equal(1)
                .WithMessage("You can't order onetime resource as subscription. Choose Period 1 for ResourceId 1 or 2");
            });
            When(x => x.ResourceId == 2, () =>
            {
                RuleFor(x => x.Period)
                .Equal(1)
                .WithMessage("You can't order onetime resource as subscription. Choose Period 1 for ResourceId 1 or 2");
            });
        }
    }
}
