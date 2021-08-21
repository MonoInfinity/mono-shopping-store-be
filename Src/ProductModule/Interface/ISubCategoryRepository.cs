using store.Src.ProductModule.Entity;

namespace store.Src.ProductModule.Interface
{
    public interface ISubCategoryRepository
    {
        public bool saveSubCategory(SubCategory subCategory);

        public bool updateSubCategory(SubCategory subCategory);

        public SubCategory getSubCategoryByname(string name);
        public SubCategory getSubCategoryBySubCategoryId(string subCategoryId);
    }
}