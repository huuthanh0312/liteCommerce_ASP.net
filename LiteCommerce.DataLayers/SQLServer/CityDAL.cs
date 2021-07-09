using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public class CityDAL : _BaseDAL, ICityDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CityDAL(string connectionString) : base(connectionString)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<City> List()
        {
            return List("");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public List<City> List(string countryName)
        {
            List<City> data = new List<City>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Cities " +
                                   "WHERE @CountryName = '' OR CountryName = @CountryName";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@CountryName", countryName);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        //Country country = new Country();
                        //country.CountryName = Convert.ToString(dbReader["CountryName"]);
                        //data.Add(country);

                        //Country country = new Country()
                        //{
                        //    CountryName = Convert.ToString(dbReader["CountryName"])
                        //};
                        //data.Add(country);

                        data.Add(new City()
                        {
                            CityName = Convert.ToString(dbReader["CityName"]),
                            CountryName = Convert.ToString(dbReader["CountryName"])
                        });
                    }
                }
                cn.Close();
            }
            return data;
        }
    }
}
