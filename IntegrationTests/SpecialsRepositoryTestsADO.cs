using CarDealership.Data.Repositories.ADO;
using CarDealership.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Tests.IntegrationTests
{
    [TestFixture]
    public class SpecialsRepositoryTestsADO
    {
        [SetUp]
        public void Init()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString);

            try
            {
                using (dbConnection)
                {
                    var cmd = new SqlCommand();
                    cmd.CommandText = "SpecialInsert";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Connection = dbConnection;
                    dbConnection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format(CultureInfo.CurrentCulture,
                          "Exception Type: {0}, Message: {1}{2}",
                          ex.GetType(),
                          ex.Message,
                          ex.InnerException == null ? String.Empty :
                          String.Format(CultureInfo.CurrentCulture,
                                       " InnerException Type: {0}, Message: {1}",
                                       ex.InnerException.GetType(),
                                       ex.InnerException.Message));

                System.Diagnostics.Debug.WriteLine(errorMessage);

                dbConnection.Close();
            }
        }
        [TearDown]
        public void SpecialDelete()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString);

            try
            {
                using (dbConnection)
                {
                    var cmd = new SqlCommand();
                    cmd.CommandText = "SpecialDelete";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Connection = dbConnection;
                    dbConnection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format(CultureInfo.CurrentCulture,
                          "Exception Type: {0}, Message: {1}{2}",
                          ex.GetType(),
                          ex.Message,
                          ex.InnerException == null ? String.Empty :
                          String.Format(CultureInfo.CurrentCulture,
                                       " InnerException Type: {0}, Message: {1}",
                                       ex.InnerException.GetType(),
                                       ex.InnerException.Message));

                System.Diagnostics.Debug.WriteLine(errorMessage);

                dbConnection.Close();
            }
        }
        [TearDown]
        public void TearDown()
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CarDealership"].ConnectionString);

            try
            {
                using (dbConnection)
                {
                    var cmd = new SqlCommand();
                    cmd.CommandText = "GuildCarsDbReset";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Connection = dbConnection;
                    dbConnection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format(CultureInfo.CurrentCulture,
                          "Exception Type: {0}, Message: {1}{2}",
                          ex.GetType(),
                          ex.Message,
                          ex.InnerException == null ? String.Empty :
                          String.Format(CultureInfo.CurrentCulture,
                                       " InnerException Type: {0}, Message: {1}",
                                       ex.InnerException.GetType(),
                                       ex.InnerException.Message));

                System.Diagnostics.Debug.WriteLine(errorMessage);

                dbConnection.Close();
            }
        }

        [Test]
        public void CanGetAllSpecials()
        {
            SpecialsRepositoryADO repo = new SpecialsRepositoryADO();
            List<Specials> specials = repo.GetAll().ToList();

            Assert.AreEqual(4, specials.Count);

            Assert.AreEqual(2, specials[1].SpecialId);
            Assert.AreEqual("Free tank of gas with every purchase!", specials[1].SpecialDetails);
            Assert.AreEqual("Free tank of gas!", specials[1].Title);
        }

        [Test]
        public void CanAddSpecial()
        {
            Specials special = new Specials
            {
                SpecialDetails = "Test Special",
                Title = "Test title"
            };

            SpecialsRepositoryADO repo = new SpecialsRepositoryADO();

            repo.Insert(special);

            List<Specials> specials = repo.GetAll().ToList();

            Assert.AreEqual(5, specials.Count);

            Assert.IsNotNull(specials[4].SpecialId);
            Assert.AreEqual("Test Special", specials[4].SpecialDetails);
            Assert.AreEqual("Test title", specials[4].Title);
        }

        [Test]
        public void CanDeleteSpecial()
        {
            Specials special = new Specials
            {
                SpecialDetails = "Test Special",
                Title = "Test title"
            };

            SpecialsRepositoryADO repo = new SpecialsRepositoryADO();

            repo.Insert(special);

            List<Specials> specials = repo.GetAll().ToList();

            Assert.AreEqual(5, specials.Count);

            Assert.IsNotNull(specials[4].SpecialId);
            Assert.AreEqual("Test Special", specials[4].SpecialDetails);
            Assert.AreEqual("Test title", specials[4].Title);

            repo.Delete(special.SpecialId);

            List<Specials> updatedSpecials = repo.GetAll().ToList();

            Specials deletedSpecial = updatedSpecials.FirstOrDefault(s => s.SpecialId == 5);

            Assert.AreEqual(4, updatedSpecials.Count);

            Assert.IsNull(deletedSpecial);
        }
    }
}
