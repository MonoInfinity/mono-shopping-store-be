using Microsoft.AspNetCore.Mvc;
using store.Src.ProductModule.DTO;

namespace store.Src.ProductModule.Interface
{
    public interface IProductController
    {
        public ObjectResult addCategory(AddCategoryDto body);
        public ObjectResult addSubCategory(AddSubCategoryDto body);
        public ObjectResult updateCategory(UpdateCategoryDto body);
        public ObjectResult updateSubCategory(UpdateSubCategoryDto body);
        public ObjectResult listAllProduct(int pageSize, int page, string name);
        public ObjectResult updateProduct(UpdateProductDto body);

        // public ObjectResult addImportInfo(AddImportInfoDto body);
        // public ObjectResult addProduct(AddProductDto body);
        // public ObjectResult deleteProduct(DeleteProductDto body);
        // public ObjectResult getAProduct(string productId);
        // public ObjectResult updateImportInfo(UpdateImportInfoDto body);
        // public ObjectResult deleteImportInfo(DeleteImportInfoDto body);
    }
}