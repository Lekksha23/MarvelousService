using FluentValidation;
using MarvelousService.API.Models;

namespace MarvelousService.API.Validators
{
    public class LeadResourceInsertRequestValidator : AbstractValidator<LeadResourceInsertRequest>
    {
        public LeadResourceInsertRequestValidator()
        {
            RuleFor(x => x.ResourceId)
                .NotEmpty();
                
        }
    }
}
