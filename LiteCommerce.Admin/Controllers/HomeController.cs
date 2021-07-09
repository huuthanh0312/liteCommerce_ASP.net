using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.countCustomer = DataService.CountCustomer("");
            ViewBag.countCategory = DataService.CountCategory("");
            ViewBag.countEmployee = DataService.CountEmployee("");
            ViewBag.countShipper = DataService.CountShipper("");
            ViewBag.countSupplier = DataService.CountSupplier("");
            ViewBag.countProduct = ProductService.CountProduct(0, 0, "");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}