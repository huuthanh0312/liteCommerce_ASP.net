using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai báo các phép xử lý dữ liệu liên quan đén quốc gia
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// Lấy danh sách tất cả các quốc gia
        /// </summary>
        /// <returns></returns>
        List<Country> List();
    }
}
