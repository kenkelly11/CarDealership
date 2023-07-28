using CarDealership.Data.Repositories.ADO;
using CarDealership.Models.Queries;
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
    public class ReportsRepositoryTestsADO
    {
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
        public void CanGetInventoryReport()
        {
            ReportsRepositoryADO repo = new ReportsRepositoryADO();

            List<InventoryReportListingItem> inventoryReportsList = repo.GetInventory().ToList();

            Assert.AreEqual(10, inventoryReportsList.Count);

            Assert.IsTrue(inventoryReportsList[8].IsNew);
            Assert.AreEqual("Acura", inventoryReportsList[8].Make);
            Assert.AreEqual("TLX", inventoryReportsList[8].Model);
            Assert.AreEqual(0, inventoryReportsList[8].UnitsInStock);
            Assert.AreEqual(34150m, inventoryReportsList[8].StockValue);
            Assert.AreEqual(new DateTime(2020, 1, 1), inventoryReportsList[8].Year);
        }

        [Test]
        public void CanGetSalesReport()
        {
            ReportsRepositoryADO repo = new ReportsRepositoryADO();

            List<SalesReportListingItem> salesReport = repo.GetSalesReport().ToList();

            Assert.AreEqual(2, salesReport.Count);

            Assert.AreEqual("11111111-1111-1111-1111-111111111111", salesReport[1].UserId);
            Assert.AreEqual("sales2@test.com", salesReport[1].UserName);
            Assert.AreEqual(2, salesReport[1].CarsSold);
            Assert.AreEqual(18000m, salesReport[1].Sales);
        }

        [Test]
        public void CanSearchOnMinDateChange()
        {
            ReportsRepositoryADO repo = new ReportsRepositoryADO();

            SalesSearchParameters parameters = new SalesSearchParameters
            {
                MaxDate = null,
                MinDate = new DateTime(2017, 1, 1),
                UserName = null
            };

            List<SalesReportListingItem> searchedSalesReport = repo.SearchSalesReports(parameters).ToList();

            Assert.AreEqual(1, searchedSalesReport.Count);

            Assert.AreEqual(27000, searchedSalesReport[0].Sales);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", searchedSalesReport[0].UserId);
            Assert.AreEqual("sales1@test.com", searchedSalesReport[0].UserName);
        }

        [Test]
        public void CanSearchOnMaxDateChange()
        {
            ReportsRepositoryADO repo = new ReportsRepositoryADO();

            SalesSearchParameters parameters = new SalesSearchParameters
            {
                MaxDate = new DateTime(2020, 1, 1),
                MinDate = null,
                UserName = null
            };

            List<SalesReportListingItem> searchedSalesReport = repo.SearchSalesReports(parameters).ToList();

            Assert.AreEqual(2, searchedSalesReport.Count);

            Assert.AreEqual(18000m, searchedSalesReport[1].Sales);
            Assert.AreEqual("11111111-1111-1111-1111-111111111111", searchedSalesReport[1].UserId);
            Assert.AreEqual("sales2@test.com", searchedSalesReport[1].UserName);
        }

        [Test]
        public void CanSearchOnUserName()
        {
            ReportsRepositoryADO repo = new ReportsRepositoryADO();

            SalesSearchParameters parameters = new SalesSearchParameters
            {
                MaxDate = null,
                MinDate = null,
                UserName = "sales1@test.com"
            };

            List<SalesReportListingItem> searchedSalesReport = repo.SearchSalesReports(parameters).ToList();

            Assert.AreEqual(1, searchedSalesReport.Count);

            Assert.AreEqual(27000, searchedSalesReport[0].Sales);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", searchedSalesReport[0].UserId);
            Assert.AreEqual("sales1@test.com", searchedSalesReport[0].UserName);
        }

        [Test]
        public void CanSearchMinAndMaxDateChange()
        {
            ReportsRepositoryADO repo = new ReportsRepositoryADO();

            SalesSearchParameters parameters = new SalesSearchParameters
            {
                MaxDate = new DateTime(2021, 4, 2),
                MinDate = new DateTime(2017, 1, 2),
                UserName = null
            };

            List<SalesReportListingItem> searchedSalesReport = repo.SearchSalesReports(parameters).ToList();

            Assert.AreEqual(1, searchedSalesReport.Count);

            Assert.AreEqual(10000m, searchedSalesReport[0].Sales);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", searchedSalesReport[0].UserId);
            Assert.AreEqual("sales1@test.com", searchedSalesReport[0].UserName);
        }

        [Test]
        public void CanSearchOnDateAndUserNameChange()
        {
            ReportsRepositoryADO repo = new ReportsRepositoryADO();

            SalesSearchParameters parameters = new SalesSearchParameters
            {
                MaxDate = new DateTime(2020, 1, 1),
                MinDate = new DateTime(2017, 1, 1),
                UserName = "sales1@test.com"
            };

            List<SalesReportListingItem> searchedSalesReport = repo.SearchSalesReports(parameters).ToList();

            Assert.AreEqual(1, searchedSalesReport.Count);

            Assert.AreEqual(27000m, searchedSalesReport[0].Sales);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", searchedSalesReport[0].UserId);
            Assert.AreEqual("sales1@test.com", searchedSalesReport[0].UserName);
        }

        [Test]
        public void CanReturnZeroResultsWrongUserName()
        {
            ReportsRepositoryADO repo = new ReportsRepositoryADO();

            SalesSearchParameters parameters = new SalesSearchParameters
            {
                MaxDate = new DateTime(2017, 4, 2),
                MinDate = new DateTime(2017, 1, 2),
                UserName = "sales2@test.com"
            };

            List<SalesReportListingItem> searchedSalesReport = repo.SearchSalesReports(parameters).ToList();

            Assert.AreEqual(0, searchedSalesReport.Count);
        }

        [Test]
        public void CanReturnZeroResultsOutOfDateRange()
        {
            ReportsRepositoryADO repo = new ReportsRepositoryADO();

            SalesSearchParameters testParametersOne = new SalesSearchParameters
            {
                MaxDate = new DateTime(2015, 4, 2),
                MinDate = new DateTime(2014, 1, 2),
                UserName = null
            };

            SalesSearchParameters testParametersTwo = new SalesSearchParameters
            {
                MaxDate = new DateTime(2018, 4, 2),
                MinDate = new DateTime(2017, 4, 2),
                UserName = null
            };

            List<SalesReportListingItem> searchedSalesReportOne = repo.SearchSalesReports(testParametersOne).ToList();

            List<SalesReportListingItem> searchedSalesReportTwo = repo.SearchSalesReports(testParametersOne).ToList();

            Assert.AreEqual(0, searchedSalesReportOne.Count);
            Assert.AreEqual(0, searchedSalesReportTwo.Count);
        }
    }
}
