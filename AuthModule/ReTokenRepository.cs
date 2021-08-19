using System;
using System.Data;
using System.Data.SqlClient;
using mono_store_be.AuthModule.Entity;
using mono_store_be.AuthModule.Interface;
using store.Utils.Interface;

namespace mono_store_be.AuthModule
{
    public class ReTokenRepository : IReTokenRepository
    {
        private readonly IDBHelper dbHelper;

        public ReTokenRepository(IDBHelper dbHelper){
            this.dbHelper = dbHelper;
        }
        public ReToken GetReTokenByReTokenId(string reTokenId)
        {
            SqlConnection connection = this.dbHelper.getDBConnection();

            ReToken reToken = null;
            string sql = "SELECT * FROM tblReToken WHERE reTokenId = @reTokenId";
            SqlCommand Command = new SqlCommand(sql, connection);
            try{
                connection.Open();
                Command.Parameters.AddWithValue("@reTokenId", reTokenId);
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        reToken = new ReToken();
                        reToken.reTokenId = reader.GetString("reTokenId");
                        reToken.data = reader.GetString("data");
                        reToken.userId = reader.GetString("userId");

                    }
                }

                connection.Close();
            }catch(SqlException e){
                Console.WriteLine(e.Message);
            }
            return reToken;
        }

        public bool saveReToken(ReToken reToken)
        {
            SqlConnection connection = this.dbHelper.getDBConnection();
            bool res = false;
            string sql = "INSERT INTO tblReToken (reTokenId, data, userId) " +
            " VALUES(@reTokenId, @data, @userId)";
            SqlCommand Command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                Command.Parameters.AddWithValue("@reTokenId", reToken.reTokenId);
                Command.Parameters.AddWithValue("@data", reToken.data);
                Command.Parameters.AddWithValue("@userId", reToken.userId);

                connection.Close();
            }
            catch(SqlException e){
                Console.WriteLine(e.Message);
            }

            return res;
        }
    }
}