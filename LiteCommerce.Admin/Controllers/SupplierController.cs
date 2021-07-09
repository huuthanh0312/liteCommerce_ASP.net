using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiteCommerce.Admin.Models;
using LiteCommerce.DomainModels;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        // GET: Suppliers
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Quản lý nhà cung cấp";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Thay đổi thông tin nhà cung cấp";
            var model = DataService.GetSupplier(id);
            if (model == null)
                return Content("0");
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung thông tin nhà cung cấp";
            Supplier model = new Supplier()
            {
                SupplierID = 0
            };
            return View("Edit", model);
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
                //Xóa Supplier có mã là id
                if(DataService.DeleteSupplier(id)== true)
                {
                    return Content("1");
                } else
                {
                    return Content("0");
                }  
            }
            else
            {
                //Lấy thông tin của Supplier cần xóa
                var model = DataService.GetSupplier(id);
                if (model == null)
                    return Content("0");
                return View(model);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Supplier data)
        {
            try
            {
                //return Json(data);
                if (string.IsNullOrWhiteSpace(data.SupplierName))
                    ModelState.AddModelError("SupplierName", "Vui lòng nhập tên nhà cung cấp");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Bạn chưa nhập tên liên hệ của nhà cung cấp");
                if (string.IsNullOrWhiteSpace(data.Address))
                    data.Address = "";
                if (string.IsNullOrWhiteSpace(data.Country))
                    data.City = "";
                if (string.IsNullOrWhiteSpace(data.PostalCode))
                    data.PostalCode = "";
                if (string.IsNullOrWhiteSpace(data.Phone))
                    data.PostalCode = "";

                if (!ModelState.IsValid)
                {
                    if (data.SupplierID == 0)
                        ViewBag.Title = "Bổ sung nhà cung cấp mới";
                    else ViewBag.Title = "Thay đổi thông tin nhà cung cấp";
                    return View("Edit", data);
                }
                int a = 1;
                if (data.SupplierID == 0)
                {
                    if (DataService.AddSupplier(data) != 0)
                        a = 1;
                    else a = 0;
                    
                }
                else
                {
                    if (DataService.UpdateSupplier(data) == true)
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
            try
            {
                int rowCount = 0;
                int pageSize = 5;
                var listOfSuppliers = DataService.ListSuppliers(page, pageSize, searchValue, out rowCount);
                var model = new Models.SupplierPaginationQueryResult()
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    RowCount = rowCount,
                    Data = listOfSuppliers
                };

                return View(model);

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }

}
