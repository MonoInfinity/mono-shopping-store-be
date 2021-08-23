using System;
using store.Src.UserModule.Entity;

namespace store.Src.ProductModule.Entity
{
    public class ImportInfo
    {
        public string importInfoId { get; set; }
        public string importDate { get; set; }
        public double importPrice { get; set; }
        public string expiryDate { get; set; }
        public int importQuantity { get; set; }
        public string createDate { get; set; }
        public string note { get; set; }
        public string brand { get; set; }
        public User manager { get; set; }

        public ImportInfo(){
            this.importInfoId = "";
            this.importDate = DateTime.Now.ToShortDateString();
            this.importPrice = 0;
            this.expiryDate = DateTime.Now.ToShortDateString();
            this.importQuantity = 0;
            this.createDate = DateTime.Now.ToShortDateString();
            this.note = "";
            this.brand = "";
            this.manager = new User();
        }

        public override string ToString()
        {
            return "ImportInfo:\nImportInfo Id: " + importInfoId + "\nImportDate: " + importDate + "\n ImportPrice: " + importPrice +
            "\nImportQuantity: " + importQuantity + "\nExprixyDate: " + expiryDate + "\nCreateDate: " + createDate + "\nNote: " + note +
            "\nBrand: " + brand + "\nUserId: " + manager.userId;
        }
    }
}