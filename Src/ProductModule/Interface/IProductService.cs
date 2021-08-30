using store.Src.ProductModule.Entity;
using System.Collections.Generic;

namespace store.Src.ProductModule.Interface
{
    public interface IProductService
    {
        public bool saveProduct(Product product);
        public bool updateProduct(Product product);
        public bool deleteProduct(string productId);
        public Product getProductByName(string name);
        public List<Product> getAllProduct(int currentPage, int pageSize, string name);
        public int getAllProductCount(string name);
        public Product getProductByProductId(string productId);

    }
}