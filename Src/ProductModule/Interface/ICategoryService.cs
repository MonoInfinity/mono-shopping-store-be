using System.Collections.Generic;
using store.Src.ProductModule.Entity;

namespace store.Src.ProductModule.Interface
{
    public interface ICategoryService
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
    }
}