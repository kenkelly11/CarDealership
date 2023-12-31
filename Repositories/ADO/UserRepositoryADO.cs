﻿using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Repositories.ADO
{
    public class UserRepositoryADO : IUserRepository
    {
        public User GetUserById(string UserId)
        {
            User user = null;

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    dbConnection.Open();

                    SqlCommand cmd = new SqlCommand("SelectUserById", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", UserId); // add userId to end of SqlParametersCollection

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            user = new User();

                            user.Id = dr["Id"].ToString();
                            user.Email = dr["Email"].ToString();
                            user.EmailConfirmed = dr.GetBoolean(dr.GetOrdinal("EmailConfirmed"));
                            user.PhoneNumberConfirmed = dr.GetBoolean(dr.GetOrdinal("PhoneNumberConfirmed"));
                            user.TwoFactorEnabled = dr.GetBoolean(dr.GetOrdinal("TwoFactorEnabled"));
                            user.AccessFailedCount = (int)dr["AccessFailedCount"];
                            user.LockoutEnabled = dr.GetBoolean(dr.GetOrdinal("LockoutEnabled"));
                            user.PasswordHash = dr["PasswordHash"].ToString();
                            user.PhoneNumber = dr["PhoneNumber"].ToString();
                            user.LockoutEndDateUtc = (dr["LockoutEndDateUtc"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["LockoutEndDateUtc"]));
                            user.UserName = dr["UserName"].ToString();
                            user.SecurityStamp = dr["SecurityStamp"].ToString();
                            user.UserRole = dr["UserRole"].ToString();
                            user.FirstName = dr["FirstName"].ToString();
                            user.LastName = dr["LastName"].ToString();
                        }
                    }

                    return user;
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

                return user;
            }
        }

        public User GetUserByUserName(string UserName)
        {
            User user = null;

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    dbConnection.Open();

                    SqlCommand cmd = new SqlCommand("SelectUserByUserName", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", UserName);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            user = new User();

                            user.Id = dr["Id"].ToString();
                            user.Email = dr["Email"].ToString();
                            user.EmailConfirmed = dr.GetBoolean(dr.GetOrdinal("EmailConfirmed"));
                            user.PhoneNumberConfirmed = dr.GetBoolean(dr.GetOrdinal("PhoneNumberConfirmed"));
                            user.TwoFactorEnabled = dr.GetBoolean(dr.GetOrdinal("TwoFactorEnabled"));
                            user.AccessFailedCount = (int)dr["AccessFailedCount"];
                            user.LockoutEnabled = dr.GetBoolean(dr.GetOrdinal("LockoutEnabled"));
                            user.PasswordHash = dr["PasswordHash"].ToString();
                            user.PhoneNumber = dr["PhoneNumber"].ToString();
                            user.LockoutEndDateUtc = (dr["LockoutEndDateUtc"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["LockoutEndDateUtc"]));
                            user.UserName = dr["UserName"].ToString();
                            user.SecurityStamp = dr["SecurityStamp"].ToString();
                            user.UserRole = dr["UserRole"].ToString();
                            user.FirstName = dr["FirstName"].ToString();
                            user.LastName = dr["LastName"].ToString();
                        }
                    }

                    return user;
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

                return user;
            }
        }

        public IEnumerable<User> GetUsersByRole(string Role)
        {
            List<User> users = new List<User>();
            User user = null;

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    dbConnection.Open();

                    SqlCommand cmd = new SqlCommand("SelectUsersByRole", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Role", Role);



                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            user = new User();

                            user.Id = dr["Id"].ToString();
                            user.Email = dr["Email"].ToString();
                            user.EmailConfirmed = dr.GetBoolean(dr.GetOrdinal("EmailConfirmed"));
                            user.PhoneNumberConfirmed = dr.GetBoolean(dr.GetOrdinal("PhoneNumberConfirmed"));
                            user.TwoFactorEnabled = dr.GetBoolean(dr.GetOrdinal("TwoFactorEnabled"));
                            user.AccessFailedCount = (int)dr["AccessFailedCount"];
                            user.LockoutEnabled = dr.GetBoolean(dr.GetOrdinal("LockoutEnabled"));
                            user.PasswordHash = dr["PasswordHash"].ToString();
                            user.PhoneNumber = dr["PhoneNumber"].ToString();
                            user.LockoutEndDateUtc = (dr["LockoutEndDateUtc"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["LockoutEndDateUtc"]));
                            user.UserName = dr["UserName"].ToString();
                            user.SecurityStamp = dr["SecurityStamp"].ToString();
                            user.UserRole = dr["UserRole"].ToString();
                            user.FirstName = dr["FirstName"].ToString();
                            user.LastName = dr["LastName"].ToString();

                            users.Add(user);
                        }
                    }

                    return users;
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

                return users;
            }
        }
        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllUsers", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            User user = new User();

                            user.Id = dr["Id"].ToString();
                            user.Email = dr["Email"].ToString();
                            user.EmailConfirmed = dr.GetBoolean(dr.GetOrdinal("EmailConfirmed"));
                            user.PhoneNumberConfirmed = dr.GetBoolean(dr.GetOrdinal("PhoneNumberConfirmed"));
                            user.TwoFactorEnabled = dr.GetBoolean(dr.GetOrdinal("TwoFactorEnabled"));
                            user.AccessFailedCount = (int)dr["AccessFailedCount"];
                            user.LockoutEnabled = dr.GetBoolean(dr.GetOrdinal("LockoutEnabled"));
                            user.PasswordHash = dr["PasswordHash"].ToString();
                            user.PhoneNumber = dr["PhoneNumber"].ToString();
                            user.LockoutEndDateUtc = (dr["LockoutEndDateUtc"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["LockoutEndDateUtc"])); ;
                            user.UserName = dr["UserName"].ToString();
                            user.SecurityStamp = dr["SecurityStamp"].ToString();
                            user.UserRole = dr["UserRole"].ToString();
                            user.FirstName = dr["FirstName"].ToString();
                            user.LastName = dr["LastName"].ToString();

                            users.Add(user);
                        }
                    }
                    return users;
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
                return users;
            }
        }

        public void Insert(User user)
        {
            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UserInsert", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@UserId", SqlDbType.NVarChar);
                    param.Direction = ParameterDirection.InputOutput;
                    param.Size = 128;
                    param.SqlValue = user.Id;

                    cmd.Parameters.Add(param);

                    cmd.Parameters.AddWithValue("@UserName", user.UserName);

                    if (user.UserRole == null)
                    {
                        cmd.Parameters.AddWithValue("@UserRole", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@UserRole", user.UserRole);
                    }

                    if (user.FirstName == null)
                    {
                        cmd.Parameters.AddWithValue("@FirstName", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    }

                    if (user.LastName == null)
                    {
                        cmd.Parameters.AddWithValue("@LastName", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    }

                    if (user.LockoutEndDateUtc == null)
                    {
                        cmd.Parameters.AddWithValue("@LockoutEndDateUtc", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LockoutEndDateUtc", user.LockoutEndDateUtc);
                    }

                    cmd.Parameters.AddWithValue("@LockoutEnabled", user.LockoutEnabled);
                    cmd.Parameters.AddWithValue("@EMailConfirmed", user.EmailConfirmed);

                    if (user.PhoneNumber == null)
                    {
                        cmd.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    }

                    cmd.Parameters.AddWithValue("@PhoneNUmberConfirmed", user.PhoneNumberConfirmed);
                    cmd.Parameters.AddWithValue("@AccessFailedCOunt", user.AccessFailedCount);

                    if (user.PasswordHash == null)
                    {
                        cmd.Parameters.AddWithValue("@PasswordHash", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    }

                    if (user.SecurityStamp == null)
                    {
                        cmd.Parameters.AddWithValue("@SecurityStamp", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SecurityStamp", user.SecurityStamp);
                    }

                    if (user.Email == null)
                    {
                        cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                    }

                    cmd.Parameters.AddWithValue("@TwoFactorEnabled", user.TwoFactorEnabled);

                    dbConnection.Open();

                    cmd.ExecuteNonQuery();

                    user.Id = param.Value.ToString();
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
        }

        public void Update(User user)
        {
            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UserUpdate", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@UserId", user.Id);

                    if (user.UserRole == null)
                    {
                        cmd.Parameters.AddWithValue("@UserRole", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@UserRole", user.UserRole);
                    }


                    if (user.LockoutEndDateUtc == null)
                    {
                        cmd.Parameters.AddWithValue("@LockoutEndDateUtc", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LockoutEndDateUtc", user.LockoutEndDateUtc);
                    }

                    if (user.FirstName == null)
                    {
                        cmd.Parameters.AddWithValue("@FirstName", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    }

                    if (user.LastName == null)
                    {
                        cmd.Parameters.AddWithValue("@LastName", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    }

                    cmd.Parameters.AddWithValue("@LockoutEnabled", user.LockoutEnabled);
                    cmd.Parameters.AddWithValue("@EMailConfirmed", user.EmailConfirmed);

                    if (user.PhoneNumber == null)
                    {
                        cmd.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    }

                    cmd.Parameters.AddWithValue("@PhoneNUmberConfirmed", user.PhoneNumberConfirmed);
                    cmd.Parameters.AddWithValue("@AccessFailedCOunt", user.AccessFailedCount);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

                    if (user.SecurityStamp == null)
                    {
                        cmd.Parameters.AddWithValue("@SecurityStamp", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SecurityStamp", user.SecurityStamp);
                    }

                    if (user.Email == null)
                    {
                        cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                    }

                    cmd.Parameters.AddWithValue("@TwoFactorEnabled", user.TwoFactorEnabled);

                    dbConnection.Open();

                    cmd.ExecuteNonQuery();
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
        }
    }
}
