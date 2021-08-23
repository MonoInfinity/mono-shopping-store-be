using store.Src.ProductModule.Entity;
using System.Collections.Generic;

namespace store.Src.ProductModule.Interface
{
    public interface ISubCategoryRepository
    {
        public bool saveSubCategory(SubCategory subCategory);

        public bool updateSubCategory(SubCategory subCategory);

        public SubCategory getSubCategoryByname(string name);
        public SubCategory getSubCategoryBySubCategoryId(string subCategoryId);
        public List<SubCategory> getAllSubCategories(int pageSize, int page, string name);
        public int getAllSubCategoriesCount(string name);
        public List<SubCategory> getSubCategoriesByCategoryId(string categoryId);
    }
}