using CarDealership.Data.Repositories.ADO;
using CarDealership.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Tests.IntegrationTests
{
    [TestFixture]
    public class ColorRepositoryTestsADO
    {
        [Test]
        public void CanGetAllColors()
        {
            ColorRepositoryADO repo = new ColorRepositoryADO();

            List<Color> Colors = repo.GetAll().ToList();

            Assert.AreEqual(5, Colors.Count);

            Assert.AreEqual(Colors[2].ColorId, 3);
            Assert.AreEqual(Colors[2].ColorName, "Gray");
        }

        [Test]
        public void CanGetColorById()
        {
            ColorRepositoryADO repo = new ColorRepositoryADO();

            Color Color = repo.GetAll().FirstOrDefault(c => c.ColorId == 3);

            Assert.AreEqual(Color.ColorId, 3);
            Assert.AreEqual(Color.ColorName, "Gray");
        }
    }
}
