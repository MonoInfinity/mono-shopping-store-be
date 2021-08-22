using Microsoft.AspNetCore.Mvc;
using store.Src.ProductModule.DTO;

namespace store.Src.ProductModule.Interface
{
    public interface IProductController
    {
        public ObjectResult AddCategory(AddCategoryDto body);
        public ObjectResult AddSubCategory(AddSubCategoryDto body);
        public ObjectResult AddProduct(AddProductDto body);
        public ObjectResult UpdateProduct(UpdateProductDto body);
        public ObjectResult DeleteProduct(DeleteProductDto body);
        public ObjectResult GetAProduct(string productId);
        public ObjectResult ListAllProduct(int pageSize, int page, string name);
    }
}