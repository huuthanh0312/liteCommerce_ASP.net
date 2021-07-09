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
    public class OrderDAL : _BaseDAL, IOrderDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public OrderDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Order data)
        {
            int OrderID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Orders( CustomerID, OrderTime, EmployeeID, AcceptTime, ShipperID, ShippedTime, FinishedTime, Status)
                                    VALUES (@CustomerID, @OrderTime, @EmployeeID, @AcceptTime, @ShipperID, @ShippedTime, @FinishedTime, @Status)
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@OrderTime", data.OrderTime);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@AcceptTime", data.AcceptTime);
                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                cmd.Parameters.AddWithValue("@ShippedTime", data.ShippedTime);
                cmd.Parameters.AddWithValue("@FinishedTime", data.FinishedTime);
                cmd.Parameters.AddWithValue("@Status", data.Status);
                OrderID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return OrderID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int AddOrderDetail(OrderDetail data)
        {
            int OrderDetailID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO OrderDetails( OrderID, ProductID, Quantity, SalePrice)
                                    VALUES (@OrderID, @ProductID, @Quantity, @SalePrice)
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@OrderID", data.OrderID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", data.Quantity);
                cmd.Parameters.AddWithValue("@SalePrice", data.SalePrice);
                OrderDetailID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return OrderDetailID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(int status, string orderToDay, string orderFromDay, string searchValue)
        {
            int result = 0;
            if (searchValue != "") searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT COUNT(*) FROM (
                                        SELECT  o.*,c.CustomerName
                                        FROM    Orders as o JOIN Customers as c ON c.CustomerID = o.CustomerID
                                        WHERE   (@searchValue = '' OR CustomerName LIKE @searchValue)
                                            AND  (@status = 0 OR Status = @status)
                                            AND (OrderTime BETWEEN @orderToDay AND @orderFromDay)
                                    ) AS s";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@orderToDay", orderToDay);
                cmd.Parameters.AddWithValue("@orderFromDay", orderFromDay);
                result = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool Delete(int orderId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetailId"></param>
        /// <returns></returns>
        public bool DeleteOrderDetail(int orderDetailId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Order Get(int orderId)
        {
            Order data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT o.*, c.CustomerName
                                    FROM Orders as o JOIN Customers as c ON c.CustomerID = o.CustomerID 
                                    WHERE OrderID = @orderId";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@orderId", orderId);
                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Order()
                    {
                        OrderID = Convert.ToInt32(dbReader["OrderID"]),
                        CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                        OrderTime = Convert.ToDateTime(dbReader["OrderTime"]),
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        AcceptTime = Convert.ToDateTime(dbReader["AcceptTime"]),
                        ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                        ShippedTime = Convert.ToDateTime(dbReader["ShippedTime"]),
                        FinishedTime = Convert.ToDateTime(dbReader["FinishedTime"]),
                        Status = Convert.ToInt32(dbReader["Status"]),
                        CustomerName = Convert.ToString(dbReader["CustomerName"])
                    };
                }

                cn.Close();

            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderEX GetEX(int orderId)
        {
            Order order = this.Get(orderId);
            List<OrderDetail> list = this.ListOrderDetails(orderId);
            OrderEX data = new OrderEX()
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID,
                OrderTime = order.OrderTime,
                EmployeeID = order.EmployeeID,
                AcceptTime = order.AcceptTime,
                ShipperID = order.ShipperID,
                ShippedTime = order.ShippedTime,
                FinishedTime = order.FinishedTime,
                Status = order.Status,
                CustomerName = order.CustomerName,
                OrderDetails = list
            };

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetailId"></param>
        /// <returns></returns>
        public OrderDetail GetOrderDetail(int orderDetailId)
        {
            OrderDetail data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT *
                                    FROM OrderDetails
                                    WHERE OrderDetailID = @orderDetailId";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@orderDetailId", orderDetailId);
                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new OrderDetail()
                    {
                        OrderDetailID = Convert.ToInt32(dbReader["OrderDetailID"]),
                        OrderID = Convert.ToInt32(dbReader["OrderID"]),
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        Quantity = Convert.ToInt32(dbReader["Quantity"]),
                        SalePrice = Convert.ToDecimal(dbReader["SalePrice"])
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
        /// <param name="status"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>

        public List<Order> List(int page, int pageSize, int status, string orderToDay, string orderFromDay, string searchValue)
        {
            List<Order> data = new List<Order>();
            if (searchValue != "") searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT  *
                                    FROM
                                    (
                                        SELECT  o.*, ROW_NUMBER() OVER(ORDER BY OrderTime) AS RowNumber,c.CustomerName
                                        FROM    Orders as o JOIN Customers as c ON c.CustomerID = o.CustomerID
                                        WHERE   (@searchValue = '' OR CustomerName LIKE @searchValue)
                                            AND  (@status = 0 OR Status = @status)
                                            AND (@orderToDay='' AND @orderFromDay='' OR OrderTime BETWEEN @orderToDay AND @orderFromDay)
                                    ) AS s
                                    WHERE s.RowNumber BETWEEN (@page - 1)*@pageSize + 1 AND @page*@pageSize
                                    Order By OrderID ASC";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@orderToDay", orderToDay);
                cmd.Parameters.AddWithValue("@orderFromDay", orderFromDay);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Order()
                        {
                            OrderID = Convert.ToInt32(dbReader["OrderId"]),
                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            CustomerName = Convert.ToString(dbReader["CustomerName"]),
                            OrderTime = Convert.ToDateTime(dbReader["OrderTime"]),
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            AcceptTime = Convert.ToDateTime(dbReader["AcceptTime"]),
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            ShippedTime = Convert.ToDateTime(dbReader["ShippedTime"]),
                            FinishedTime = Convert.ToDateTime(dbReader["FinishedTime"]),
                            Status = Convert.ToInt32(dbReader["Status"])

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
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderDetail> ListOrderDetails(int orderId)
        {
            List<OrderDetail> data = new List<OrderDetail>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * 
                                    FROM OrderDetails
                                    WHERE OrderID = @orderId";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@orderId", orderId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new OrderDetail()
                        {
                            OrderDetailID = Convert.ToInt32(dbReader["OrderDetailID"]),
                            OrderID = Convert.ToInt32(dbReader["OrderID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            Quantity = Convert.ToInt32(dbReader["Quantity"]),
                            SalePrice = Convert.ToDecimal(dbReader["SalePrice"])
                        });
                    }
                }
                cn.Close();
            }

            return data;
        }

        public List<OrderStatus> ListOrderStatuses()
        {
            List<OrderStatus> data = new List<OrderStatus>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * 
                                    FROM OrderStatus
                                    Order By Status ASC";
                cmd.CommandType = System.Data.CommandType.Text;
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new OrderStatus()
                        {
                            Status = Convert.ToInt32(dbReader["Status"]),
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
        public bool Update(Order data)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Orders 
                                    SET CustomerID = @CustomerID, 
                                        OrderTime = @OrderTime,
                                        EmployeeID = @EmployeeID,
                                        AcceptTime = @AcceptTime,
                                        ShipperID = @ShipperID,
                                        ShippedTime = @ShippedTime,
                                        FinishedTime = @FinishedTime,
                                        Status = @Status
                                   WHERE OrderID = @OrderID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@OrderID", data.OrderID);
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@OrderTime", data.OrderTime);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@AcceptTime", data.AcceptTime);
                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                cmd.Parameters.AddWithValue("@ShippedTime", data.ShippedTime);
                cmd.Parameters.AddWithValue("@FinishedTime", data.FinishedTime);
                cmd.Parameters.AddWithValue("@Status", data.Status);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateOrderDetail(OrderDetail data)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE OrderDetails
                                    SET ProductID = @ProductID, 
                                        Quantity = @Quantity,
                                        SalePrice = @SalePrice
                                   WHERE OrderDetailID = @OrderDetailID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@OrderDetailID", data.OrderDetailID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", data.Quantity);
                cmd.Parameters.AddWithValue("@SalePrice", data.SalePrice);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }
    }
}
