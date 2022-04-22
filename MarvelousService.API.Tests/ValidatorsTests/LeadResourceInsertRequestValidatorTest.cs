using FluentValidation.TestHelper;
using MarvelousService.API.Models;
using MarvelousService.API.Validators;
using NUnit.Framework;



namespace MarvelousService.API.Tests.ValidatorsTests
{
    public class LeadResourceInsertRequestValidatorTest
    {
        private LeadResourceInsertRequestValidator _validatorLeadResource;

        [SetUp]
        public void SetUp()
        {
            _validatorLeadResource = new LeadResourceInsertRequestValidator();
        }


        [Test]
        public void LeadResourceInsertRequestModel_IsValid_Ok()
        {
            //given
            var resourse = new LeadResourceInsertRequest
            {
                ResourceId = 1,
                Period = 1
            };

            //when
            var validationResult = _validatorLeadResource.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveAnyValidationErrors();
        }

        [TestCase(null)]

        public void LeadResourceInsertRequestModel_NoService(int resourseId)
        {
            //given
            var resourse = new LeadResourceInsertRequest
            {
                ResourceId = resourseId,
                Period = 1
            };

            //when
            var validationResult = _validatorLeadResource.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.ResourceId);
        }

        [TestCase(null)]

        public void LeadResourceInsertRequestModel_NoPeriod(int period)
        {
            //given
            var resourse = new LeadResourceInsertRequest
            {
                ResourceId = 1,
                Period = period
            };

            //when
            var validationResult = _validatorLeadResource.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.Period);
        }
    }
}
