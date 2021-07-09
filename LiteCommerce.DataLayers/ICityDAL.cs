using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến thành phố
    /// </summary>
    public interface ICityDAL
    {
        /// <summary>
        /// Lấy danh sách tất cả thành phố
        /// </summary>
        /// <returns></returns>
        List<City> List();
        /// <summary>
        /// Lấy danh sách thành phố thuộc một quốc gia
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        List<City> List(string countryName);
    }
}
