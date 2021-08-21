using store.Src.ProductModule.Entity;
using store.Src.ProductModule.Interface;

namespace store.Src.ProductModule
{
    public class ProductService : IProductService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ISubCategoryRepository subCategoryRepository;
        private readonly IProductRepository productRepository;
        public ProductService(ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IProductRepository productRepository)
        {
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.productRepository = productRepository;
        }
        public Category getCategoryByCategoryId(string categoryId)
        {
            Category category = this.categoryRepository.getCategoryByCategoryId(categoryId);
            return category;
        }

        public SubCategory getSubCategoryBySubCategoryId(string subCategoryId)
        {
            SubCategory subCategory = this.subCategoryRepository.getSubCategoryBySubCategoryId(subCategoryId);
            return subCategory;
        }

        public bool saveCategory(Category category)
        {
            bool res = this.categoryRepository.saveCategory(category);
            return res;
        }

        public bool saveSubCategory(SubCategory subCategory)
        {
            bool res = subCategoryRepository.saveSubCategory(subCategory);
            return res;
        }

        public bool updateCategory(Category category)
        {
            bool res = categoryRepository.updateCategory(category);
            return res;
        }

        public bool updateSubCategory(SubCategory subCategory)
        {
            bool res = subCategoryRepository.saveSubCategory(subCategory);
            return res;
        }

        public bool saveProduct(Product product)
        {
            bool res = productRepository.saveProduct(product);
            return res;
        }
    }
}