using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin
{
    /// <summary>
    /// cung cấp các hàm tiện ích liên quan  đến SelectListItem
    /// </summary>
    public static class SelectListHelper
    {
        /// <summary>
        /// Danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DataService.ListCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }
            return list;
        }
        /// <summary>
        /// Lấy danh sách thành phố
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Cities()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DataService.ListCities())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CityName,
                    Text = item.CityName
                });
            }
            return list;
        }
        /// <summary>
        /// Check mặt hàng trong edit
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> ParentCategoties()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Không thuộc mặt hàng nào"
            });
            foreach (var item in DataService.ListParentCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.CategoryID),
                    Text = item.CategoryName
                });
            }
            return list;
        }
        /// <summary>
        /// Lấy danh sách nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> ParentSuppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Không thuộc nhà cung cấp nào"
            });
            foreach (var item in DataService.ListParentSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.SupplierID),
                    Text = item.SupplierName
                });
            }
            return list;
        }
        /// <summary>
        /// Danh sách nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            int page = 1;
            int pageSize = 1000;
            string searchvalue = "";
            int rowcount = 0;
            foreach (var item in DataService.ListSuppliers(page, pageSize, searchvalue, out rowcount))
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.SupplierID),
                    Text = item.SupplierName
                });
            }
            return list;
        }
        /// <summary>
        /// Danh sách khách hàng
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Customers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            int page = 1;
            int pageSize = 1000;
            string searchvalue = "";
            int rowcount = 0;

            foreach (var item in DataService.ListCustomers(page, pageSize, searchvalue, out rowcount))
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.CustomerID),
                    Text = item.CustomerName
                });
            }
            return list;
        }
        /// <summary>
        /// Danh sách trạng thái đơn hàng
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> ListOrderStatus()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var item in SaleService.ListOrderStatus())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.Status),
                    Text = item.Description
                });
            }
            return list;
        }
        /// <summary>
        /// Danh sách nhà vận chuyển
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Shippers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            int page = 1;
            int pageSize = 1000;
            string searchvalue = "";
            int rowcount = 0;
            foreach (var item in DataService.ListShippers(page, pageSize, searchvalue, out rowcount))
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.ShipperID),
                    Text = item.ShipperName
                });
            }
            return list;
        }
        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Employees()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            int page = 1;
            int pageSize = 1000;
            string searchvalue = "";
            int rowcount = 0;
            foreach (var item in DataService.ListEmployees(page, pageSize, searchvalue, out rowcount))
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.EmployeeID),
                    Text = item.LastName + " " + item.FirstName
                });
            }
            return list;
        }
        /// <summary>
        /// Danh sách hàng hóa
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Products()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            int page = 1;
            int pageSize = 1000;
            string searchvalue = "";
            int supplier = 0;
            int category = 0;
            int rowcount = 0;
            foreach (var item in ProductService.List(page, pageSize, category, supplier, searchvalue, out rowcount))
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.ProductID),
                    Text = item.ProductName
                });
            }
            return list;
        }
    }
}