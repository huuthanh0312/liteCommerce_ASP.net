using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định Nghĩa các phép xử lý liên quan đến hàng hoá
    /// </summary>
    public interface IProductDAL
    {
        /// <summary>
        /// Lấy danh sách các mặt hàng 
        /// </summary>
        /// <param name="Page">Trang</param>
        /// <param name="PageSize">Kích thước trang</param>
        /// <param name="CategoryId">Mã loại hàng</param>
        /// <param name="suplierId">Nhà cung cấp</param>
        /// <param name="searchValue">Tên của mặt hàng cần tìm kiếm (chuỗi rỗng nếu không tìm thấy ) </param>
        /// <returns></returns>
        List<Product> List(int Page, int PageSize, int CategoryId, int suplierId, string searchValue);
        /// <summary>
        /// Đếm mặt hàng
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="supplierId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(int categoryId, int supplierId, string searchValue);
        /// <summary>
        /// Lấy thông tin mặt hàng theo mã
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Product Get(int productId);
        /// <summary>
        /// Lấy thông tin mặt hàng và các thông tin liên quan đến mã hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ProductEX GetEX(int productId);
        /// <summary>
        /// Bổ sung 1 mặt hàng mới( hàm trả về  mã hàng được bổ sung nếu thành công , trả về 0 nếu không được bổ sung được   
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Product data);
        /// <summary>
        /// Cập nhật thông tin mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Product data);
        /// <summary>
        /// Xoá 1 mặt hàng ( khi xoá mặt hàng đồng thời xoá các thuộc tính và thư viện ảnh của mặt hàng)
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        bool Delete(int productid);
        /// <summary>
        /// Lấy danh sách ác thuộc tính hiện có của 1 Product ( sắp xếp  theo DisplayOrder)
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ProductAttribute> ListAttributes(int productId);
        /// <summary>
        /// Lấy thông tin  của 1 thuộc tính
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        ProductAttribute GetAttribute(long attributeId);
        /// <summary>
        /// Bổ sung thuộc tính cho mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddAttribute(ProductAttribute data);
        /// <summary>
        /// Cập nhật thuộc tính
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateAttribute(ProductAttribute data);
        /// <summary>
        /// Xoá thuộc tính
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        bool DeleteAttribute(long attributeId);
        /// <summary>
        /// Lấy danh sách 1 hình ảnh
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ProductGallery> ListGalleries(int productId);
        /// <summary>
        /// Lấy thông tin 1 hình ảnh
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        ProductGallery GetGallery(long galleryId);
        /// <summary>
        /// Bổ sung ảnh cho mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddGallery(ProductGallery data);
        /// <summary>
        /// Cập nhật hình ảnh cho mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateGallery(ProductGallery data);
        /// <summary>
        /// Xoá 1 ảnh của ặt hàng
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        bool DeleteGallery(long galleryId);
    }
}
