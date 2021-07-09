using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý liên quan đến đơn hàng
    /// </summary>
    public interface IOrderDAL
    {
        /// <summary>
        /// Lấy danh sách các đơn hàng (phân trang, tìm kiếm, lọc dữ liệu)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="status"></param>
        /// <param name="orderToDay"></param>
        /// <param name="orderFromDay"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        List<Order> List(int page, int pageSize, int status, string orderToDay, string orderFromDay, string searchValue);
        /// <summary>
        /// Đếm các đơn hàng
        /// </summary>
        /// <param name="status"></param>
        /// <param name="orderToDay"></param>
        /// <param name="orderFromDay"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(int status, string orderToDay, string orderFromDay, string searchValue);
        /// <summary>
        /// Lấy thông tin đơn hàng theo mã
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Order Get(int orderId);
        /// <summary>
        /// Lấy thông tin đơn hàng và thông tin liên quan theo mã đơn hàng
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderEX GetEX(int orderId);
        /// <summary>
        /// Bổ sung 1 đơn hàng mới (hàm trả về mã đơn hàng được bổ sung nếu thành công, trả về 0  nếu không bổ sung được)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Order data);
        /// <summary>
        /// Cập nhật thông tin đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Order data);
        /// <summary>
        /// Xóa 1 đơn hàng (khi xóa đơn hàng thì đồng thời cũng xóa các chi tiết đơn hàng)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        bool Delete(int orderId);
        /// <summary>
        /// Lấy danh sách các chi tiết đơn hàng của 1 Order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        List<OrderDetail> ListOrderDetails(int orderId);
        /// <summary>
        /// Lấy thông tin chi tiết của một OrderDetail
        /// </summary>
        /// <param name="orderDetailId"></param>
        /// <returns></returns>
        OrderDetail GetOrderDetail(int orderDetailId);
        /// <summary>
        /// Bổ sung chi tiết đơn hàng cho đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int AddOrderDetail(OrderDetail data);
        /// <summary>
        /// Cập nhật chi tiết đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateOrderDetail(OrderDetail data);
        /// <summary>
        /// Xóa chi tiết đơn hàng
        /// </summary>
        /// <param name="orderDetailId"></param>
        /// <returns></returns>
        bool DeleteOrderDetail(int orderDetailId);
        /// <summary>
        /// Danh sách trạng thái đơn hàng
        /// </summary>
        /// <returns></returns>
        List<OrderStatus> ListOrderStatuses();
    }
}
