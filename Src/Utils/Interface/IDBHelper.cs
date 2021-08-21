
using System.Data.SqlClient;


namespace store.Src.Utils.Interface
{
    public interface IDBHelper
    {
        public SqlConnection getDBConnection();
    }
}