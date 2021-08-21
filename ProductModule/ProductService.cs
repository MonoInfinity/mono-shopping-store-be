using store.ProductModule.Entity;
using store.ProductModule.Interface;

namespace store.ProductModule
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
            bool res = subCategoryRepository.updateSubCategory(subCategory);
            return res;
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
            Product product = this.productRepository.getProductByname(name);
            return product;
        }
    }
}