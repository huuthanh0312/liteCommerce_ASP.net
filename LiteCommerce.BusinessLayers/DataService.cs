using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// các chức năng nghiệp vị liên quan đến quản lý dữ liệu chung
    /// </summary>
    public static class DataService
    {
        private static ICountryDAL CountryDB;
        private static ICityDAL CityDB;
        private static ISupplierDAL SupplierDB;
        private static ICategoryDAL CategoryDB;
        private static IShipperDAL ShipperDB;
        private static IEmployeeDAL EmployeeDB;
        private static ICustomerDAL CustomerDB;

        /// khởi tạo tính năng tác nghiệp ( hàm này phải được gọi nếu muốn sử dụng các tinh năng của lớp này)
        /// </summary>
        /// <param name="dbTyte"></param>
        /// <param name="connectionString"></param>
        public static void Init(DatabaseTypes dbTyte, string connectionString)
        {
            switch (dbTyte)
            {
                case DatabaseTypes.SQLServer:
                    CountryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
                    CityDB = new DataLayers.SQLServer.CityDAL(connectionString);
                    SupplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
                    CategoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
                    ShipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
                    EmployeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
                    CustomerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
                    break;
                default:
                    throw new Exception("Database tyte is not support");

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListCountries()
        {
            return CountryDB.List();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<City> ListCities()
        {
            return CityDB.List();
        }
        /// <summary>
        /// lấy danh sách id và tên loại hàng hóa
        /// </summary>
        /// <returns></returns>
        public static List<Category> ListParentCategories()
        {
            return CategoryDB.ListParentCategories();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> ListParentSuppliers()
        {
            return SupplierDB.ListParentSuppliers();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryname"></param>
        /// <returns></returns>
        public static List<City> ListCities(string countryname)
        {
            return CityDB.List(countryname);
        }

        /// <summary>
        /// Lấy danh sách khách hàng (tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = CustomerDB.Count(searchValue);
            return CustomerDB.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// Lấy thông tin 1 khách hàng
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerId)
        {
            return CustomerDB.Get(customerId);
        }
        /// <summary>
        /// Đếm số lượng khách hàng
        /// </summary>
        /// <returns></returns>
        public static int CountCustomer(string searchValue)
        {
            return CustomerDB.Count(searchValue);
        }
        /// <summary>
        /// Thêm khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return CustomerDB.Add(data);
        }
        /// <summary>
        /// Cập nhật khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return CustomerDB.Update(data);
        }
        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerId)
        {
            return CustomerDB.Delete(customerId);
        }

        /// <summary>
        /// Lấy danh sách loại hàng hóa (tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Category> ListCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = CategoryDB.Count(searchValue);
            return CategoryDB.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// Thêm loại hàng hóa
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return CategoryDB.Add(data);
        }
        /// <summary>
        /// Cập nhật loại hàng hóa
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return CategoryDB.Update(data);
        }
        /// <summary>
        /// Xóa loại hàng hóa
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryId)
        {
            return CategoryDB.Delete(categoryId);
        }
        /// <summary>
        /// Đếm số lượng loại hàng hoá
        /// </summary>
        /// <returns></returns>
        public static int CountCategory(string searchValue)
        {
            return CategoryDB.Count(searchValue);
        }
        /// <summary>
        /// Lấy thông tin 1 loại hàng hóa
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryId)
        {
            return CategoryDB.Get(categoryId);
        }

        /// <summary>
        ///Lấy danh sách nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        public static List<Employee> ListEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = EmployeeDB.Count(searchValue);
            return EmployeeDB.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Employeeid"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int Employeeid)
        {
            return EmployeeDB.Get(Employeeid);
        }
        /// <summary>
        /// Đếm số lượng nhân viên
        /// </summary>
        /// <returns></returns>
        public static int CountEmployee(string searchValue)
        {
            return EmployeeDB.Count(searchValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return EmployeeDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return EmployeeDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Employeeid"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int Employeeid)
        {
            return EmployeeDB.Delete(Employeeid);
        }

        /// <summary>
        /// Lấy danh sách nhà vận chuyển
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        public static List<Shipper> ListShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = ShipperDB.Count(searchValue);
            return ShipperDB.List(page, pageSize, searchValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Shipperid"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int Shipperid)
        {
            return ShipperDB.Get(Shipperid);
        }
        /// <summary>
        /// Đếm số lượng nhà vận chuyển
        /// </summary>
        /// <returns></returns>
        public static int CountShipper(string searchValue)
        {
            return ShipperDB.Count(searchValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return ShipperDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return ShipperDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Shipperid"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int Shipperid)
        {
            return ShipperDB.Delete(Shipperid);
        }

        /// <summary>
        /// Lấy danh sách nhà cung cấp
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Supplier> ListSuppliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = SupplierDB.Count(searchValue);
            return SupplierDB.List(page, pageSize, searchValue);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Supplierid"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int Supplierid)
        {
            return SupplierDB.Get(Supplierid);
        }
        /// <summary>
        /// Đếm số lượng nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public static int CountSupplier(string searchValue)
        {
            return SupplierDB.Count(searchValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return SupplierDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return SupplierDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Supplierid"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int Supplierid)
        {
            return SupplierDB.Delete(Supplierid);
        }
        
    }
}
