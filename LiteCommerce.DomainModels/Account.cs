using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Tài khoản của nguời sử dụng
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Tên / ID của tài khoản
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// BirthDate
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Ảnh đại diện của tài khoản
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// Thông tin bản thân
        /// </summary>
        public string Notes { get; set; }
    }
}
