using Microsoft.AspNetCore.Mvc;
using store.Src.ProductModule.DTO;

namespace store.Src.ProductModule.Interface
{
    public interface IProductController
    {
        public ObjectResult addProduct(AddProductDto body);
        public ObjectResult updateProduct(UpdateProductDto body);
        public ObjectResult deleteProduct(DeleteProductDto body);
        public ObjectResult listAllProduct(int pageSize, int page, string name);
        public ObjectResult getAProduct(string productId);
    }
}