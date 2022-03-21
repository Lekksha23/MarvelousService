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
    public class AddServiceTestCaseSourse : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            var service = new ServiceModel
            {
                Id = 1,
                Name = "Тренинг",
                Description = "БлаБлаБла",
                Price = 1500
            };

            var expected = 1;

            yield return new object[] { service, expected };

        }
    }
}
