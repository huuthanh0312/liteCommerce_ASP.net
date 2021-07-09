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
    public class ShipperController : Controller
    {
        // GET: Shipper
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Quản lý nhà vận chuyển";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung thông tin nhà vận chuyển";
            Shipper model = new Shipper()
            {
                ShipperID = 0
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
            ViewBag.Title = "Thay đổi thông tin nhà vận chuyển";
            var model = DataService.GetShipper(id);
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
                if (DataService.DeleteShipper(id) == true)
                {
                    return Content("1");
                }
                else
                {
                    return Content("0");
                }
            }
            else
            {
                var model = DataService.GetShipper(id);
                if (model == null)
                    return Content("0");
                return View(model);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Shipper data)
        {
            try
            {
                //return Json(data);
                if (string.IsNullOrWhiteSpace(data.ShipperName))
                    ModelState.AddModelError("ShipperName", "Vui lòng nhập tên nhà vận chuyển");

                if (string.IsNullOrWhiteSpace(data.Phone))
                    data.Phone = "";

                if (!ModelState.IsValid)
                {
                    if (data.ShipperID == 0)
                        ViewBag.Title = "Bổ sung thông tin nhà vận chuyển";
                    else ViewBag.Title = "Thay đổi thông tin nhà vận chuyển";
                    return View("Edit", data);
                }
                int a = 1;
                if (data.ShipperID == 0)
                {
                    if (DataService.AddShipper(data) != 0)
                        a = 1;
                    else a = 0;

                }
                else
                {
                    if (DataService.UpdateShipper(data) == true)
                        a = 1;
                    else a = 0;
                }
                return Content(Convert.ToString(a));
            }
            catch
            {
                return Content("Oops! trang này hình như không tồn tại .");
            }
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {
            try
            {
                int rowCount = 0;
                int pageSize = 10;
                var listOfShippers = DataService.ListShippers(page, pageSize, searchValue, out rowCount);
                var model = new Models.ShipperPaginationQueryResult()
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    RowCount = rowCount,
                    Data = listOfShippers
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