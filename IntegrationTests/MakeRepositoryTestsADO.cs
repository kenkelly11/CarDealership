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
    public class MakeRepositoryTestsADO
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
                    cmd.CommandText = "MakeInsert";
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
        public void CanGetAllMakes()
        {
            MakeRepositoryADO repo = new MakeRepositoryADO();

            List<Make> Makes = repo.GetAll().ToList();

            Assert.AreEqual(5, Makes.Count);

            Assert.AreEqual(Makes[2].MakeId, 3.ToString());
            Assert.AreEqual(Makes[2].MakeName, "Ford");
            Assert.AreEqual(Makes[2].DateAdded, new DateTime(2019, 6, 2));
        }

        [Test]
        public void CanGetMakeById()
        {
            MakeRepositoryADO repo = new MakeRepositoryADO();

            Make Make = repo.GetAll().FirstOrDefault(c => c.MakeId == 3.ToString());

            Assert.AreEqual(Make.MakeId, 3.ToString());
            Assert.AreEqual(Make.MakeName, "Ford");
            Assert.AreEqual(Make.DateAdded, new DateTime(2019, 6, 2));
        }

        [Test]
        public void CanAddMake()
        {
            Make make = new Make
            {
                MakeName = "TestMake",
                DateAdded = DateTime.Now.Date,
                AddedBy = "TestUser"

            };

            MakeRepositoryADO repo = new MakeRepositoryADO();
            repo.Insert(make);

            List<Make> makes = repo.GetAll().ToList();
            Assert.AreEqual(6, makes.Count);

            Assert.IsNotNull(makes[5].MakeId);
            Assert.AreEqual(make.MakeName, makes[5].MakeName);
            Assert.AreEqual(make.DateAdded, makes[5].DateAdded);
            Assert.AreEqual(make.AddedBy, makes[5].AddedBy);

        }
    }
}
