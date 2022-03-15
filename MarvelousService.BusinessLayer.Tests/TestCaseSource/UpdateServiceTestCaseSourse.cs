using MarvelousService.BusinessLayer.Models;
using MarvelousService.DataLayer.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelousService.BusinessLayer.Tests.TestCaseSource
{
    public class UpdateServiceTestCaseSourse : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            var service = new Service
            {
                Id = 1,
                Name = "Тренинг",
                Description = "БлаБлаБла",
                OneTimePrice = 1500
            };

            var updateService = new ServiceModel
            {
                Id = 1,
                Name = "qwewqe",
                Description = "asdasd",
                OneTimePrice = 1233
            };


            var expected = new ServiceModel
            {
                Id = 1,
                Name = "qwewqe",
                Description = "asdasd",
                OneTimePrice = 1233
            };

            yield return new object[] { service, updateService, expected };
        }
    }
}
