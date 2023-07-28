using CarDealership.Data.Interfaces;
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
    public class SpecialsRepositoryADO : ISpecialsRepository
    {
        public void Delete(int SpecialId)
        {
            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SpecialDelete", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SpecialId", SpecialId);

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

        public IEnumerable<Specials> GetAll()
        {
            List<Specials> specials = new List<Specials>();

            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SelectAllSpecials", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Specials special = new Specials();

                            special.SpecialId = (int)dr["SpecialsId"];
                            special.SpecialDetails = dr["SpecialDetails"].ToString();
                            special.Title = dr["Title"].ToString();

                            specials.Add(special);
                        }
                    }
                    return specials;
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
                return specials;
            }
        }

        public void Insert(Specials special)
        {
            using (var dbConnection = new SqlConnection(Settings.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SpecialInsert", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param = new SqlParameter("@SpecialId", SqlDbType.Int);
                    param.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(param);

                    cmd.Parameters.AddWithValue("@SpecialDetails", special.SpecialDetails);
                    cmd.Parameters.AddWithValue("Title", special.Title);

                    dbConnection.Open();

                    cmd.ExecuteNonQuery();

                    special.SpecialId = (int)param.Value;
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
