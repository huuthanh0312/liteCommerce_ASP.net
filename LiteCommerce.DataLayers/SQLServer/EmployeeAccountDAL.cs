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
    public class EmployeeAccountDAL : _BaseDAL , IAccountDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeAccountDAL(string connectionString) : base(connectionString)
        { 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account Authorize(string loginName, string password)
        {
            Account data = null;
            using ( SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT EmployeeID , FirstName, LastName, BirthDate, Email , Photo, Notes
                                    FROM Employees
                                    WHERE Email = @loginName AND Password = @password";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@loginName", loginName);
                cmd.Parameters.AddWithValue("@password", password);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {   
                    if (dbReader.Read())
                    {
                        data = new Account()
                        {
                            UserName = dbReader["EmployeeID"].ToString(),
                            FullName = $"{dbReader["FirstName"]} {dbReader["LastName"]}",
                            BirthDate = Convert.ToDateTime(dbReader["BirthDate"].ToString()),
                            Email = dbReader["Email"].ToString(),
                            Photo = dbReader["Photo"].ToString(),
                            Notes = dbReader["Notes"].ToString()
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool ChangePassword(string accountid, string oldpassword, string newpassword)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Employees 
                                    SET Password = @NewPassword
                                   WHERE EmployeeID = @EmployeeID AND Password = @Password";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@EmployeeID", accountid);
                cmd.Parameters.AddWithValue("@Password", oldpassword);
                cmd.Parameters.AddWithValue("@NewPassword", newpassword);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true; ;
        }

        public Account Get(string accountid)
        {
            throw new NotImplementedException();
        }
    }
}
