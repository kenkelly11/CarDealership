﻿using CarDealership.Data.Repositories.ADO;
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
    public class ModelRepositoryTestsADO
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
                    cmd.CommandText = "ModelInsert";
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
        public void CanGetAllModels()
        {
            ModelRepositoryADO repo = new ModelRepositoryADO();

            List<Model> Models = repo.GetAll().ToList();

            Assert.AreEqual(5, Models.Count);

            Assert.AreEqual(Models[2].ModelId, 3);
            Assert.AreEqual(Models[2].ModelName, "TLX");
            Assert.AreEqual(Models[2].DateAdded, new DateTime(2020, 7, 2));
        }

        [Test]
        public void CanGetModelById()
        {
            ModelRepositoryADO repo = new ModelRepositoryADO();

            Model Model = repo.GetModelById(3);

            Assert.AreEqual(Model.ModelId, 3);
            Assert.AreEqual(Model.ModelName, "TLX");
            Assert.AreEqual(Model.DateAdded, new DateTime(2020, 7, 2));
        }

        [Test]
        public void CanGetModelByMakeId()
        {
            ModelRepositoryADO repo = new ModelRepositoryADO();

            List<Model> models = new List<Model>();

            models = repo.GetModelsByMakeId(2);

            Assert.AreEqual(models[0].ModelId, 3);
            Assert.AreEqual(models[0].ModelName, "TLX");
            Assert.AreEqual(models[0].DateAdded, new DateTime(2020, 7, 2));
        }

        [Test]
        public void CanAddModel()
        {
            Model model = new Model
            {
                MakeId = 2,
                ModelName = "TestModel",
                DateAdded = DateTime.Now.Date,
                Addedby = "admin3@test.com"
            };

            ModelRepositoryADO repo = new ModelRepositoryADO();
            repo.Insert(model);

            List<Model> Models = repo.GetAll().ToList();
            Assert.AreEqual(6, Models.Count);

            Assert.IsNotNull(Models[5].ModelId);
            Assert.AreEqual(2, Models[5].MakeId);
            Assert.AreEqual(model.ModelName, Models[5].ModelName);
            Assert.AreEqual(model.DateAdded, Models[5].DateAdded);
            Assert.AreEqual(model.Addedby, Models[5].Addedby);
        }
    }
}
