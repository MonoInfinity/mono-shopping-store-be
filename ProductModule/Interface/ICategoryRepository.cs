using store.ProductModule.Entity;

namespace store.ProductModule.Interface
{
    public interface ICategoryRepository
    {
        public bool saveCategory(Category category);

        public bool updateCategory(Category category);
    }
}