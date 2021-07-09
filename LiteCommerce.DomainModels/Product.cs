using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Mặt hàng
    /// </summary>
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
    }
    /// <summary>
    /// Mặ hàng với các thông tin bổ sung mở rộng
    /// </summary>
    public class ProductEX : Product
    {
        /// <summary>
        /// Danh sách các thuộc tính mặt hàng
        /// </summary>
        public List<ProductAttribute> Attributes { get; set; }
        /// <summary>
        /// Danh sách các hình ảnh của mặt hàng
        /// </summary>
        public List<ProductGallery> Galleries { get; set; }
    }
}
