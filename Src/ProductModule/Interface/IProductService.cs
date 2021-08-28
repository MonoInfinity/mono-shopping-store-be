using store.Src.ProductModule.Entity;
using System.Collections.Generic;

namespace store.Src.ProductModule.Interface
{
    public interface IProductService
    {
        public bool saveCategory(Category category);
        public bool saveSubCategory(SubCategory subCategory);
        public bool updateCategory(Category category);
        public bool updateSubCategory(SubCategory subCategory);
        public Category getCategoryByCategoryId(string categoryId);
        public Category getCategoryByCategoryName(string name);
        public SubCategory getSubCategoryBySubCategoryId(string subCategoryId);
        public SubCategory getSubCategoryBySubCategoryName(string name);

        public List<Category> getAllCategory();
        public List<SubCategory> getAllSubCategory(int currentPage, int pageSize, string name);
        public int getAllSubCategoryCount(string name);
        public List<SubCategory> getSubCategoryByCategoryId(string categoryId);

        public bool saveProduct(Product product);
        // public bool updateProduct(Product product);
        // public Product getProductByName(string name);
        // public bool deleteProduct(string productId);
        // public List<Product> getAllProduct(int currentPage, int pageSize, string name);
        // public int getAllProductCount(string name);
        // public Product getProductByProductId(string productId);
        // public bool saveImportInfo(ImportInfo importInfo);
        // public ImportInfo getImportInfoByImportInfoId(string importInfoId);

        // public bool updateImportInfo(ImportInfo importInfo);
        // public bool deleteImportInfo(string importInfoId);
    }
}