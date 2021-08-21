using System;
using System.Data;
using System.Data.SqlClient;
using store.Src.ProductModule.Entity;
using store.Src.ProductModule.Interface;
using store.Src.Utils.Interface;

namespace store.Src.ProductModule
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
            string sql = "INSERT INTO tblProduct(productId,name,description,location,status,expiryDate,wholesalePrice,retailPrice,createDate,quantity,imageUrl,subCategoryId) " +
            " VALUES(@productId, @name, @description, @location, @status, @expiryDate, @wholesalePrice, @retailPrice, @createDate, @quantity, @imageUrl, @subCategoryId)";
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
                res = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return res;
        }
    }
}