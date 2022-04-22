using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Helpers;
using NUnit.Framework;

namespace MarvelousService.BusinessLayer.Tests
{
    public class RoleStrategyProviderTests
    {
        [Test]
        public void GetStrategy_RegularRole_ReturnsInstanceOfRegularRoleStrategyClass()
        {
            // given
            var role = (int)Role.Regular;
            var sut = new RoleStrategyProvider();

            // when
            var actual = sut.GetStrategy(role);

            // then 
            Assert.NotNull(actual);
            Assert.IsInstanceOf(typeof(RegularRoleStrategy), actual);
        }

        [Test]
        public void GetStrategy_VipRole_ReturnsInstanceOfVipRoleStrategyClass()
        {
            // given
            var role = (int)Role.Vip;
            var sut = new RoleStrategyProvider();

            // when
            var actual = sut.GetStrategy(role);

            // then 
            Assert.NotNull(actual);
            Assert.IsInstanceOf(typeof(VIPRoleStrategy), actual);
        }

        [Test]
        public void GetStrategy_AdminRole_ReturnsInstanceOfAdminRoleStrategyClass()
        {
            // given
            var role = (int)Role.Admin;
            var sut = new RoleStrategyProvider();

            // when
            var actual = sut.GetStrategy(role);

            // then 
            Assert.NotNull(actual);
            Assert.IsInstanceOf(typeof(AdminRoleStrategy), actual);
        }

        [Test]
        public void GetStrategy_UnknownRole_ReturnsNull()
        {
            // given
            var role = 42;
            var sut = new RoleStrategyProvider();

            // when
            var actual = sut.GetStrategy(role);

            // then 
            Assert.IsNull(actual);
        }
    }
}
