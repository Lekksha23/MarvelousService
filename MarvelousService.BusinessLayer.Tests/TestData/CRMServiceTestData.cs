using System.Collections.Generic;
using Marvelous.Contracts.Enums;
using MarvelousService.BusinessLayer.Models;

namespace MarvelousService.BusinessLayer.Tests.TestData
{
    public static class CRMServiceTestData
    {
        public static List<AccountModel> GetAccountModelListWithRubAccountForTests()
        {
            return new List<AccountModel>
            {
                 new AccountModel
                 {
                     Id = 1,
                     Balance = 20000,
                     CurrencyType = Currency.USD,
                     Name = "CleverName1",
                     IsBlocked = false
                 },
                 new AccountModel
                 {
                     Id = 2,
                     Balance = 23000,
                     CurrencyType = Currency.TRY,
                     Name = "CleverName2",
                     IsBlocked = false
                 },
                 new AccountModel
                 {
                     Id = 3,
                     Balance = 21000,
                     CurrencyType = Currency.RUB,
                     Name = "CleverName3",
                     IsBlocked = false
                 },
                 new AccountModel
                 {
                     Id = 4,
                     Balance = 27000,
                     CurrencyType = Currency.RSD,
                     Name = "CleverName4",
                     IsBlocked = false
                 }
            };
        }

        public static List<AccountModel> GetAccountModelListWithoutRubAccountForTests()
        {
            return new List<AccountModel>
            {
                 new AccountModel
                 {
                     Id = 1,
                     Balance = 20000,
                     CurrencyType = Currency.USD,
                     Lead = new LeadModel
                     {
                         Id = 1
                     },
                     Name = "CleverName1",
                     IsBlocked = false
                 },
                 new AccountModel
                 {
                     Id = 2,
                     Balance = 23000,
                     CurrencyType = Currency.TRY,
                     Name = "CleverName2",
                     IsBlocked = false
                 },
                 new AccountModel
                 {
                     Id = 3,
                     Balance = 21000,
                     CurrencyType = Currency.CNY,
                     Name = "CleverName3",
                     IsBlocked = false
                 },
                 new AccountModel
                 {
                     Id = 4,
                     Balance = 27000,
                     CurrencyType = Currency.RSD,
                     Name = "CleverName4",
                     IsBlocked = false
                 }
            };
        }
    }
}
