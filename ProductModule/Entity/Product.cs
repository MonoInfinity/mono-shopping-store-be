using System;

namespace store.ProductModule.Entity
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
        public DateTime expiryDate { get; set; }
        public double wholesalePrice { get; set; }
        public double retailPrice { get; set; }
        public DateTime createDate { get; set; }
        public int quantity { get; set; }
        public SubCategory subCategory { get; set; }

        public Product()
        {
            this.productId = "";
            this.name = "";
            this.description = "";
            this.location = "";
            this.status = ProductStatus.NOT_SALE;
            this.expiryDate = DateTime.Now;
            this.wholesalePrice = 0;
            this.retailPrice = 0;
            this.createDate = DateTime.Now;
            this.quantity = 0;
            this.subCategory = new SubCategory();
        }

        public override string ToString()
        {
            return "Product: \nProductId: " + productId + " \nName: " + name + " \nDescription: " + description + " \nLocation: " +
                            location + " \nStatus: " + status + " \nExpiry Date: " + expiryDate + " \nWholesale Price: " + wholesalePrice + " \nRetail Price: " +
                            retailPrice + " \nCreateDate: " + createDate + " \nQuantity: " + quantity + " \nSubCategogyId: " + subCategory.status;
        }
    }
}