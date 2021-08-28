using store.Src.ProductModule.Entity;
using System.Collections.Generic;

namespace store.Src.ProductModule.Interface
{
    public interface IProductRepository
    {
        public bool saveProduct(Product product);
        public bool deleteProduct(string productId);
        public bool updateProduct(Product product);
        public List<Product> getAllProducts(int pageSize, int page, string name);
        public int getAllProductsCount(string name);
        public Product getProductByProductId(string productId);
        public Product getProductByName(string name);
    }
}