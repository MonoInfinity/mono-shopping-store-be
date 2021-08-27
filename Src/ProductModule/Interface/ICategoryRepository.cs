using store.Src.ProductModule.Entity;
using System.Collections.Generic;

namespace store.Src.ProductModule.Interface
{
    public interface ICategoryRepository
    {
        public bool saveCategory(Category category);

        public bool updateCategory(Category category);

        public Category getCategoryByCategoryId(string categoryId);

        public Category getCategoryByName(string name);
        public List<Category> getAllCategories();
    }
}