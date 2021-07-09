using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        // GET: Customer
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Quản lý khách hàng";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung thông tin khách hàng";
            Customer model = new Customer()
            {
                CustomerID = 0
            };
            return View("Edit", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Chỉnh sửa thông tin khách hàng";
            var model = DataService.GetCustomer(id);
            if (model == null)
                return Content("0");
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "POST")
            {
                if (DataService.DeleteCustomer(id) == true)
                    return Content("1");
                else
                    return Content("0");
            }
            else
            {
                var model = DataService.GetCustomer(id);
                if (model == null)
                    return Content("0");
                return View(model);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Customer data)
        {
            try
            {
                //return Json(data);
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                    ModelState.AddModelError("CustomerName", "Vui lòng nhập tên khách hàng");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Bạn chưa nhập tên giao dịch của khách hàng");
                if (string.IsNullOrWhiteSpace(data.Address))
                    data.Address = "";
                if (string.IsNullOrWhiteSpace(data.Country))
                    data.City = "";
                if (string.IsNullOrWhiteSpace(data.PostalCode))
                    data.PostalCode = "";
                if (string.IsNullOrWhiteSpace(data.Email))
                    data.Email = "";
                if (string.IsNullOrWhiteSpace(data.Password))
                    data.Password = "";

                if (!ModelState.IsValid)
                {
                    if (data.CustomerID == 0)
                        ViewBag.Title = "Bổ sung khách hàng mới";
                    else ViewBag.Title = "Thay đổi thông tin khách hàng";
                    return View("Edit", data);
                }
                int a = 1;
                if (data.CustomerID == 0)
                {
                    if (DataService.AddCustomer(data) != 0)
                        a = 1;
                    else a = 0;
                }
                else
                {
                    if (DataService.UpdateCustomer(data) == true)
                        a = 1;
                    else a = 0;
                }
                return Content(Convert.ToString(a));
            }
            catch
            {
                return Content("Oops! Trang này hình như không tồn tại .)");
            }
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 10;
            var listOfCustomers = DataService.ListCustomers(page, pageSize, searchValue, out rowCount);
            var model = new Models.CustomerPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listOfCustomers
            };
            return View(model);
        }
    }
}