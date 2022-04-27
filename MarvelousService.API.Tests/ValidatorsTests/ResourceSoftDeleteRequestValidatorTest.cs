using FluentValidation.TestHelper;
using MarvelousService.API.Models;
using MarvelousService.API.Validators;
using NUnit.Framework;

namespace MarvelousService.API.Tests.ValidatorsTests
{
    public class ResourceSoftDeleteRequestValidatorTest
    {
        private ResourceSoftDeleteRequestValidator _validatorDelete;

        [SetUp]
        public void SetUp()
        {
            _validatorDelete = new ResourceSoftDeleteRequestValidator();
        }

        [Test]
        public void ResourceSoftDeleteRequest_IsValid_Ok()
        {
            //given
            var resourse = new ResourceSoftDeleteRequest
            {
                IsDeleted = true
            };

            //when
            var validationResult = _validatorDelete.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveAnyValidationErrors();
        }

        [TestCase(null)]

        public void ResourceSoftDeleteRequest_IsDeletedOrNotNull(bool delete)
        {
            //given
            var resourse = new ResourceSoftDeleteRequest
            {
                IsDeleted = delete
            };

            //when
            var validationResult = _validatorDelete.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.IsDeleted);
        }

        [TestCase(true)]

        public void ResourceSoftDeleteRequest_IsDeletedOrExistst(bool delete)
        {
            //given
            var resourse = new ResourceSoftDeleteRequest
            {
                IsDeleted = delete
            };

            //when
            var validationResult = _validatorDelete.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.IsDeleted);
        }

    }
}
