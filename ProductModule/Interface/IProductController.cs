using Microsoft.AspNetCore.Mvc;
using store.ProductModule.DTO;

namespace store.ProductModule.Interface
{
    public interface IProductController
    {
        public ObjectResult AddCategory(AddCategoryDto body);

        public ObjectResult AddSubCategory(AddSubCategoryDto body);
        public ObjectResult DeleteProduct(DeleteProductDto body);
    }
}