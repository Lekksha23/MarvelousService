using MarvelousService.BusinessLayer.Tests.TestCaseSource;
using NUnit.Framework;

namespace MarvelousService.BusinessLayer.Tests.ModelTests
{
    public class LeadResourceModelTests
    {
        [Test]
        public void CountPrice_LeadResourceModelWithOneTimePeriod_ReturnsPriceForOneTime()
        {
            // given
            var expectedPrice = 1800.0M;
            var sut = LeadResourceTestData.GetLeadResourceModelWithOneTimePeriodForTests();

            // when
            var actual = sut.CountPrice();

            // then
            Assert.AreEqual(expectedPrice, actual);
        }

        [Test]
        public void CountPrice_LeadResourceModelWithWeekPeriod_ReturnsPriceForWeek()
        {
            // given
            var expectedPrice = 3600.0M;
            var sut = LeadResourceTestData.GetLeadResourceModelWithWeekPeriodForTests();

            // when
            var actual = sut.CountPrice();

            // then
            Assert.AreEqual(expectedPrice, actual);
        }

        [Test]
        public void CountPrice_LeadResourceModelWithMonthPeriod_ReturnsPriceForMonth()
        {
            // given
            var expectedPrice = 7200.0M;
            var sut = LeadResourceTestData.GetLeadResourceModelWithMonthPeriodForTests();

            // when
            var actual = sut.CountPrice();

            // then
            Assert.AreEqual(expectedPrice, actual);
        }

        [Test]
        public void CountPrice_LeadResourceModelWithYearPeriod_ReturnsPriceForYear()
        {
            // given
            var expectedPrice = 36000.0M;
            var sut = LeadResourceTestData.GetLeadResourceModelWithYearPeriodForTests();

            // when
            var actual = sut.CountPrice();

            // then
            Assert.AreEqual(expectedPrice, actual);
        }
    }
}
