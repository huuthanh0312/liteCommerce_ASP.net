using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        public ProductDAL (string connectionString) : base(connectionString)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Product data)
        {
            int ProductID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Products
                                    (
                                     ProductName, SupplierID, CategoryID, Unit, Price, Photo
                                    )
                                    VALUES
                                    (
                                     @ProductName, @SupplierID, @CategoryID, @Unit, @Price, @Photo
                                    );
                                    SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);

                ProductID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return ProductID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long AddAttribute(ProductAttribute data)
        {
            int AttributeID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO ProductAttributes
                                    (
                                     ProductID, AttributeName, AttributeValue, DisplayOrder
                                    )
                                    VALUES
                                    (
                                     @ProductID, @AttributeName, @AttributeValue, @DisplayOrder
                                    );
                                    SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);

                AttributeID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return AttributeID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long AddGallery(ProductGallery data)
        {
            int GalleryID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO ProductGallery
                                    (
                                     ProductID, Photo, Description, DisplayOrder, IsHidden
                                    )
                                    VALUES
                                    (
                                     @ProductID, @Photo, @Description, @DisplayOrder, @IsHidden
                                    );
                                    SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);

                GalleryID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return GalleryID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="supplierId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(int categoryId, int supplierId, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT  COUNT (*) 
                                    FROM    Products 
                                    WHERE   (@categoryId = 0 OR CategoryID = @categoryId)
                                            AND  (@supplierId = 0 OR SupplierID = @supplierId)
                                            AND (@searchValue = '' OR ProductName LIKE @searchValue)";
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@supplierId", supplierId);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                result = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public bool Delete(int productId)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM ProductAttributes WHERE ProductAttributes.ProductID = @ProductID 
                                                                        AND NOT EXISTS ( SELECT * FROM OrderDetails WHERE OrderDetails.ProductID = @ProductID)
                                    DELETE FROM ProductGallery WHERE ProductGallery.ProductID = @ProductID 
                                                                        AND NOT EXISTS ( SELECT * FROM OrderDetails WHERE OrderDetails.ProductID = @ProductID)
                                    DELETE FROM Products WHERE ProductID = @ProductID
                                                                        AND NOT EXISTS ( SELECT * FROM OrderDetails WHERE OrderDetails.ProductID = @ProductID)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", productId);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        public bool DeleteAttribute(long attributeId)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM ProductAttributes
                                    WHERE AttributeID = @attributeId";
                /// AND NOT EXISTS( SELECT * FROM ProductA WHERE SupplierID = Suppliers.SupplierID)
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@attributeId", attributeId);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        public bool DeleteGallery(long galleryId)
        {
            int result;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM ProductGallery 
                                    WHERE GalleryID = @galleryId";
                /// AND NOT EXISTS( SELECT * FROM ProductA WHERE SupplierID = Suppliers.SupplierID)
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@galleryId", galleryId);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product Get(int productId)
        {
            Product data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Products WHERE ProductID =@ProductID";
                cmd.Parameters.AddWithValue("@ProductID", productId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    if (dbReader.Read())
                    {
                        data = new Product()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            Unit = Convert.ToString(dbReader["Unit"]),
                            Price = Convert.ToInt32(dbReader["Price"]),
                            Photo = Convert.ToString(dbReader["Photo"]),

                        };
                    }

                }
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        public ProductAttribute GetAttribute(long attributeId)
        {
            ProductAttribute data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM ProductAttributes WHERE AttributeID =@AttributeId";
                cmd.Parameters.AddWithValue("@AttributeId", attributeId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {

                    if (dbReader.Read())
                    {
                        data = new ProductAttribute()
                        {
                            AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),
                            AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),

                        };
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductEX GetEX(int productId)
        {
            Product product = this.Get(productId);
            if(product == null)
            {
                return null;
            }
            List<ProductAttribute> listAttributes = this.ListAttributes(productId);
            List<ProductGallery> listGallery = this.ListGalleries(productId);
            ProductEX data = new ProductEX()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                SupplierID = product.SupplierID,
                CategoryID = product.CategoryID,
                Unit = product.Unit,
                Price = product.Price,
                Photo = product.Photo,
                Attributes = listAttributes,
                Galleries = listGallery
            };
            return data;
        } 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        public ProductGallery GetGallery(long galleryId)
        {
            ProductGallery data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM ProductGallery WHERE GalleryID =@galleryId";
                cmd.Parameters.AddWithValue("@galleryId", galleryId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {

                    if (dbReader.Read())
                    {
                        data = new ProductGallery()
                        {
                            GalleryID = Convert.ToInt32(dbReader["GalleryID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                            IsHidden = Convert.ToInt32(dbReader["IsHidden"])
                        };
                    }

                }
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="CategoryId"></param>
        /// <param name="suplierId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Product> List(int page, int pageSize, int categoryId, int supplierId, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            List<Product> data = new List<Product>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT  *
                                    FROM
                                    (
                                        SELECT  *, ROW_NUMBER() OVER(ORDER BY ProductName) AS RowNumber
                                        FROM    Products 
                                        WHERE   (@categoryId = 0 OR CategoryID = @categoryId)
                                            AND  (@supplierId = 0 OR SupplierID = @supplierId)
                                            AND (@searchValue = '' OR ProductName LIKE @searchValue)
                                    ) AS s
                                    WHERE s.RowNumber BETWEEN (@page - 1)*@pageSize + 1 AND @page*@pageSize";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@supplierId", supplierId);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Product()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            Unit = Convert.ToString(dbReader["Unit"]),
                            Price = Convert.ToDecimal(dbReader["Price"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            
                        });
                    }
                }
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductAttribute> ListAttributes(int productId)
        {
            List<ProductAttribute> data = new List<ProductAttribute>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT  *
                                    FROM ProductAttributes
                                    WHERE @ProductId = 0 OR ProductID = @ProductId
                                    ORDER BY DisplayOrder ASC";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductId", productId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new ProductAttribute()
                        {
                            AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),
                            AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),

                        });
                    }
                }
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductGallery> ListGalleries(int productId)
        {
            List<ProductGallery> data = new List<ProductGallery>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT  *
                                    FROM ProductGallery
                                    WHERE @ProductId = 0 OR ProductID = @ProductId
                                    ORDER BY DisplayOrder ASC";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductId", productId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new ProductGallery()
                        {
                            GalleryID = Convert.ToInt32(dbReader["GalleryID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                            IsHidden = Convert.ToInt32(dbReader["IsHidden"])

                        });
                    }
                }
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Product data)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Products
                                    SET 
                                        ProductName=@ProductName,
	                                    SupplierID=@SupplierID,
	                                    CategoryID=@CategoryID,        
	                                    Unit=@Unit,
	                                    Price=@Price,
                                        Photo=@Photo
                                    WHERE
                                        ProductID =@ProductID;";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);

                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateAttribute(ProductAttribute data)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE ProductAttributes
                                    SET 
                                        ProductID=@ProductID,
	                                    AttributeName=@AttributeName,
	                                    AttributeValue=@AttributeValue,        
	                                    DisplayOrder=@DisplayOrder
                                    WHERE
                                        AttributeID =@AttributeID;";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);

                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateGallery(ProductGallery data)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE ProductGallery 
                                    SET ProductID = @ProductID, 
                                        Photo = @Photo,
                                        Description = @Description,
                                        DisplayOrder = @DisplayOrder,
                                        IsHidden = @IsHidden
                                   WHERE GalleryID = @GalleryID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@GalleryID", data.GalleryID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }
    }
}
