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
    public class GetServiceByIdTestCaseSource : IEnumerable
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


            var expected = new ServiceModel
            {
                Id = 1,
                Name = "Тренинг",
                Description = "БлаБлаБла",
                OneTimePrice = 1500
            };

            yield return new object[] { service, expected };

            var service1 = new Service
            {
                Id = 30,
                Name = "ЕНКГЕКГЕКГЕК",
                Description = "32123132",
                OneTimePrice = 20000
            };


            var expected1 = new ServiceModel
            {
                Id = 30,
                Name = "ЕНКГЕКГЕКГЕК",
                Description = "32123132",
                OneTimePrice = 20000
            };

            yield return new object[] { service1, expected1 };

        }
    }
}
