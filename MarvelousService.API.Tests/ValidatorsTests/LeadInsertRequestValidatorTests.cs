using CRM.APILayer.Validation;
using FluentValidation.TestHelper;
using MarvelousService.API.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousService.API.Tests.ValidatorsTests
{
    public class LeadInsertRequestValidatorTests
    {

        private LeadInsertRequestValidator _validatorLead;

        [SetUp]
        public void SetUp()
        {
            _validatorLead = new LeadInsertRequestValidator();
        }

        [Test]
        public void LeadInsertRequestModel_IsValid_Ok()
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = "Qqq",
                LastName = "Qwe",
                Password = "12345678",
                BirthDate = DateTime.Parse("2022-04-15"),
                Phone  = "89657933475",
                Email = "sasas@mail.ru",
                City = "Magadan"

            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveAnyValidationErrors();
        }











    }
}
