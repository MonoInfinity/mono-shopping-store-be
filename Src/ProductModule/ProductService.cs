using store.Src.ProductModule.Entity;
using store.Src.ProductModule.Interface;
using System.Collections.Generic;

namespace store.Src.ProductModule
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;

        }

        public bool saveProduct(Product product)
        {
            bool res = productRepository.saveProduct(product);
            return res;
        }

        public bool updateProduct(Product product)
        {
            bool res = productRepository.updateProduct(product);
            return res;
        }

        public Product getProductByProductId(string productId)
        {
            Product product = this.productRepository.getProductByProductId(productId);
            return product;
        }

        public Product getProductByName(string name)
        {
            Product product = this.productRepository.getProductByName(name);
            return product;
        }
        public bool deleteProduct(string productId)
        {
            bool res = productRepository.deleteProduct(productId);
            return res;
        }

        public List<Product> getAllProduct(int currentPage, int pageSize, string name)
        {
            List<Product> products = this.productRepository.getAllProducts(pageSize, currentPage, name);
            return products;
        }
        public int getAllProductCount(string name)
        {
            var count = this.productRepository.getAllProductsCount(name);
            return count;
        }


    }
}