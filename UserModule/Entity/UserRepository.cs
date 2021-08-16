using System;
using System.Data;
using System.Data.SqlClient;
using store.UserModule.Interface;
using store.Utils;

namespace store.UserModule.Entity
{
    public class UserRepository : IUserRepository
    {

        private readonly IDBHelper dbHelper;
        public UserRepository(IDBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }
    
        public User getUserByUsername(string username)
        {
            SqlConnection connection = this.dbHelper.getDBConnection();

            User user = null;
            string sql = "SELECT * FROM tblUser WHERE username = @username";
            SqlCommand Command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                Command.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user = new User();
                        user.userId = reader.GetString("userId");
                        user.name = reader.GetString("name");
                        user.username = reader.GetString("username");
                        user.password = reader.GetString("password");
                        user.email = reader.GetString("email");
                        user.phone = reader.GetString("phone");
                        user.address = reader.GetString("address");
                        user.googleId = reader.GetString("googleId");
                        user.createDate = reader.GetDateTime("createDate");
                        user.salary = reader.GetDouble("salary");
                        user.role = reader.GetString("role");
                    }

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return user;
        }

        public bool saveUser(User user)
        {
            SqlConnection connection = this.dbHelper.getDBConnection();
            bool res = false;
            string sql = "INSERT INTO tblUser " +
            " (userId, name, username, password, email ,phone, address, googleId, createDate, salary, role) " + 
            " VALUES (@userId, @name, @username, @password, @email, @phone, @address, @googleId, @createDate, @salary, @role) ";
            SqlCommand Command = new SqlCommand(sql, connection);
            
            try{
                connection.Open();
                Command.Parameters.AddWithValue("@userId", user.userId);
                Command.Parameters.AddWithValue("@name", user.name);
                Command.Parameters.AddWithValue("@username", user.username);
                Command.Parameters.AddWithValue("@password", user.password);
                Command.Parameters.AddWithValue("@email", user.email);
                Command.Parameters.AddWithValue("@phone", user.phone);
                Command.Parameters.AddWithValue("@address", user.address);
                Command.Parameters.AddWithValue ("@googleId", user.googleId);
                Command.Parameters.AddWithValue("@createDate", user.createDate);
                Command.Parameters.AddWithValue("@salary", user.salary);
                Command.Parameters.AddWithValue("@role", user.role);

                res = Command.ExecuteNonQuery() > 0;
            }
            catch(SqlException e)
            {
                Console.WriteLine("This is an error in UserRepository: " + e.Message);
            }
            return res;
        }
    }
}