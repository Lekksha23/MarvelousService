using FluentValidation;
using MarvelousService.API.Models;

namespace MarvelousService.API.Validators
{
    public class ResourceSoftDeleteRequestValidator : AbstractValidator<ResourceSoftDeleteRequest>
    {
        public ResourceSoftDeleteRequestValidator()
        {
            RuleFor(x => x.IsDeleted)
                .NotEmpty()
                .Must(x => x == false || x == true);

        }

    }
}
