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

        public SubCategory getSubCategoryBySubCategoryId(string subCategoryId);
        public bool saveProduct(Product product);
        public bool deleteProduct(string productId);
        public List<Product> getAllProduct(int currentPage, int pageSize, string name);
        public int getAllProductCount(string name);
        public Product getProductByProductId(string productId);
    }
}