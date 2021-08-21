using store.ProductModule.Entity;

namespace store.ProductModule.Interface
{
    public interface IProductRepository
    {
        public bool saveProduct(Product product);
        public bool updateProduct(Product product);
        public Product getProductByname(string name);
        public Product getProductByProductId(string productId);
    }
}