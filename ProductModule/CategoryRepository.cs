using System;
using System.Data.SqlClient;
using store.ProductModule.Entity;
using store.ProductModule.Interface;
using store.Utils.Interface;

namespace store.ProductModule
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDBHelper dBHelper;
        public CategoryRepository(IDBHelper dBHelper){
            this.dBHelper = dBHelper;
        }
        public bool saveCategory(Category category)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "INSERT INTO tblCategory (categoryId, name, status, createDate) " +
            " VALUES(@categoryId, @name, @status, @createDate)";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@categoryId", category.categoryId);
                command.Parameters.AddWithValue("@name", category.name);
                command.Parameters.AddWithValue("@status", category.status);
                command.Parameters.AddWithValue("@createDate", category.createDate);

                res = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            catch(SqlException e){
                Console.WriteLine(e.Message);
            }

            return res;
        }

        public bool updateCategory(Category category)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "UPDATE tblCategory SET name=@name, status=@status WHERE categoryId=@categoryId";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@name", category.name);
                command.Parameters.AddWithValue("@status", category.status);
                command.Parameters.AddWithValue("@categoryId", category.categoryId);
                res = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            catch(SqlException e){
                Console.WriteLine(e.Message);
            }

            return res;
        }
    }
}