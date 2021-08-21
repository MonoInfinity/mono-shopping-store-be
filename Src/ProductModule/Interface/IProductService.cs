using store.Src.ProductModule.Entity;

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
    }
}