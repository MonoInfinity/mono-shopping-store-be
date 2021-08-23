using System;
using System.Data;
using System.Data.SqlClient;
using store.Src.ProductModule.Entity;
using store.Src.ProductModule.Interface;
using store.Src.Utils.Interface;
using System.Collections.Generic;

namespace store.Src.ProductModule
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDBHelper dBHelper;
        private readonly ISubCategoryRepository subCategoryRepository;
        private readonly IImportInfoRepository importInfoRepository;
        public ProductRepository(IDBHelper dBHelper, ISubCategoryRepository subCategoryRepository, IImportInfoRepository importInfoRepository)
        {
            this.dBHelper = dBHelper;
            this.importInfoRepository = importInfoRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public bool saveProduct(Product product)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "INSERT INTO tblProduct(productId,name,description,location,status,expiryDate,wholesalePrice,retailPrice,createDate,quantity,imageUrl,subCategoryId,importInfoId) " +
            " VALUES(@productId, @name, @description, @location, @status, @expiryDate, @wholesalePrice, @retailPrice, @createDate, @quantity, @imageUrl, @subCategoryId, @importInfoId)";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@productId", product.productId);
                command.Parameters.AddWithValue("@name", product.name);
                command.Parameters.AddWithValue("@description", product.description);
                command.Parameters.AddWithValue("@location", product.location);
                command.Parameters.AddWithValue("@status", product.status);
                command.Parameters.AddWithValue("@expiryDate", product.expiryDate);
                command.Parameters.AddWithValue("@wholesalePrice", product.wholesalePrice);
                command.Parameters.AddWithValue("@retailPrice", product.retailPrice);
                command.Parameters.AddWithValue("@createDate", product.createDate);
                command.Parameters.AddWithValue("@quantity", product.quantity);
                command.Parameters.AddWithValue("@imageUrl", product.imageUrl);
                command.Parameters.AddWithValue("@subCategoryId", product.subCategory.subCategoryId);
                command.Parameters.AddWithValue("@importInfoId", product.importInfo.importInfoId);
                res = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return res;
        }

        public bool deleteProduct(string productId)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "DELETE FROM tblProduct WHERE productId=@productId";
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@productId", productId);
                res = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return res;
        }
        public List<Product> getAllProducts(int pageSize, int currentPage, string name)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();

            var products = new List<Product>();
            string sql = "SELECT TOP (@limit) * FROM tblProduct WHERE name Like  @name EXCEPT SELECT TOP (@skip) * FROM tblProduct";
            SqlCommand Command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                Command.Parameters.AddWithValue("@name ", "%" + name + "%");
                Command.Parameters.AddWithValue("@limit ", (pageSize + 1) * currentPage);
                Command.Parameters.AddWithValue("@skip ", currentPage * pageSize);
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.productId = reader.GetString("productId");
                        product.name = reader.GetString("name");
                        product.description = reader.GetString("description");
                        product.location = reader.GetString("location");
                        product.status = (ProductStatus)reader.GetInt32("status");
                        product.expiryDate = reader.GetString("expiryDate");
                        product.wholesalePrice = reader.GetDouble("wholesalePrice");
                        product.retailPrice = reader.GetDouble("retailPrice");
                        product.createDate = reader.GetString("createDate");
                        product.quantity = reader.GetInt32("quantity");
                        product.subCategory.subCategoryId = reader.GetString("subCategoryId");
                        product.importInfo.importInfoId = reader.GetString("importInfoId");

                        products.Add(product);
                    }

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return products;
        }
        public int getAllProductsCount(string name)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            int count = 0;

            string sql = "SELECT COUNT(*) FROM tblProduct where name Like @name";
            SqlCommand Command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                Command.Parameters.AddWithValue("@name ", "%" + name + "%");
                count = (Int32)Command.ExecuteScalar();
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return count;
        }

        public Product getProductByProductId(string productId)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();

            Product product = null;
            string sql = "SELECT * FROM tblProduct WHERE productId = @productId";
            SqlCommand Command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                Command.Parameters.AddWithValue("@productId", productId);
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var subCategoryId = reader.GetString("subCategoryId");
                        SubCategory subCategory = this.subCategoryRepository.getSubCategoryBySubCategoryId(subCategoryId);
                        if(subCategory == null) break;

                        var importInfoId = reader.GetString("importInfoId");
                        ImportInfo importInfo = this.importInfoRepository.getImportInfoByImportInfoId(importInfoId);
                        if(importInfo == null) break;


                        product = new Product();
                        product.productId = reader.GetString("productId");
                        product.name = reader.GetString("name");
                        product.description = reader.GetString("description");
                        product.location = reader.GetString("location");
                        product.status = (ProductStatus)reader.GetInt32("status");
                        product.expiryDate = reader.GetString("expiryDate");
                        product.wholesalePrice = reader.GetDouble("wholesalePrice");
                        product.retailPrice = reader.GetDouble("retailPrice");
                        product.createDate = reader.GetString("createDate");
                        product.quantity = reader.GetInt32("quantity");
                        product.subCategory = subCategory;
                        product.importInfo = importInfo;
                    }

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return product;
        }

        public bool updateProduct(Product product)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "UPDATE tblProduct SET name=@name, description=@description, location=@location, status=@status, wholesalePrice=@wholesalePrice, retailPrice=@retailPrice" +
            ", quantity=@quantity, imageUrl=@imageUrl, subCategoryId=@subCategoryId, importInfoId=@importInfoId WHERE productId=@productId";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@name", product.name);
                command.Parameters.AddWithValue("@description", product.description);
                command.Parameters.AddWithValue("@location", product.location);
                command.Parameters.AddWithValue("@status", product.status);
                command.Parameters.AddWithValue("@wholesalePrice", product.wholesalePrice);
                command.Parameters.AddWithValue("@retailPrice", product.retailPrice);
                command.Parameters.AddWithValue("@quantity", product.quantity);
                command.Parameters.AddWithValue("@imageUrl", product.imageUrl);
                command.Parameters.AddWithValue("@subCategoryId", product.subCategory.subCategoryId);
                command.Parameters.AddWithValue("@importInfoId", product.importInfo.importInfoId);
                command.Parameters.AddWithValue("@productId", product.productId);
                res = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return res;
        }

        public Product getProductByname(string name)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            Product product = null;
            string sql = "SELECT * FROM tblProduct WHERE name=@name";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string subCategoryId = reader.GetString("subCategoryId");
                        SubCategory subCategory = this.subCategoryRepository.getSubCategoryBySubCategoryId(subCategoryId);
                        if (subCategory == null) break;

                        string importInfoId = reader.GetString("importInfoId");
                        ImportInfo importInfo = this.importInfoRepository.getImportInfoByImportInfoId(importInfoId);
                        if(importInfo == null) break;

                        product = new Product();
                        product.productId = reader.GetString("productId");
                        product.name = reader.GetString("name");
                        product.description = reader.GetString("description");
                        product.location = reader.GetString("location");
                        product.status = (ProductStatus)reader.GetInt32("status");
                        product.expiryDate = reader.GetString("expiryDate");
                        product.wholesalePrice = reader.GetDouble("wholesalePrice");
                        product.retailPrice = reader.GetDouble("retailPrice");
                        product.createDate = reader.GetString("createDate");
                        product.quantity = reader.GetInt32("quantity");
                        product.subCategory = subCategory;
                        product.importInfo = importInfo;
                        product.imageUrl = reader.GetString("imageUrl");
                    }

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return product;
        }

        public bool updateCategory(Category category)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "UPDATE tblCategory " +
            " SET name = @name, status = @status " +
            "WHERE categoryId = @categoryId";
            SqlCommand Command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                Command.Parameters.AddWithValue("@categoryId", category.categoryId);
                Command.Parameters.AddWithValue("@name", category.name);
                Command.Parameters.AddWithValue("@status", category.status);
                res = Command.ExecuteNonQuery() > 0;

            }
            catch (SqlException e)
            {
                Console.WriteLine("This is an error in CategoryRepository: " + e.Message);
            };
            return res;
        }

        public bool updateSubCategory(SubCategory subCategory)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "UPDATE tblSubCategory " +
            " SET name = @name, status = @status " +
            "WHERE subCategoryId = @subCategoryId";
            SqlCommand Command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                Command.Parameters.AddWithValue("@subCategoryId", subCategory.subCategoryId);
                Command.Parameters.AddWithValue("@name", subCategory.name);
                Command.Parameters.AddWithValue("@status", subCategory.status);
                res = Command.ExecuteNonQuery() > 0;

            }
            catch (SqlException e)
            {
                Console.WriteLine("This is an error in SubCategoryRepository: " + e.Message);
            };
            return res;
        }
    }
}