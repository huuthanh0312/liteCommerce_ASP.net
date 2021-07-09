using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IAccountDAL
    {

        /// <summary>
        /// Kiểm tra thông tin đăng nhập của User ( Hàm trả về null nếu thông tin đăng nhập
        /// </summary>
        /// <param name="loginName">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns></returns>
        Account Authorize(string loginName, string password);
        /// <summary>
        /// Đổi mật khẩu 
        /// </summary>
        /// <param name="accountid"></param>
        /// <param name="oldpassword"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        bool ChangePassword(string accountid, string oldpassword, string newpassword);
        /// <summary>
        /// Lấy tài khoản của 1 tài khoản theo ID
        /// </summary>
        /// <param name="accountid"></param>
        /// <returns></returns>
        Account Get(string accountid);
    }
}
