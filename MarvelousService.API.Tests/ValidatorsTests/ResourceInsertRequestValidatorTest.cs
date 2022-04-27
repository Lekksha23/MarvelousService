using FluentValidation.TestHelper;
using MarvelousService.API.Models;
using MarvelousService.API.Validators;
using NUnit.Framework;


namespace MarvelousService.API.Tests.ValidatorsTests
{
    public class ResourceInsertRequestValidatorTest
    {
        private ResourceInsertRequestValidator _validatorInsert;

        [SetUp]
        public void SetUp()
        {
            _validatorInsert = new ResourceInsertRequestValidator();
        }


        [Test]
        public void ResourceInsertRequestModel_IsValid_Ok()
        {
            //given
            var resourse = new ResourceInsertRequest
            {
                Name = "Qwe",
                Description = "Super predligenie",
                Price = 1500,
                Type = 1,
            };

            //when
            var validationResult = _validatorInsert.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveAnyValidationErrors();
        }


        [TestCase("ՖանդրեյՉմո")]
        [TestCase(null)]
        [TestCase("أندريه")]
        [TestCase("アンドレイ")]
        public void ResourceInsertRequestModel_NoNameOrIncorrectCharactersUsed(string name)
        {
            //given
            var resourse = new ResourceInsertRequest
            {
                Name = name,
                Description = "Super predligenie",
                Price = 1500,
                Type = 1,
            };

            //when
            var validationResult = _validatorInsert.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.Name);
        }

        [TestCase("Qwe")]
        public void ResourceInsertRequestModel_ExistsName(string name)
        {
            //given
            var resourse = new ResourceInsertRequest
            {
                Name = name,
                Description = "Super predligenie",
                Price = 1500,
                Type = 1,
            };

            //when
            var validationResult = _validatorInsert.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.Name);
        }

        [TestCase(null)]
        [TestCase($"Eh bien, mon prince. Gênes et Lucques ne sont plus que des apanages, des поместья, de la famille Buonaparte. Non, je vous préviens que si vous ne me dites pas que nous avons la guerre, si vous vous permettez encore de pallier toutes les infamies, toutes les atrocités de cet Antichrist (ma parole, j'y crois) — je ne vous connais plus, vous n'êtes plus mon ami, vous n'êtes plus мой верный раб, comme vous dites 1. Ну, здравствуйте, здравствуйте. Je vois que je vous fais peur 2, садитесь и рассказывайте" +
            $"Так говорила в июле 1805 года известная Анна Павловна Шерер, фрейлина и приближенная императрицы Марии Феодоровны, встречая важного и чиновного князя Василия, первого приехавшего на ее вечер.Анна Павловна кашляла несколько дней, у нее был грипп, как она говорила(грипп был тогда новое слово, употреблявшееся только редкими).В записочках, разосланных утром с красным лакеем, было написано без различия во всех " +
            $"Si vous n'avez rien de mieux à faire, Monsieur le comte (или mon prince), et si la perspective de passer la soirée chez une pauvre malade ne vous effraye pas trop, je serai charmée de vous voir chez moi entre 7 et 10 heures. Annette Scherer» 3. " +
            $"— Dieu, quelle virulente sortie! 4 — отвечал, нисколько не смутясь такою встречей, вошедший князь, в придворном, шитом мундире, в чулках, башмаках и звездах, с светлым выражением плоского лица. Он говорил на том изысканном французском языке, на котором не только говорили, но и думали наши деды, и с теми, тихими, покровительственными интонациями, которые свойственны состаревшемуся в свете и при дворе значительному человеку.Он подошел к Анне Павловне, поцеловал ее руку, подставив ей свою надушенную и сияющую лысину, и покойно уселся на диване.— Avant tout dites - moi, comment vous allez, chère amie ? 5 Успокойте меня, — сказал он, не изменяя голоса и тоном, в ко")]
        public void ResourceInsertRequestModel_NoDescriptionOrIncorrectCharactersUsed(string description)
        {
            //given
            var resourse = new ResourceInsertRequest
            {
                Name = "Qwe",
                Description = description,
                Price = 1500,
                Type = 1,
            };

            //when
            var validationResult = _validatorInsert.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.Description);
        }

        [TestCase("Qweqwe,qweqwe")]
        public void ResourceInsertRequestModel_ExiststDescription(string description)
        {
            //given
            var resourse = new ResourceInsertRequest
            {
                Name = "Qwe",
                Description = description,
                Price = 1500,
                Type = 1,
            };

            //when
            var validationResult = _validatorInsert.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.Description);
        }

        [TestCase(123123.123213)]
        [TestCase(null)]
        [TestCase(0.123123)]
        [TestCase(112323.1)]
        public void ResourceInsertRequestModel_NoPriceOrIncorrectCharactersUsed(decimal price)
        {
            //given
            var resourse = new ResourceInsertRequest
            {
                Name = "Qwe",
                Description = "Super predligenie",
                Price = price,
                Type = 1,
            };

            //when
            var validationResult = _validatorInsert.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.Price);
        }

        [TestCase(123123.0)]
        [TestCase(12323)]
        public void ResourceInsertRequestModel_ExiststPrice(decimal price)
        {
            //given
            var resourse = new ResourceInsertRequest
            {
                Name = "Qwe",
                Description = "Super predligenie",
                Price = price,
                Type = 1,
            };

            //when
            var validationResult = _validatorInsert.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.Price);
        }

        [TestCase(null)]
        public void ResourceInsertRequestModel_NoTypeOrIncorrectCharactersUsed(int type)
        {
            //given
            var resourse = new ResourceInsertRequest
            {
                Name = "Qwe",
                Description = "Super predligenie",
                Price = 1500,
                Type = type,
            };

            //when
            var validationResult = _validatorInsert.TestValidate(resourse);

            // then
            validationResult.ShouldHaveValidationErrorFor(resourse => resourse.Type);
        }

        [TestCase(1)]
        public void ResourceInsertRequestModel_ExistsType(int type)
        {
            //given
            var resourse = new ResourceInsertRequest
            {
                Name = "Qwe",
                Description = "Super predligenie",
                Price = 1500,
                Type = type,
            };

            //when
            var validationResult = _validatorInsert.TestValidate(resourse);

            // then
            validationResult.ShouldNotHaveValidationErrorFor(resourse => resourse.Type);
        }

    }
}
