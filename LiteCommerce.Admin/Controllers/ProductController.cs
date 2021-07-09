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
    public class ProductController : Controller
    {
        // GET: Product
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Thông tin hàng hóa";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung thông tin hàng hóa";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Chỉnh sửa thông tin hàng hóa";
            var model = ProductService.GetEX(id);
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
                //Xóa có mã là id
                if (ProductService.Delete(id) == true)
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
                //Lấy thông tin  cần xóa
                var model = ProductService.Get(id);
                if (model == null)
                    return Content("0");
                return View(model);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Product data)
        {
            try
            {
                //return Json(data);
                if (string.IsNullOrWhiteSpace(data.ProductName))
                    ModelState.AddModelError("ProductName", "Vui lòng nhập tên hàng hoá");
                if (string.IsNullOrWhiteSpace(data.Unit))
                    ModelState.AddModelError("Unit", "Bạn chưa nhập đơn vị tính ");
                if (string.IsNullOrWhiteSpace(data.Photo))
                    data.Photo = "";
                if (!ModelState.IsValid)
                {
                    if (data.ProductID == 0)
                        ViewBag.title = "bổ sung thông tin hàng hóa mới";
                    else ViewBag.title = "thay đổi thông tin hàng hóa";
                    return View("edit", data);
                }
                int ID;
                if (data.ProductID == 0)
                {
                    ID = ProductService.Add(data);

                }
                else
                {
                    ProductService.Update(data);
                    ID = data.ProductID;
                }
                return Content(Convert.ToString(ID));
            }
            catch
            {
                return Content("Oops! Trang này hình như không tồn tại .");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="supplier"></param>
        /// <param name="searchValue"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult List(int category=0, int supplier=0, string searchValue="",int page=1)
        {
            try
            {
                int rowCount = 0;
                int pageSize = 10;
                var model = ProductService.List(page, pageSize, category, supplier, searchValue, out rowCount);
                ViewBag.RowCount = rowCount;
                ViewBag.Page = page;
                int pageCount = rowCount / pageSize;
                if (rowCount % pageSize > 0)
                    pageCount++;
                ViewBag.PageCount = pageCount;

                return View(model);

            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAttributes(int id)
        {
            ViewBag.Title = "Bổ sung thông tin thuộc tính hàng hóa";
            ProductAttribute model = new ProductAttribute()
            {
                AttributeID = 0,
                ProductID = id
            };
            return View("EditAttributes", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditAttributes(int id)
        {
            ViewBag.Title = "Chỉnh sửa thông tin hàng hóa";
            var model = ProductService.GetAttribute(id);
            if (model == null)
                return RedirectToAction("Index"); ///Content("0");
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteAttributes(int id , long[] attributeIds)
        {
            if(attributeIds == null)
            {
                return Content("0");
            }
            ProductService.DeleteAttributes(attributeIds);
            return Content(Convert.ToString(id));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveAttributes(ProductAttribute data)
        {
            try
            {
                if (data.AttributeID == 0)
                    ProductService.AddAttribute(data);
                else ProductService.UpdateAttribute(data);
                return Content(Convert.ToString(data.ProductID));
            }
            catch
            {
                return Content("Oops! Trang này hình như không tồn tại .");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddGallery(int id)
        {
            ViewBag.Title = "Bổ sung thông tin thuộc tính hàng hóa";
            ProductGallery model = new ProductGallery()
            {
                GalleryID = 0,
                ProductID = id
            };
            return View("EditGallery", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditGallery(int id)
        {
            ViewBag.Title = "Chỉnh sửa thông tin hàng hóa";
            var model = ProductService.GetGallery(id);
            if (model == null)
                return RedirectToAction("Index"); ///Content("0");
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteGallery(int id, long[] galleryIds)
        {
            if (galleryIds == null)
            {
                return Content("0");
            }
            ProductService.DeleteGallery(galleryIds);
            return Content(Convert.ToString(id));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult SaveGallery(int id, ProductGallery data)
        {
            try
            {
                if (data.GalleryID == 0)
                    ProductService.AddGallery(data);
                else ProductService.UpdateGallery(data);
                return Content(Convert.ToString(id));
            }
            catch
            {
                return Content("Oops! Trang này hình như không tồn tại .");
            }
        }
    }
}