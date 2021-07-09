using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// lớp biểu diễn lớp dữ liệu đơn hàng
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public int CustomerID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AcceptTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ShipperID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ShippedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime FinishedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }
    }
    /// <summary>
    /// Đơn hàng với các thông tin bổ sung
    /// </summary>
    public class OrderEX : Order
    {
        /// <summary>
        /// Danh sách Chi tiết đơn hàng 
        /// </summary>
        public List<OrderDetail> OrderDetails { get; set; }
    }

}
