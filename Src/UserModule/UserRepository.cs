using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using store.Src.UserModule.Interface;
using store.Src.Utils.Interface;
using store.Src.UserModule.Entity;
using store.Src.UserModule.DTO;

namespace store.Src.UserModule
{
    public class UserRepository : IUserRepository
    {

        private readonly IDBHelper dbHelper;
        public UserRepository(IDBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public User getUserByUserId(string userId)
        {
            SqlConnection connection = this.dbHelper.getDBConnection();

            User user = null;
            string sql = "SELECT * FROM tblUser WHERE userId = @userId";
            SqlCommand Command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                Command.Parameters.AddWithValue("@userId", userId);
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
                        user.createDate = reader.GetString("createDate");
                        user.salary = reader.GetDouble("salary");
                        user.role = (UserRole)reader.GetInt32("role");
                        user.status = (UserStatus)reader.GetInt32("status");
                        user.avatarUrl = reader.GetString("avatarUrl");
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
                        user.createDate = reader.GetString("createDate");
                        user.salary = reader.GetDouble("salary");
                        user.role = (UserRole)reader.GetInt32("role");
                        user.status = (UserStatus)reader.GetInt32("status");
                        user.avatarUrl = reader.GetString("avatarUrl");
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

        public List<User> getAllUsers(int pageSize, int currentPage, string name)
        {
            SqlConnection connection = this.dbHelper.getDBConnection();

            var users = new List<User>();
            string sql = "SELECT TOP (@limit) * FROM tblUser  WHERE name Like  @name EXCEPT SELECT TOP (@skip) * FROM tblUser";
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
                        User user = new User();
                        user.userId = reader.GetString("userId");
                        user.name = reader.GetString("name");
                        user.username = reader.GetString("username");
                        user.password = "";
                        user.email = reader.GetString("email");
                        user.phone = reader.GetString("phone");
                        user.address = reader.GetString("address");
                        user.googleId = reader.GetString("googleId");
                        user.createDate = reader.GetString("createDate");
                        user.salary = reader.GetDouble("salary");
                        user.role = (UserRole)reader.GetInt32("role");
                        user.status = (UserStatus)reader.GetInt32("status");
                        user.avatarUrl = reader.GetString("avatarUrl");

                        users.Add(user);
                    }

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return users;
        }
        public int getAllUsersCount(string name)
        {
            SqlConnection connection = this.dbHelper.getDBConnection();
            int count = 0;

            string sql = "SELECT COUNT(*) FROM tblUser where name Like @name";
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

        public bool saveUser(User user)
        {
            SqlConnection connection = this.dbHelper.getDBConnection();
            bool res = false;
            string sql = "INSERT INTO tblUser " +
            " (userId, name, username, password, email ,phone, address, googleId, createDate, salary, role, status, avatarUrl) " +
            " VALUES (@userId, @name, @username, @password, @email, @phone, @address, @googleId, @createDate, @salary, @role, @status, @avatarUrl) ";
            SqlCommand Command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                Command.Parameters.AddWithValue("@userId", user.userId);
                Command.Parameters.AddWithValue("@name", user.name);
                Command.Parameters.AddWithValue("@username", user.username);
                Command.Parameters.AddWithValue("@password", user.password);
                Command.Parameters.AddWithValue("@email", user.email);
                Command.Parameters.AddWithValue("@phone", user.phone);
                Command.Parameters.AddWithValue("@address", user.address);
                Command.Parameters.AddWithValue("@googleId", user.googleId);
                Command.Parameters.AddWithValue("@createDate", user.createDate);
                Command.Parameters.AddWithValue("@salary", user.salary);
                Command.Parameters.AddWithValue("@role", user.role);
                Command.Parameters.AddWithValue("@status", user.status);
                Command.Parameters.AddWithValue("@avatarUrl", user.avatarUrl);

                res = Command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("This is an error in UserRepository: " + e.Message);
            }
            return res;
        }
        public bool updateUser(User user)
        {
            SqlConnection connection = this.dbHelper.getDBConnection();
            bool res = false;
            string sql = "UPDATE tblUser SET name=@newName, email=@newEmail, phone=@newPhone, address=@newAddress, avatarUrl=@avatarUrl  WHERE userId=@userId";
            SqlCommand Command = new SqlCommand(sql, connection);
            Console.WriteLine(user.name);
            try
            {
                connection.Open();
                Command.Parameters.Add("@userId", SqlDbType.NVarChar).Value = user.userId;
                Command.Parameters.Add("@newName", SqlDbType.NVarChar).Value = user.name;
                Command.Parameters.Add("@newEmail", SqlDbType.NVarChar).Value = user.email;
                Command.Parameters.Add("@newPhone", SqlDbType.NVarChar).Value = user.phone;
                Command.Parameters.Add("@newAddress", SqlDbType.NVarChar).Value = user.address;
                Command.Parameters.Add("@avatarUrl", SqlDbType.NVarChar).Value = user.avatarUrl;
                res = Command.ExecuteNonQuery() > 0;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return res;
        }

        public bool updateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            SqlConnection connection = this.dbHelper.getDBConnection();
            bool res = false;
            string sql = "UPDATE tblUser SET role=@role, salary=@salary, status=@status WHERE userId=@userId";
            SqlCommand Command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                Command.Parameters.Add("@userId", SqlDbType.NVarChar).Value = updateEmployeeDto.userId;
                Command.Parameters.Add("@role", SqlDbType.NVarChar).Value = updateEmployeeDto.role;
                Command.Parameters.Add("@salary", SqlDbType.NVarChar).Value = updateEmployeeDto.salary;
                Command.Parameters.Add("@status", SqlDbType.NVarChar).Value = updateEmployeeDto.status;
                res = Command.ExecuteNonQuery() > 0;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return res;
        }
        public bool updateUserPassword(string userId, string password)
        {
            SqlConnection connection = this.dbHelper.getDBConnection();
            bool res = false;
            string sql = "UPDATE tblUser " +
            " SET password = @password " +
            "WHERE userId = @userId";
            SqlCommand Command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                Command.Parameters.AddWithValue("@password", password);
                Command.Parameters.AddWithValue("@userId", userId);
                res = Command.ExecuteNonQuery() > 0;
            }
            catch (SqlException e)
            {
                Console.WriteLine("This is an error in UserRepository: " + e.Message);
            }
            return res;
        }
    }
}