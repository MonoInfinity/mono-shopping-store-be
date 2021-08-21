using store.ProductModule.Entity;

namespace store.ProductModule.Interface
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
        public bool updateProduct(Product product);
        public Product getProductByName(string name);
        public Product getProductByProductId(string productId);
    }
}