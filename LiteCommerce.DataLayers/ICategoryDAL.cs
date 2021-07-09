using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến loại hàng hóa
    /// </summary>
    public interface ICategoryDAL
    {
        /// <summary>
        /// Bổ sung một loại hàng hóa. Hàm trả về mã của loại hàng hóa nếu bổ sung thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của loại hàng hóa cần bổ sung</param>
        /// <returns></returns>
        int Add(Category data);
        /// <summary>
        /// Lấy danh sách loại hàng hóa (tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo CategoryName, Description (chuỗi rỗng nếu không có yêu cầu tìm kiếm)</param>
        /// <returns></returns>
        List<Category> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số lượng loại hàng hóa thỏa điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo CategoryName, Description (chuỗi rỗng nếu không có yêu cầu tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của một loại hàng hóa theo mã. Trong trường hợp loại hàng hóa không tồn tại, hàm trả về giá trị null.
        /// </summary>
        /// <param name="categoryID">Mã của loại hàng hóa cần lấy thông tin</param>
        /// <returns></returns>
        Category Get(int categoryID);
        /// <summary>
        /// Cập nhật thông tin của loại hàng hóa. Hàm trả về boolean cho biết việc cập nhật có thành công hay không
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Category data);
        /// <summary>
        /// Xóa một loại hàng hóa dựa vào mã. Hàm trả về giá trị bool cho biết việc xóa có thực hiện được hay không 
        /// (Lưu ý: không được xóa loại hàng hóa nếu đang có mặt hàng hoặc loại hàng khóa khác tham chiếu đến loại hàng hóa)
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        bool Delete(int categoryID);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Category> ListParentCategories();
    }
}
