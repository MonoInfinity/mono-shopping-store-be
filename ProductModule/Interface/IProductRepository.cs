using store.ProductModule.Entity;
using System.Collections.Generic;

namespace store.ProductModule.Interface
{
    public interface IProductRepository
    {
        public bool saveProduct(Product product);
        public bool deleteProduct(string productId);
        public List<Product> getAllProducts(int pageSize, int page, string name);
        public int getAllProductsCount(string name);
        public Product getProductByProductId(string productId);
    }
}