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
    public class UserRepositoryTestsADO
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
                    cmd.CommandText = "UserInsert";
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
        public void CanGetAllUsers()
        {
            UserRepositoryADO repo = new UserRepositoryADO();

            List<User> users = repo.GetUsers().ToList();

            Assert.AreEqual(4, users.Count);

            Assert.AreEqual("00000000-0000-0000-0000-000000000000", users[0].Id);
            Assert.AreEqual("Sales", users[0].UserRole);
            Assert.AreEqual("sales1@test.com", users[0].UserName);
            Assert.AreEqual("sales1@test.com", users[0].Email);
            Assert.IsTrue(string.IsNullOrEmpty(users[0].PhoneNumber));
            Assert.IsFalse(string.IsNullOrEmpty(users[0].PasswordHash));
            Assert.IsFalse(string.IsNullOrEmpty(users[0].SecurityStamp));
            Assert.IsFalse(users[0].EmailConfirmed);
            Assert.IsFalse(users[0].PhoneNumberConfirmed);
            Assert.IsFalse(users[0].TwoFactorEnabled);
            Assert.AreEqual(users[0].LockoutEndDateUtc, DateTime.MinValue);
            Assert.IsFalse(users[0].LockoutEnabled);
            Assert.AreEqual(0, users[0].AccessFailedCount);

        }

        [Test]
        public void CanGetUserById()
        {
            UserRepositoryADO repo = new UserRepositoryADO();

            User user = repo.GetUserById("00000000-0000-0000-0000-000000000000");

            Assert.AreEqual("00000000-0000-0000-0000-000000000000", user.Id);
            Assert.AreEqual("Sales", user.UserRole);
            Assert.AreEqual("sales1@test.com", user.UserName);
            Assert.AreEqual("sales1@test.com", user.Email);
            Assert.IsTrue(string.IsNullOrEmpty(user.PhoneNumber));
            Assert.IsFalse(string.IsNullOrEmpty(user.PasswordHash));
            Assert.IsFalse(string.IsNullOrEmpty(user.SecurityStamp));
            Assert.IsFalse(user.EmailConfirmed);
            Assert.IsFalse(user.PhoneNumberConfirmed);
            Assert.IsFalse(user.TwoFactorEnabled);
            Assert.AreEqual(user.LockoutEndDateUtc, DateTime.MinValue);
            Assert.IsFalse(user.LockoutEnabled);
            Assert.AreEqual(0, user.AccessFailedCount);
        }

        [Test]
        public void CanAddUser()
        {
            User user = new User
            {
                Id = "Added-Test-User",
                UserName = "Added User",
                Email = "addeduser@test.com",
                AccessFailedCount = 0,
                TwoFactorEnabled = false,
                EmailConfirmed = false,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                LockoutEndDateUtc = null,
                LockoutEnabled = false,
                PasswordHash = null,
                SecurityStamp = null,
                UserRole = "Admin"
            };

            UserRepositoryADO repo = new UserRepositoryADO();
            repo.Insert(user);

            List<User> users = repo.GetUsers().ToList();
            Assert.AreEqual(5, users.Count);

            Assert.AreEqual("Added-Test-User", users[4].Id);
            Assert.AreEqual("Added User", users[4].UserName);
            Assert.AreEqual("addeduser@test.com", users[4].Email);
            Assert.AreEqual(0, users[4].AccessFailedCount);
            Assert.IsFalse(users[4].TwoFactorEnabled);
            Assert.IsFalse(users[4].EmailConfirmed);
            Assert.IsFalse(users[4].PhoneNumberConfirmed);
            Assert.IsFalse(users[4].LockoutEnabled);
            Assert.IsTrue(string.IsNullOrEmpty(users[4].PhoneNumber));
            Assert.IsTrue(users[4].LockoutEndDateUtc == DateTime.MinValue);
            Assert.IsTrue(string.IsNullOrEmpty(users[4].PasswordHash));
            Assert.IsTrue(string.IsNullOrEmpty(users[4].SecurityStamp));
            Assert.AreEqual("Admin", users[4].UserRole);
        }

        [Test]
        public void CanUpdateUser()
        {
            User user = new User
            {
                Id = "Added-Test-User",
                UserName = "Added User",
                Email = "addeduser@test.com",
                AccessFailedCount = 0,
                TwoFactorEnabled = false,
                EmailConfirmed = false,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                LockoutEndDateUtc = null,
                LockoutEnabled = false,
                PasswordHash = null,
                SecurityStamp = null,
                UserRole = "Admin"
            };

            UserRepositoryADO repo = new UserRepositoryADO();
            repo.Insert(user);

            user.UserName = "Updated User";
            user.Email = "updateduser@test.com";
            user.TwoFactorEnabled = true;
            user.PhoneNumber = "804-555-5555";
            user.PhoneNumberConfirmed = true;
            user.UserRole = "Sales";
            user.PasswordHash = "APTyyq+Bp99LHIKp2XOeOiLot5b/Li+db4pQdafI6FN6xfBhCkfOKzl/s0SQ5CjOfg==";

            repo.Update(user);

            List<User> users = repo.GetUsers().ToList();
            Assert.AreEqual(5, users.Count);

            Assert.AreEqual("Added-Test-User", users[4].Id);
            Assert.AreEqual("Updated User", users[4].UserName);
            Assert.AreEqual("APTyyq+Bp99LHIKp2XOeOiLot5b/Li+db4pQdafI6FN6xfBhCkfOKzl/s0SQ5CjOfg==", users[4].PasswordHash);
            Assert.AreEqual("updateduser@test.com", users[4].Email);
            Assert.AreEqual(0, users[4].AccessFailedCount);
            Assert.IsTrue(users[4].TwoFactorEnabled);
            Assert.IsFalse(users[4].EmailConfirmed);
            Assert.IsTrue(users[4].PhoneNumberConfirmed);
            Assert.IsFalse(users[4].LockoutEnabled);
            Assert.AreEqual("804-555-5555", users[4].PhoneNumber);
            Assert.IsTrue(users[4].LockoutEndDateUtc == DateTime.MinValue);
            Assert.IsTrue(string.IsNullOrEmpty(users[4].SecurityStamp));
            Assert.AreEqual("Sales", users[4].UserRole);
        }
    }
}
