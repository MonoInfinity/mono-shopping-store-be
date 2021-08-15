using System;
using System.Data;
using System.Data.SqlClient;

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

            User User = null;
            string Sql = "SELECT * FROM tblUser WHERE Username = @Username";
            SqlCommand Command = new SqlCommand(Sql, this.dbHelper.getDBConnection());
            try
            {
                connection.Open();
                Command.Parameters.AddWithValue("@Username", username);
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User = new User(reader.GetString("username"), reader.GetString("password"));
                    }

                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return User;
        }
    }
}