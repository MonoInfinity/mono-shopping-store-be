using System;

namespace store.ProductModule.Entity
{
    public enum SubCategoryStatus
    {
        NOT_SALE = 0,
        SALE = 1
    }
    public class SubCategory
    {
        public string subCategoryId { get; set; }
        public string name { get; set; }
        public SubCategoryStatus status { get; set; }
        public string createDate { get; set; }
        public Category category { get; set; }

        public SubCategory(){
            this.subCategoryId = "";
            this.name = "";
            this.status = SubCategoryStatus.NOT_SALE;
            this.createDate = DateTime.Now.ToShortDateString();
            this.category = new Category();
        }

        public override string ToString()
        {
            return "SubCategory: \nSubCategoryId: " + subCategoryId + " \nName: " + name + "\nStatus: " +
            status + " \nCreateDate: " + createDate + " \nCategoryId: " + category.categoryId;
        }

    }
}