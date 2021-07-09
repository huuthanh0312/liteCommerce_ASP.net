using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ISupplierDAL
    {
        /// <summary>
        /// bổ sung một nhà cung cấp . hàm sẽ trả về mã của nhà cung cấp nếu  bổ sung thành công
        /// </summary>
        /// <param name="data">một đối tượng lưu thông tin nhà cung cáp cần bổ sung</param>
        /// <returns></returns>
        int Add(Supplier data);
        /// <summary>
        /// láy danh sách nhà cung cấp (Tìm kiếm , phần trang)
        /// </summary>
        /// <param name="page">trang cần lấy dữ liệu</param>
        /// <param name="pageSize">số dòng hiển trị trên mỗi  trang</param>
        /// <param name="seachValue"> giá trị càn tìm theo Supplier Name, Contactname, Address, Phone ( chuỗi rỗng nếu không có tim kiếm)</param>
        /// <returns></returns>
        List<Supplier> List(int page, int pageSize, string seachValue);
        /// <summary>
        /// đếm số lượng nhà cung cấp thỏa điều kiện 
        /// </summary>
        /// <param name="seachValue">giá trị càn tìm theo Supplier Name, Contactname, Address, Phone ( chuỗi rỗng nếu k có tim kiếm)</param>
        /// <returns></returns>
        int Count(string seachValue);
        /// <summary>
        /// lấy thông tin một nhà cung cấp
        /// nhà cung cấp không ton tai trar về giá tri null
        /// </summary>
        /// <param name="SupplierID"> Mã nhà cung cấp cần lấy thông tin</param>
        /// <returns></returns>
        Supplier Get(int SupplierID);
        /// <summary>
        /// cập nhật thông tin hàng trả vè giá trị bool  cho biết việc cập nhật có thành công không
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        bool Update(Supplier data);

        /// <summary>
        /// xoá nhà cung cấp dựa vào mx, hàm trả về giá trị bool cho biết việc có thức hiện được hay không( luu ý :
        /// không được xóa nhà cung cấp nếu đang có một hàng tham chiếu đến nhà cung cấp)
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        bool Delete(int SupplierID);
        /// <summary>
        /// Lấy danh sách nhà cung cấp có sắp xếp
        /// </summary>
        /// <returns></returns>
        List<Supplier> ListParentSuppliers();
    }
}
    
 
