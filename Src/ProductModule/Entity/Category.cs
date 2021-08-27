using System;

namespace store.Src.ProductModule.Entity
{
    public enum CategoryStatus
    {
        NOT_SALE = 0,
        SALE = 1
    }
    public class Category
    {
        public string categoryId { get; set; }
        public string name { get; set; }
        public CategoryStatus status { get; set; }
        public string createDate { get; set; }

        public Category()
        {
            this.categoryId = "";
            this.name = "";
            this.status = CategoryStatus.NOT_SALE;
            this.createDate = DateTime.Now.ToShortDateString();
        }

        public override string ToString()
        {
            return "Category: \nCategoryId: " + categoryId + " \nName: " +
            name + " \n Status: " + status + " \nCreateDate: " + createDate;
        }
    }
}