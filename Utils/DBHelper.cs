using System.Data.SqlClient;

namespace store.Utils
{
    public class DBHelper : IDBHelper
    {
        private readonly IConfig config;
        public DBHelper(IConfig config)
        {
            this.config = config;
        }
        public SqlConnection getDBConnection()
        {
            SqlConnection connection = new SqlConnection(this.config.getEnvByKey("DB_URL"));
            return connection;
        }
    }
}