using LiteCommerce.Admin.Helpers;
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
    public class EmployeeController : Controller
    {
        // GET: Employee
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Quản lý nhân viên";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung thông tin nhân viên";
            Employee model = new Employee()
            {
                EmployeeID = 0
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
            ViewBag.Title = "Chỉnh sửa thông tin nhân viên";
            var model = DataService.GetEmployee(id);
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
                if (DataService.DeleteEmployee(id) == true)
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
                var model = DataService.GetEmployee(id);
                if (model == null)
                    return Content("0");
                return View(model);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Employee data)
        {
            try
            {
                //return Json(data);
                if (string.IsNullOrWhiteSpace(data.LastName))
                    ModelState.AddModelError("LastName", "Vui lòng nhập họ nhân viên");
                if (string.IsNullOrWhiteSpace(data.FirstName))
                    ModelState.AddModelError("FirstName", "Bạn chưa nhập tên nhân viên");
                if (string.IsNullOrWhiteSpace(data.Photo))
                    data.Photo = "";
                if (string.IsNullOrWhiteSpace(data.Notes))
                    data.Notes = "";
                if (string.IsNullOrWhiteSpace(data.Email))
                    data.Email = "";
                if (string.IsNullOrWhiteSpace(data.Password))
                    data.Password = "";
                if (!ModelState.IsValid)
                {
                    if (data.EmployeeID == 0)
                        ViewBag.Title = "Bổ sung thông tin nhân viên";
                    else ViewBag.Title = "Chỉnh sửa thông tin nhân viên";
                    return View("Edit", data);
                }
                int a = 1;
                if (data.EmployeeID == 0)
                {
                    if (DataService.AddEmployee(data) != 0)
                        a = 1;
                    else a = 0;

                }
                else
                {
                    if (DataService.UpdateEmployee(data) == true)
                        a = 1;
                    else a = 0;
                }
                return Content(Convert.ToString(a));
            }
            catch
            {
                return Content("Oops! Trang này hình như không tồn tại.)");
            }
        }
        public ActionResult List(int page=1 ,string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 5;
            var listOfEmployees = DataService.ListEmployees(page, pageSize, searchValue, out rowCount);
            var model = new Models.EmployeePaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listOfEmployees
            };
            return View(model);
        }
    }
}