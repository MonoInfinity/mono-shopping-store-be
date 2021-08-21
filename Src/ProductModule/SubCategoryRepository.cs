using System;
using System.Data;
using System.Data.SqlClient;
using store.Src.ProductModule.Entity;
using store.Src.ProductModule.Interface;
using store.Src.Utils.Interface;

namespace store.Src.ProductModule
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly IDBHelper dBHelper;
        private readonly ICategoryRepository categoryRepository;
        public SubCategoryRepository(IDBHelper dBHelper, ICategoryRepository categoryRepository)
        {
            this.dBHelper = dBHelper;
            this.categoryRepository = categoryRepository;
        }

        public SubCategory getSubCategoryByname(string name)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            SubCategory subCategory = null;
            string sql = "SELECT * FROM tblSubCategory WHERE name=@name";
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
                        string categoryId = reader.GetString("categoryId");
                        Category category = this.categoryRepository.getCategoryByCategoryId(categoryId);
                        if (category == null) break;

                        subCategory = new SubCategory();
                        subCategory.subCategoryId = reader.GetString("subCategoryId");
                        subCategory.name = reader.GetString("name");
                        subCategory.status = (SubCategoryStatus)reader.GetInt32("status");
                        subCategory.createDate = reader.GetString("createDate");
                        subCategory.category = category;
                    }
                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return subCategory;
        }

        public SubCategory getSubCategoryBySubCategoryId(string subCategoryId)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            SubCategory subCategory = null;
            string sql = "SELECT * FROM tblSubCategory WHERE subCategoryId=@subCategoryId";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@subCategoryId", subCategoryId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string categoryId = reader.GetString("categoryId");
                        Category category = this.categoryRepository.getCategoryByCategoryId(categoryId);
                        if (category == null) break;

                        subCategory = new SubCategory();
                        subCategory.subCategoryId = reader.GetString("subCategoryId");
                        subCategory.name = reader.GetString("name");
                        subCategory.status = (SubCategoryStatus)reader.GetInt32("status");
                        subCategory.createDate = reader.GetString("createDate");
                        subCategory.category = category;
                    }
                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return subCategory;
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
            }
            catch (SqlException e)
            {
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
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return res;
        }
    }
}