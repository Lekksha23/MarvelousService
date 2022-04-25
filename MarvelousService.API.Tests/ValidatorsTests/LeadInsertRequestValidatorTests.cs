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

        [TestCase("Qweertbnghfjdkncbvhwiri")]
        [TestCase(null)]

        public void LeadInsertRequestModel_NoNameOrIncorrectCharactersUsed(string name)
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = name,
                LastName = "Qwe",
                Password = "12345678",
                BirthDate = DateTime.Parse("2022-04-15"),
                Phone = "89657933475",
                Email = "sasas@mail.ru",
                City = "Magadan"
            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.Name);
        }

        [TestCase("Qweert")]
        [TestCase("Андрей")]

        public void LeadInsertRequestModel_ExistsName(string name)
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = name,
                LastName = "Qwe",
                Password = "12345678",
                BirthDate = DateTime.Parse("2022-04-15"),
                Phone = "89657933475",
                Email = "sasas@mail.ru",
                City = "Magadan"
            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.Name);
        }

        [TestCase("Qweertbnghfjdkncbvhwiri")]
        [TestCase(null)]

        public void LeadInsertRequestModel_NoLastNameOrIncorrectCharactersUsed(string lastName)
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = "Qwe",
                LastName = lastName,
                Password = "12345678",
                BirthDate = DateTime.Parse("2022-04-15"),
                Phone = "89657933475",
                Email = "sasas@mail.ru",
                City = "Magadan"
            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.LastName);
        }

        [TestCase("Qweertbng")]
        [TestCase("Пупкин")]

        public void LeadInsertRequestModel_ExistsLastName(string lastName)
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = "Qwe",
                LastName = lastName,
                Password = "12345678",
                BirthDate = DateTime.Parse("2022-04-15"),
                Phone = "89657933475",
                Email = "sasas@mail.ru",
                City = "Magadan"
            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.LastName);
        }

        [TestCase("221421424125345534wqeqweqweqweqe21312312321qwqewqewq21312314124wewerewr")]
        [TestCase("123")]
        [TestCase(null)]

        public void LeadInsertRequestModel_NoPasswordOrIncorrectCharactersUsed(string password)
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = "Qwe",
                LastName = "Qertwuy",
                Password = password,
                BirthDate = DateTime.Parse("2022-04-15"),
                Phone = "89657933475",
                Email = "sasas@mail.ru",
                City = "Magadan"
            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.Password);
        }

        [TestCase("Qweertbngh")]
        [TestCase("123123213")]

        public void LeadInsertRequestModel_EXistsPasswordO(string password)
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = "Qwe",
                LastName = "Qertwuy",
                Password = password,
                BirthDate = DateTime.Parse("2022-04-15"),
                Phone = "89657933475",
                Email = "sasas@mail.ru",
                City = "Magadan"
            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.Password);
        }

        [TestCase(null)]

        public void LeadInsertRequestModel_NoBirthDateOrIncorrectCharactersUsed(DateTime birthDate)
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = "Qwe",
                LastName = "Qertwuy",
                Password = "12313132",
                BirthDate = birthDate,
                Phone = "89657933475",
                Email = "sasas@mail.ru",
                City = "Magadan"
            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.BirthDate);
        }

        [TestCase("09856())^$786")]
        [TestCase("wqeqweqweqeqw")]

        public void LeadInsertRequestModel_NoPhoneOrIncorrectCharactersUsed(string phone)
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = "Qwe",
                LastName = "Qertwuy",
                Password = "12313132",
                BirthDate = DateTime.Parse("2022-04-15"),
                Phone = phone,
                Email = "sasas@mail.ru",
                City = "Magadan"
            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.Phone);
        }

        [TestCase(null)]
        [TestCase("89659876452")]

        public void LeadInsertRequestModel_EXistsPhone(string phone)
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = "Qwe",
                LastName = "Qertwuy",
                Password = "12313132",
                BirthDate = DateTime.Parse("2022-04-15"),
                Phone = phone,
                Email = "sasas@mail.ru",
                City = "Magadan"
            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.Phone);
        }

        [TestCase("09856())^$786")]
        [TestCase("wqeqweqweqeqw")]
        [TestCase(null)]

        public void LeadInsertRequestModel_NoEmailOrIncorrectCharactersUsed(string email)
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = "Qwe",
                LastName = "Qertwuy",
                Password = "12313132",
                BirthDate = DateTime.Parse("2022-04-15"),
                Phone = "89657933475",
                Email = email,
                City = "Magadan"
            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.Email);
        }


        [TestCase("qweqwe@mail.ru")]
        [TestCase("qweqwe@yande.com")]

        public void LeadInsertRequestModel_ExistsEmail(string email)
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = "Qwe",
                LastName = "Qertwuy",
                Password = "12313132",
                BirthDate = DateTime.Parse("2022-04-15"),
                Phone = "89657933475",
                Email = email,
                City = "Magadan"
            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.Email);
        }


        [TestCase("Qwqeqweqweqeqwwqeqweq")]

        public void LeadInsertRequestModel_NoCityOrIncorrectCharactersUsed(string city)
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = "Qwe",
                LastName = "Qertwuy",
                Password = "12313132",
                BirthDate = DateTime.Parse("2022-04-15"),
                Phone = "89657933475",
                Email = "sasas@mail.ru",
                City = city
            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.City);
        }

        [TestCase(null)]

        public void LeadInsertRequestModel_ExistsCityOrIncorrectCharactersUsed(string city)
        {
            //given
            var resourse = new LeadInsertRequest
            {
                Name = "Qwe",
                LastName = "Qertwuy",
                Password = "12313132",
                BirthDate = DateTime.Parse("2022-04-15"),
                Phone = "89657933475",
                Email = "sasas@mail.ru",
                City = city
            };

            //when
            var validationResult = _validatorLead.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.City);
        }

    }
}
