using System;
using System.Data;
using System.Data.SqlClient;
using store.Src.ProductModule.Entity;
using store.Src.ProductModule.Interface;
using store.Src.UserModule.Entity;
using store.Src.UserModule.Interface;
using store.Src.Utils.Interface;

namespace store.Src.ProductModule
{
    public class ImportInfoRepository : IImportInfoRepository
    {
        private readonly IDBHelper dBHelper;
        private readonly IUserRepository userRepository;
        public ImportInfoRepository(IDBHelper dBHelper, IUserRepository userRepository){
            this.dBHelper = dBHelper;
            this.userRepository = userRepository;
        }
        public bool deleteImportInfo(string importInfoId)
        {
            throw new System.NotImplementedException();
        }

        public ImportInfo getImportInfoByImportInfoId(string importInfoId)
        {
            SqlConnection connection = this.dBHelper.getDBConnection();
            ImportInfo importInfo = null;
            string sql = "SELECT * FROM tblImportInfo WHERE importInfoId=@importInfoId";
            SqlCommand Command = new SqlCommand(sql, connection);

            try{
                connection.Open();
                Command.Parameters.AddWithValue("@importInfoId", importInfoId);
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var managerId = reader.GetString("managerId");
                        User manager = this.userRepository.getUserByUserId(managerId);
                        if(manager == null) break;

                        importInfo = new ImportInfo();
                        importInfo.importInfoId = reader.GetString("importInfoId");
                        importInfo.importDate = reader.GetString("importDate");
                        importInfo.importPrice = reader.GetDouble("importPrice");
                        importInfo.importQuantity = reader.GetInt32("importQuantity");
                        importInfo.expiryDate = reader.GetString("expiryDate");
                        importInfo.note = reader.GetString("note");
                        importInfo.note = reader.GetString("brand");
                        importInfo.manager = manager;
                    }
                }
                connection.Close();
            }
            catch(SqlException e){
                Console.WriteLine(e.Message);
            }
            return importInfo;
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