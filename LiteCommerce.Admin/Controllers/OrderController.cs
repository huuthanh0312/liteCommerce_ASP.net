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
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="searchValue"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult List(int status = 0, string orderToDay = "", string orderFromDay = "", string searchValue = "", int page = 1)
        {
            try
            {
                int rowCount = 0;
                int pageSize = 10;
                var listOfOrder = SaleService.List(page, pageSize, status, orderToDay, orderFromDay, searchValue, out rowCount);
                var model = new Models.OrderPaginationQueryResult()
                {
                    Page = page,
                    PageSize = pageSize,
                    RowCount = rowCount,
                    Data = listOfOrder
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            Order model = new Order()
            {
                OrderID = 0,
                Status = 1,
                AcceptTime = new DateTime(2000, 1, 1),
                ShipperID = 1,
                ShippedTime = new DateTime(2000, 1, 1),
                FinishedTime = new DateTime(2000, 1, 1)
            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var model = SaleService.GetEX(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);

        }

        public ActionResult Save(int id, Order data)
        {

            try
            {

                if (data.OrderID == 0)
                    id = SaleService.Add(data);
                else SaleService.Update(data);
                return RedirectToAction("Edit", new { id = id });
            }
            catch
            {
                return Content("Oops! trang này hình như không tồn tại :)");
            }
        }

        public ActionResult EditOrderdetail(int id)
        {
            ViewBag.Title = "Thay đổi Chi tiết hóa đơn";
            OrderDetail model = SaleService.GetOrderDetail(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }

        public ActionResult AddOrderdetail(int id)
        {
            ViewBag.Title = "Thêm Chi tiết hóa đơn";
            OrderDetail model = new OrderDetail()
            {
                OrderDetailID = 0,
                OrderID = id
            };
            return View("EditOrderdetail", model);
        }
        public ActionResult SaveOrderDetail(int id, OrderDetail data)
        {
            try
            {
                if (data.OrderDetailID == 0)
                    SaleService.AddOrderDetail(data);
                else SaleService.UpdateOrderDetail(data);
                return RedirectToAction("Edit", new { id = id });
            }
            catch
            {
                return Content("Oops! trang này hình như không tồn tại :)");
            }
        }
    }
}