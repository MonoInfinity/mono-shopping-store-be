using store.Src.ProductModule.Entity;
using store.Src.ProductModule.Interface;
using System.Collections.Generic;

namespace store.Src.ProductModule
{
    public class ProductService : IProductService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ISubCategoryRepository subCategoryRepository;
        private readonly IProductRepository productRepository;
        public ProductService(ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IProductRepository productRepository)
        {
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.productRepository = productRepository;
        }
        public Category getCategoryByCategoryId(string categoryId)
        {
            Category category = this.categoryRepository.getCategoryByCategoryId(categoryId);
            return category;
        }

        public SubCategory getSubCategoryBySubCategoryId(string subCategoryId)
        {
            SubCategory subCategory = this.subCategoryRepository.getSubCategoryBySubCategoryId(subCategoryId);
            return subCategory;
        }

        public bool saveCategory(Category category)
        {
            bool res = this.categoryRepository.saveCategory(category);
            return res;
        }

        public bool saveSubCategory(SubCategory subCategory)
        {
            bool res = subCategoryRepository.saveSubCategory(subCategory);
            return res;
        }

        public bool updateCategory(Category category)
        {
            bool res = categoryRepository.updateCategory(category);
            return res;
        }

        public bool updateSubCategory(SubCategory subCategory)
        {
            bool res = subCategoryRepository.saveSubCategory(subCategory);
            return res;
        }

        public bool saveProduct(Product product)
        {
            bool res = productRepository.saveProduct(product);
            return res;
        }
        public bool deleteProduct(string productId)
        {
            bool res = productRepository.deleteProduct(productId);
            return res;
        }

        public List<Product> getAllProduct(int currentPage, int pageSize, string name)
        {
            var products = this.productRepository.getAllProducts(pageSize, currentPage, name);
            return products;
        }
        public int getAllProductCount(string name)
        {
            var count = this.productRepository.getAllProductsCount(name);
            return count;
        }

        public Product getProductByProductId(string productId)
        {
            var product = this.productRepository.getProductByProductId(productId);
            return product;
        }
    }
}