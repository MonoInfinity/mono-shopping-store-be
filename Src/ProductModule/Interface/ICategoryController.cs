using Microsoft.AspNetCore.Mvc;
using store.Src.ProductModule.DTO;

namespace store.Src.ProductModule.Interface
{
    public interface ICategoryController
    {
        public ObjectResult addCategory(AddCategoryDto body);
        public ObjectResult addSubCategory(AddSubCategoryDto body);
        public ObjectResult updateCategory(UpdateCategoryDto body);
        public ObjectResult updateSubCategory(UpdateSubCategoryDto body);
        public ObjectResult listAllCategory();
        public ObjectResult listAllSubCategory(int pageSize, int page, string name);
        public ObjectResult listSubCategoryByCategoryId(string categoryId);
    }
}