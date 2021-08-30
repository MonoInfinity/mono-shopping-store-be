using System.Collections.Generic;
using store.Src.ProductModule.Entity;
using store.Src.ProductModule.Interface;

namespace store.Src.ProductModule
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository categoryRepository;
        private readonly ISubCategoryRepository subCategoryRepository;
        public CategoryService(ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public List<Category> getAllCategory()
        {
            List<Category> categories = this.categoryRepository.getAllCategories();
            return categories;
        }

        public List<SubCategory> getAllSubCategory(int currentPage, int pageSize, string name)
        {
            List<SubCategory> subCategories = this.subCategoryRepository.getAllSubCategories(pageSize, currentPage, name);
            return subCategories;
        }
        public int getAllSubCategoryCount(string name)
        {
            var count = this.subCategoryRepository.getAllSubCategoriesCount(name);
            return count;
        }

        public List<SubCategory> getSubCategoryByCategoryId(string categoryId)
        {
            List<SubCategory> subCategories = this.subCategoryRepository.getSubCategoriesByCategoryId(categoryId);
            return subCategories;
        }

        public Category getCategoryByCategoryName(string name)
        {
            Category category = this.categoryRepository.getCategoryByName(name);
            return category;
        }

        public SubCategory getSubCategoryBySubCategoryName(string name)
        {
            SubCategory subCategory = this.subCategoryRepository.getSubCategoryByname(name);
            return subCategory;
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
            bool res = subCategoryRepository.updateSubCategory(subCategory);
            return res;
        }
    }
}