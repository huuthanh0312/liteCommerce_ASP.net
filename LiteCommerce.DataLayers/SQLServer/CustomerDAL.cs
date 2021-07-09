using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DataLayers;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class CustomerDAL : _BaseDAL, ICustomerDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CustomerDAL(string connectionString) : base(connectionString)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Customer data)
        {
            int customerID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Customers ( CustomerName, ContactName, Address, City, PostalCode, Country, Email, Password)
                                    VALUES ( @CustomerName, @ContactName, @Address, @City, @PostalCode, @Country, @Email, @Password) 
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);

                customerID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }

            return customerID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT COUNT(*) 
                                    FROM Customers
                                    WHERE (@searchValue = '')
                                      OR(
		                                            CustomerName LIKE @searchValue
		                                            Or ContactName LIKE @searchValue
		                                            Or Address LIKE @searchValue
		                                            Or Email LIKE @searchValue
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
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool Delete(int customerId)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Customers 
                                    WHERE CustomerID = @customerId
                                          AND NOT EXISTS ( SELECT * FROM Orders WHERE Customers.CustomerID = CustomerID)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@customerId", customerId);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customer Get(int customerID)
        {
            Customer data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT * 
                                    FROM Customers WHERE CustomerID = @customerID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@customerID", customerID);
                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Customer()
                    {
                        CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                        CustomerName = Convert.ToString(dbReader["CustomerName"]),
                        ContactName = Convert.ToString(dbReader["ContactName"]),
                        Address = Convert.ToString(dbReader["Address"]),
                        City = Convert.ToString(dbReader["City"]),
                        PostalCode = Convert.ToString(dbReader["PostalCode"]),
                        Country = Convert.ToString(dbReader["Country"]),
                        Email = Convert.ToString(dbReader["Email"]),
                        Password = Convert.ToString(dbReader["Password"])
                    };
                }

                connection.Close();

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
        public List<Customer> List(int page, int pageSize, string searchValue)
        {
            List<Customer> data = new List<Customer>();
            using (SqlConnection cn = GetConnection())
            {
                if (searchValue != "")
                    searchValue = "%" + searchValue + "%";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM
                                        (
                                            SELECT *, ROW_NUMBER() OVER(ORDER BY CustomerName) AS RowNumber
                                            FROM Customers
                                            WHERE (@searchValue='')
	                                            OR(
		                                            CustomerName LIKE @searchValue
		                                            Or ContactName LIKE @searchValue
		                                            Or Address LIKE @searchValue
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
                        data.Add(new Customer()
                        {

                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            CustomerName = Convert.ToString(dbReader["CustomerName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
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
        public bool Update(Customer data)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Customers 
                                    SET CustomerName = @CustomerName, 
                                        ContactName = @ContactName, 
                                        Address = @Address, 
                                        City = @City, 
                                        PostalCode = @PostalCode,
                                        Country = @Country, 
                                        Email = @Email,
                                        Password = @Password
                                   WHERE CustomerID = @CustomerID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
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
