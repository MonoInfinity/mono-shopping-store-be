using System;
using System.Data;
using System.Data.SqlClient;
using store.ProductModule.Entity;
using store.ProductModule.Interface;
using store.Utils.Interface;

namespace store.ProductModule
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDBHelper dBHelper;
        private readonly ISubCategoryRepository subCategoryRepository;
        public ProductRepository(IDBHelper dBHelper, ISubCategoryRepository subCategoryRepository)
        {
            this.dBHelper = dBHelper;
            this.subCategoryRepository = subCategoryRepository;
        }

        public bool saveProduct(Product product)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "INSERT INTO tblProduct(productId,name,description,location,status,expiryDate,wholesalePrice,retailPrice,createDate,quantity,subCategoryId) " +
            " VALUES(@productId, @name, @description, @location, @status, @expiryDate, @wholesalePrice, @retailPrice, @createDate, @quantity, @subCategoryId)";
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
                command.Parameters.AddWithValue("@subCategoryId", product.subCategory.subCategoryId);
                res = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return res;
        }

        public bool updateProduct(Product product)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "UPDATE tblProduct SET name=@name, description=@description, location=@location, status=@status, wholesalePrice=@wholesalePrice, retailPrice=@retailPrice" +
            ", quantity=@quantity, subCategoryId=@subCategoryId WHERE productId=@productId";
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
                command.Parameters.AddWithValue("@subCategoryId", product.subCategory.subCategoryId);
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

        public Product getProductByProductId(string productId)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            Product product = null;
            string sql = "SELECT * FROM tblProduct WHERE productId=@productId";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@productId", productId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string subCategoryId = reader.GetString("subCategoryId");
                        SubCategory subCategory = this.subCategoryRepository.getSubCategoryBySubCategoryId(subCategoryId);
                        if (subCategory == null) break;

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
    }
}