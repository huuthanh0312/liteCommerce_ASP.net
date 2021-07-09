using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
using System.Data.SqlClient;
using System;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeDAL : _BaseDAL, IEmployeeDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Employee data)
        {
            int ShipperID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Employees( LastName, FirstName, BirthDate, Photo, Notes, Email, Password)
                                    VALUES (@LastName, @FirstName, @BirthDate, @Photo, @Notes, @Email, @Password)
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                ShipperID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return ShipperID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            int result = 0;
            if (searchValue != "") searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT COUNT(*) 
                                    FROM Employees
                                    WHERE (@searchValue = '')
                                      OR(
		                                    LastName LIKE @searchValue
		                                    OR FirstName LIKE @searchValue
		                                    OR BirthDate LIKE @searchValue
		                                    OR Notes LIKE @searchValue
                                            OR Email LIKE @searchValue
	                                        )";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                result = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public bool Delete(int employeeID)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Employees WHERE EmployeeID = @employeeID
                                   AND NOT EXISTS ( SELECT * FROM Orders WHERE EmployeeID = @employeeID)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Employee Get(int employeeID)
        {
            Employee data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * 
                                    FROM Employees WHERE EmployeeID = @employeeID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Employee()
                    {
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        LastName = Convert.ToString(dbReader["LastName"]),
                        FirstName = Convert.ToString(dbReader["FirstName"]),
                        BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        Notes = Convert.ToString(dbReader["Notes"]),
                        Email = Convert.ToString(dbReader["Email"]),
                        Password = Convert.ToString(dbReader["Password"])
                    };
                }

                cn.Close();

            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Employee> List(int page, int pageSize, string searchValue)
        {
            List<Employee> data = new List<Employee>();
            using (SqlConnection cn = GetConnection())
            {
                if (searchValue != "")
                    searchValue = "%" + searchValue + "%";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM
                                        (
                                            SELECT *, ROW_NUMBER() OVER(ORDER BY LastName) AS RowNumber
                                            FROM Employees
                                            WHERE (@searchValue='')
	                                            OR(
		                                            LastName LIKE @searchValue
		                                            Or FirstName LIKE @searchValue
		                                            Or BirthDate LIKE @searchValue
		                                            Or Notes LIKE @searchValue
                                                    Or Email LIKE @searchValue
	                                                )
                                          ) AS s
                                     WHERE s.RowNumber BETWEEN (@page - 1)*@pageSize + 1 AND @page*@pageSize";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Employee()
                        {
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            LastName = Convert.ToString(dbReader["LastName"]),
                            FirstName = Convert.ToString(dbReader["FirstName"]),
                            BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Notes = Convert.ToString(dbReader["Notes"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"])
                        });
                    }
                }
                cn.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Employee data)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Employees 
                                    SET LastName = @LastName, 
                                        FirstName = @FirstName, 
                                        BirthDate = @BirthDate, 
                                        Photo = @Photo, 
                                        Notes = @Notes,
                                        Email = @Email,
                                        Password = @Password
                                   WHERE EmployeeID = @EmployeeID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }
    }
}
