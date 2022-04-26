using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Helpers;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Tests.TestCaseSource;
using NUnit.Framework;

namespace MarvelousService.BusinessLayer.Tests.RoleStrategyTests
{
    public class VIPRoleStrategyTests
    {
        [Test]
        public void GiveDiscountToLead_ShouldCountPriceWithDiscount()
        {
            // given
            var leadResourceModel = LeadResourceTestData.GetLeadResourceModelWithMonthPeriodForTests();
            var oldLeadResourceModel = new LeadResourceModel() 
            { 
                Id = leadResourceModel.Id,
                LeadId = leadResourceModel.LeadId,
                Period = leadResourceModel.Period,
                StartDate = leadResourceModel.StartDate,
                Status = leadResourceModel.Status,
                Price = leadResourceModel.Price
            };
            var expectedPrice = 1620.0M;
            var sut = new VipRoleStrategy();

            // when
            sut.GiveDiscountToLead(leadResourceModel, Role.Vip);

            // then 
            Assert.AreEqual(expectedPrice, leadResourceModel.Price);
            Assert.AreNotEqual(oldLeadResourceModel.Price, leadResourceModel.Price);
            Assert.AreEqual(oldLeadResourceModel.Id, leadResourceModel.Id);
            Assert.AreEqual(oldLeadResourceModel.LeadId, leadResourceModel.LeadId);
            Assert.AreEqual(oldLeadResourceModel.Period, leadResourceModel.Period);
            Assert.AreEqual(oldLeadResourceModel.Status, leadResourceModel.Status);
            Assert.AreEqual(oldLeadResourceModel.StartDate, leadResourceModel.StartDate);
        }
    }
}
