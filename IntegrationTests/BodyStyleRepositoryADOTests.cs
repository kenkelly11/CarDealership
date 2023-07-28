using CarDealership.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Data.Repositories.ADO;

namespace CarDealership.Tests.IntegrationTests
{
    [TestFixture]
    public class BodyStyleRepositoryADOTests
    {
        [Test]
        public void CanGetAllBodyStyles()
        {
            BodyStyleRepositoryADO repo = new BodyStyleRepositoryADO();

            List<BodyStyle> bodyStyles = repo.GetAll().ToList();

            Assert.AreEqual(4, bodyStyles.Count);

            Assert.AreEqual(bodyStyles[2].BodyStyleId, 3);
            Assert.AreEqual(bodyStyles[2].BodyStyleType, "SUV");
        }

        [Test]
        public void CanGetBodyStyleById()
        {
            BodyStyleRepositoryADO repo = new BodyStyleRepositoryADO();

            BodyStyle bodyStyle = repo.GetAll().FirstOrDefault(b => b.BodyStyleId == 3);

            Assert.AreEqual(bodyStyle.BodyStyleId, 3);
            Assert.AreEqual(bodyStyle.BodyStyleType, "SUV");
        }
    }
}
