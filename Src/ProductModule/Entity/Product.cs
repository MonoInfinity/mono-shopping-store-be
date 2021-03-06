using System;

namespace store.Src.ProductModule.Entity
{

    public enum ProductStatus
    {
        NOT_SALE = 0,
        SALE = 1,
        SOLD_OUT = 2
    }

    public class Product
    {
        public string productId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public ProductStatus status { get; set; }
        public double wholesalePrice { get; set; }
        public double retailPrice { get; set; }
        public string createDate { get; set; }
        public int quantity { get; set; }
        public string imageUrl { get; set; }
        public SubCategory subCategory { get; set; }
        public ImportInfo importInfo { get; set; }
        public Product()
        {
            this.productId = "";
            this.name = "";
            this.description = "";
            this.location = "";
            this.imageUrl = "";
            this.status = ProductStatus.NOT_SALE;
            this.wholesalePrice = 0;
            this.retailPrice = 0;
            this.createDate = DateTime.Now.ToShortDateString();
            this.quantity = 0;
            this.subCategory = new SubCategory();
            this.importInfo = new ImportInfo();
        }

        public override string ToString()
        {
            return "Product: \nProductId: " + productId + " \nName: " + name + " \nImageUrl" + imageUrl + " \nDescription: " + description + " \nLocation: " +
                            location + " \nStatus: " + status  + " \nWholesale Price: " + wholesalePrice + " \nRetail Price: " +
                            retailPrice + " \nCreateDate: " + createDate + " \nQuantity: " + quantity + " \nSubCategogyId: " + subCategory.subCategoryId + " \nImportInfoId: " + importInfo.importInfoId;
        }
    }
}