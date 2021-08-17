using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using mono_shopping_store_be.Utils.Interface;

namespace mono_shopping_store_be.Utils
{
    public class UploadFileService : IUploadFileService
    {
        readonly string folderUrl  = "/public/image/";
        public bool checkFileExtension(IFormFile file, string[] extensions)
        {
            bool result = false;
            string fileExtension = file.FileName.ToLower().Split(".")[file.FileName.ToLower().Split(".").Length - 1];
            foreach(string extension in extensions){
                if(extension == fileExtension){
                    result = true;
                }
            }
            return result;
        }

        public bool checkFileSize(IFormFile file, int limit)
        {
            return file.Length < limit * 1024 * 1024;
        }

        public string upload(IFormFile file){
            string formatFolderUrl = "." + folderUrl;
            string fortmatFileName = System.Guid.NewGuid() + file.FileName;

            try{
                if(!Directory.Exists(formatFolderUrl)){
                    Directory.CreateDirectory(formatFolderUrl);
                }

                using(FileStream fileStream = System.IO.File.Create(formatFolderUrl + fortmatFileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
                return folderUrl + fortmatFileName;
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}