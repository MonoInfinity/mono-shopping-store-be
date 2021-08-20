using store.ProductModule.Entity;
using store.ProductModule.Interface;

namespace store.ProductModule
{
    public class ProductService : IProductService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ISubCategoryRepository subCategoryRepository;
        public ProductService(ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository){
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
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
    }
}