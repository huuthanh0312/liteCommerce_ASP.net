using System.Data.SqlClient;

namespace LiteCommerce.DataLayers.SQLServer
{
    public abstract class _BaseDAL
    {
        /// <summary>
        /// Chuỗi tham số kết nối đến cơ sở dữ liệu
        /// </summary>
        protected string connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public _BaseDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Tạo và mở kết nối cơ sở dữ liệu
        /// </summary>
        /// <returns></returns>
        protected SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = this.connectionString;
            connection.Open();
            return connection;
        }
    }
}