using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Bổ sung một nhân viên. Hàm trả về mã của nhân viên nếu bổ sung thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của nhân viên cần bổ sung</param>
        /// <returns></returns>
        int Add(Employee data);
        /// <summary>
        /// Lấy danh sách nhân viên (tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo LastName, FirstName, BirthDate, Notes, Email  (chuỗi rỗng nếu không có yêu cầu tìm kiếm)</param>
        /// <returns></returns>
        List<Employee> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số lượng nhân viên thỏa điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm theo LastName, FirstName, BirthDate, Notes, Email (chuỗi rỗng nếu không có yêu cầu tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của một nhân viên theo mã. Trong trường hợp nhân viên không tồn tại, hàm trả về giá trị null.
        /// </summary>
        /// <param name="employeeID">Mã của nhân viên cần lấy thông tin</param>
        /// <returns></returns>
        Employee Get(int employeeID);
        /// <summary>
        /// Cập nhật thông tin của nhân viên. Hàm trả về boolean cho biết việc cập nhật có thành công hay không
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Employee data);
        /// <summary>
        /// Xóa một nhân viên dựa vào mã. Hàm trả về giá trị bool cho biết việc xóa có thực hiện được hay không 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        bool Delete(int employeeID);
    }
}
