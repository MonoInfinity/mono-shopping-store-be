using System;
using System.Data;
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

        public Category getCategoryByCategoryId(string categoryId)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            Category category = null;
            string sql = "SELECT * FROM tblCategory WHERE categoryId=@categoryId";
            SqlCommand command = new SqlCommand(sql, connection);

            try{
                connection.Open();
                command.Parameters.AddWithValue("@categoryId", categoryId);
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows){
                    while(reader.Read()){
                        category = new Category();
                        category.categoryId = reader.GetString("categoryId");
                        category.name = reader.GetString("name");
                        category.status = (CategoryStatus)reader.GetInt32("status");
                        category.createDate = reader.GetString("createDate");
                    }
                }

                connection.Close();
            }catch(SqlException e){
                Console.WriteLine(e.Message);
            }
            return category;
        }

        public Category getCategoryByName(string name)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            Category category = null;
            string sql = "SELECT * FROM tblCategory WHERE name=@name";
            SqlCommand command = new SqlCommand(sql, connection);

            try{
                connection.Open();
                command.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows){
                    while(reader.Read()){
                        category = new Category();
                        category.categoryId = reader.GetString("categoryId");
                        category.name = reader.GetString("name");
                        category.status = (CategoryStatus)reader.GetInt32("status");
                        category.createDate = reader.GetString("createDate");
                    }
                }

                connection.Close();
            }catch(SqlException e){
                Console.WriteLine(e.Message);
            }
            return category;
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