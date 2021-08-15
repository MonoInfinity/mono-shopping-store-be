
using System.Data.SqlClient;


namespace store.Utils
{
    public interface IDBHelper
    {
        public SqlConnection getDBConnection();
    }
}