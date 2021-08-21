using store.Src.ProductModule.Entity;

namespace store.Src.ProductModule.Interface
{
    public interface IProductRepository
    {
        public bool saveProduct(Product product);
    }
}