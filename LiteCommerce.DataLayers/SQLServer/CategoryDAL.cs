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
    public class CategoryDAL : _BaseDAL, ICategoryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CategoryDAL(string connectionString) : base(connectionString)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Category data)
        {
            int categoryID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Categories ( CategoryName, Description, ParentCategoryID)
                                    VALUES (@CategoryName, @Description, @ParentCategoryID)
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@ParentCategoryID", data.ParentCategoryId);

                categoryID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }

            return categoryID;
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
                                    FROM Categories
                                    WHERE @searchValue = ''
                                    OR ( CategoryName LIKE @searchValue
                                         OR Description LIKE @searchValue)";
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
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public bool Delete(int categoryID)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Categories WHERE CategoryID = @categoryID
                                    AND NOT EXISTS ( SELECT * FROM Products WHERE CategoryID = Categories.CategoryID)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Category Get(int categoryID)
        {
            Category data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Categories WHERE CategoryID = @categoryID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Category()
                    {
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        CategoryName = Convert.ToString(dbReader["CategoryName"]),
                        Description = Convert.ToString(dbReader["Description"]),

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
        public List<Category> List(int page, int pageSize, string searchValue)
        {
            List<Category> data = new List<Category>();
            using (SqlConnection cn = GetConnection())
            {
                if (searchValue != "")
                    searchValue = "%" + searchValue + "%";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM
                                        (
                                            SELECT *, ROW_NUMBER() OVER(ORDER BY CategoryName) AS RowNumber
                                            FROM Categories
                                            WHERE (@searchValue='')
	                                            OR(
		                                            CategoryName LIKE @searchValue
		                                            Or Description LIKE @searchValue
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
                        data.Add(new Category()
                        {

                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            Description = Convert.ToString(dbReader["Description"])

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
        public bool Update(Category data)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Categories
                                    SET CategoryName = @CategoryName,
                                        Description = @Description,
                                        ParentCategoryID = @ParentCategoryID
                                    WHERE CategoryID = @CategoryID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@ParentCategoryId", data.ParentCategoryId);

                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }
        /// <summary>
        /// Lấy danh sách tất cả id và tên loại hàng
        /// </summary>
        /// <returns></returns>
        public List<Category> ListParentCategories()
        {
            List<Category> data = new List<Category>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT CategoryID, CategoryName FROM Categories";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {

                        data.Add(new Category()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"])
                        });
                    }
                }
                cn.Close();
            }
            return data;
        }
    }
}
