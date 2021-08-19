using store.ProductModule.Entity;

namespace store.ProductModule.Interface
{
    public interface ISubCategoryRepository
    {
        public bool saveSubCategory(SubCategory subCategory);

        public bool updateSubCategory(SubCategory subCategory);

        public SubCategory getSubCategoryBySubCategoryId(string subCategoryId);
    }
}