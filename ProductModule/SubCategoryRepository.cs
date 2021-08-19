using System;
using System.Data.SqlClient;
using store.ProductModule.Entity;
using store.ProductModule.Interface;
using store.Utils.Interface;

namespace store.ProductModule
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly IDBHelper dBHelper;
        public SubCategoryRepository(IDBHelper dBHelper){
            this.dBHelper = dBHelper;
        }
        public bool saveSubCategory(SubCategory subCategory)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "INSERT INTO tblSubCategory(subCategoryId,name,status,createDate,categoryId) " +
            " VALUES(@subCategoryId, @name, @status, @createDate, @categoryId)";
            SqlCommand command = new SqlCommand(sql, connection);
            
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@subCategoryId", subCategory.subCategoryId);
                command.Parameters.AddWithValue("@name", subCategory.name);
                command.Parameters.AddWithValue("@status", subCategory.status);
                command.Parameters.AddWithValue("@createDate", subCategory.createDate);
                command.Parameters.AddWithValue("@categoryId", subCategory.category.categoryId);

                res = command.ExecuteNonQuery() > 0;
                connection.Close();
            }catch(SqlException e){
                Console.WriteLine(e.Message);
            }
            
            return res;
        }

        public bool updateSubCategory(SubCategory subCategory)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "UPDATE tblSubCategory SET name=@name, status=@status WHERE subCategoryId=@subCategoryId";
            SqlCommand command = new SqlCommand(sql, connection);
            
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@name", subCategory.name);
                command.Parameters.AddWithValue("@status", subCategory.status);
                command.Parameters.AddWithValue("@subCategoryId", subCategory.subCategoryId);

                res = command.ExecuteNonQuery() > 0;
                connection.Close();
            }catch(SqlException e){
                Console.WriteLine(e.Message);
            }
            
            return res;
        }
    }
}