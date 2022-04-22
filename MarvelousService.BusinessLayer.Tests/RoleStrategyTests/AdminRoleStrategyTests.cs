using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Exceptions;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Tests.TestCaseSource;
using NUnit.Framework;

namespace MarvelousService.BusinessLayer.Tests.RoleStrategyTests
{
    public class AdminRoleStrategyTests
    {
        [Test]
        public void GiveDiscountToLead_ShouldThrowRoleException()
        {
            // given
            var leadResourceModel = LeadResourceTestData.GetLeadResourceModelWithMonthPeriodForTests();
            var sut = new AdminRoleStrategy();

            // then 
            Assert.Throws<RoleException>(() => sut.GiveDiscountToLead(leadResourceModel, Role.Admin));
        }
    }
}
