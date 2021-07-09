using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// Cung cấp các chức năng tác nghiệp liên quan đến bán hàng
    /// </summary>
    public static class SaleService
    {
        private static IOrderDAL OrderDB;
        /// <summary>
        /// Khởi tạo tính năng tác nghiệp (hàm này phải được gọi nếu muốn sử dụng các tính năng của lớp)
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectionString"></param>
        public static void Init(DatabaseTypes dbType, string connectionString)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    OrderDB = new DataLayers.SQLServer.OrderDAL(connectionString);
                    break;
                default:
                    throw new Exception("Database type is not supported");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="status"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Order> List(int page, int pageSize, int status, string orderToDay, string orderFromDay, string searchValue, out int rowCount)
        {
            rowCount = OrderDB.Count(status, orderToDay, orderFromDay, searchValue);
            return OrderDB.List(page, pageSize, status, orderToDay, orderFromDay, searchValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 
        public static int Add(Order data)
        {
            return OrderDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddOrderDetail(OrderDetail data)
        {
            return OrderDB.AddOrderDetail(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<OrderStatus> ListOrderStatus()
        {
            return OrderDB.ListOrderStatuses();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static Order Get(int orderId)
        {
            return OrderDB.Get(orderId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static OrderEX GetEX(int orderId)
        {
            return OrderDB.GetEX(orderId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetailId"></param>
        /// <returns></returns>
        public static OrderDetail GetOrderDetail(int orderDetailId)
        {
            return OrderDB.GetOrderDetail(orderDetailId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static List<OrderDetail> ListOrderDetails(int orderId)
        {
            return OrderDB.ListOrderDetails(orderId);
        }

        public static bool Update(Order data)
        {
            return OrderDB.Update(data);
        }
        public static bool UpdateOrderDetail(OrderDetail data)
        {
            return OrderDB.UpdateOrderDetail(data);
        }
    }

}
