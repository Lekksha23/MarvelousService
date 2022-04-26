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
        [TestCase(0)]
        [TestCase(-1)]

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


        [TestCase(1)]
        [TestCase(2)]

        public void LeadResourceInsertRequestModel_ServiceExists(int resourseId)
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
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.ResourceId);
        }
        



        [TestCase(null)]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(6)]

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


        [TestCase(1)]

        public void LeadResourceInsertRequestModel_ExistsPeriod(int period)
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
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.Period);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]

        public void LeadResourceInsertRequestModel_ExistsPeriodThree(int period)
        {
            //given
            var resourse = new LeadResourceInsertRequest
            {
                ResourceId = 3,
                Period = period
            };

            //when
            var validationResult = _validatorLeadResource.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.Period);
        }
    }
}
