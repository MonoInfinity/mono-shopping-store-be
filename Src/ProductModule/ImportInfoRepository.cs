using System;
using System.Data.SqlClient;
using store.Src.ProductModule.Entity;
using store.Src.ProductModule.Interface;
using store.Src.Utils.Interface;

namespace store.Src.ProductModule
{
    public class ImportInfoRepository : IImportInfoRepository
    {
        private readonly IDBHelper dBHelper;
        public ImportInfoRepository(IDBHelper dBHelper){
            this.dBHelper = dBHelper;
        }
        public bool deleteImportInfo(string importInfoId)
        {
            throw new System.NotImplementedException();
        }

        public ImportInfo getImportInfoByImportInfoId(string importInfoId)
        {
            throw new System.NotImplementedException();
        }

        public bool saveImportInfo(ImportInfo importInfo)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            bool res = false;
            string sql = "INSERT INTO tblImportInfo(importInfoId, importDate, importPrice, importQuantity, expiryDate, note, brand, createDate, managerId) " + 
            " VALUES(@importInfoId, @importDate, @importPrice, @importQuantity, @expiryDate, @note, @brand, @createDate, @managerId)";
            SqlCommand command = new SqlCommand(sql, connection);

            try{
                connection.Open();
                command.Parameters.AddWithValue("@importInfoId", importInfo.importInfoId);
                command.Parameters.AddWithValue("@importDate", importInfo.importDate);
                command.Parameters.AddWithValue("@importPrice", importInfo.importPrice);
                command.Parameters.AddWithValue("@importQuantity", importInfo.importQuantity);
                command.Parameters.AddWithValue("@expiryDate", importInfo.expiryDate);
                command.Parameters.AddWithValue("@note", importInfo.note);
                command.Parameters.AddWithValue("@brand", importInfo.brand);
                command.Parameters.AddWithValue("@createDate", importInfo.createDate);
                command.Parameters.AddWithValue("@managerId", importInfo.manager.userId);

                res = command.ExecuteNonQuery() > 0;
                connection.Close();
            }
            catch(SqlException e){
                Console.WriteLine(e.Message);
            }
            return res;
        }

        public bool updateImportInfo(ImportInfo importInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}