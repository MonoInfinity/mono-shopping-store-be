
using System.Data.SqlClient;


namespace store.Utils.Interface
{
    public interface IDBHelper
    {
        public SqlConnection getDBConnection();
    }
}