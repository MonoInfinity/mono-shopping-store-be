using store.ProductModule.Entity;

namespace store.ProductModule.Interface
{
    public interface IProductRepository
    {
        public bool saveProduct(Product product);
    }
}