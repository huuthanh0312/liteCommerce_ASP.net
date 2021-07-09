using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LiteCommerce.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LiteCommerceDB"].ConnectionString;
            DataService.Init(DatabaseTypes.SQLServer, connectionString);
            ProductService.Init(DatabaseTypes.SQLServer, connectionString);
            ReportService.Init(DatabaseTypes.SQLServer, connectionString); ;
            SaleService.Init(DatabaseTypes.SQLServer, connectionString);
            AccountService.Init(DatabaseTypes.SQLServer, connectionString, AccountTypes.Employee);


        }
    }
}
