using LiteCommerce.Admin.Helpers;
using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiteCommerce.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login(string loginName = "", string password = "")
        {
            ViewBag.LoginName = loginName;
            if (Request.HttpMethod == "POST")
            {
                var account = AccountService.Authorize(loginName, CryptHelper.Md5(password));
                if (account == null)
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập sai");
                    return View();
                }
                FormsAuthentication.SetAuthCookie(CookieHelper.AccountToCookieString(account), false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        [Authorize]
        public ActionResult Profile(string accountid="" , string oldpassword="", string newpassword="" )
        {
            if (Request.HttpMethod == "POST")
            {
                var user = CookieHelper.CookieStringToAccount(User.Identity.Name);
                var account = AccountService.Authorize(user.Email, CryptHelper.Md5(oldpassword));
                if (account == null)
                {
                    ModelState.AddModelError("oldpass", "Mật Khẩu không đúng. Vui lòng thử lại");
                    return View();
                }
                if(string.IsNullOrWhiteSpace(newpassword))
                {
                    ModelState.AddModelError("newpass", "Mật khẩu không chứa khoảng trắng");
                    return View();
                }
                var change = AccountService.ChangePassword(account.UserName, CryptHelper.Md5(oldpassword), CryptHelper.Md5(newpassword));
                if(change == false)
                {
                    ModelState.AddModelError("newpass", "Thay đổi mật khẩu thất bại");
                    return View();
                }
                else
                {
                    return RedirectToAction("Profile", "Account");
                }
                
            }
            else
            {
                return View();
            }
        }
    }
}