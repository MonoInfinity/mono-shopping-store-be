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
        private readonly IImportInfoRepository importInfoRepository;
        public ProductService(ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository, IProductRepository productRepository, IImportInfoRepository importInfoRepository)
        {
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.productRepository = productRepository;
            this.importInfoRepository = importInfoRepository;
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

        // public bool updateProduct(Product product)
        // {
        //     bool res = productRepository.updateProduct(product);
        //     return res;
        // }

        // public Product getProductByProductId(string productId)
        // {
        //     Product product = this.productRepository.getProductByProductId(productId);
        //     return product;
        // }

        // public Product getProductByName(string name)
        // {
        //     Product product = this.productRepository.getProductByName(name);
        //     return product;
        // }
        // public bool deleteProduct(string productId)
        // {
        //     bool res = productRepository.deleteProduct(productId);
        //     return res;
        // }

        // public List<Product> getAllProduct(int currentPage, int pageSize, string name)
        // {
        //     List<Product> products = this.productRepository.getAllProducts(pageSize, currentPage, name);
        //     return products;
        // }
        // public int getAllProductCount(string name)
        // {
        //     var count = this.productRepository.getAllProductsCount(name);
        //     return count;
        // }

        public bool saveImportInfo(ImportInfo importInfo)
        {
            bool res = this.importInfoRepository.saveImportInfo(importInfo);
            return res;
        }

        public ImportInfo getImportInfoByImportInfoId(string importInfoId)
        {
            ImportInfo importInfo = this.importInfoRepository.getImportInfoByImportInfoId(importInfoId);
            return importInfo;
        }

        public List<Category> getAllCategory()
        {
            List<Category> categories = this.categoryRepository.getAllCategories();
            return categories;
        }

        public List<SubCategory> getAllSubCategory(int currentPage, int pageSize, string name)
        {
            List<SubCategory> subCategories = this.subCategoryRepository.getAllSubCategories(pageSize, currentPage, name);
            return subCategories;
        }
        public int getAllSubCategoryCount(string name)
        {
            var count = this.subCategoryRepository.getAllSubCategoriesCount(name);
            return count;
        }

        public List<SubCategory> getSubCategoryByCategoryId(string categoryId)
        {
            List<SubCategory> subCategories = this.subCategoryRepository.getSubCategoriesByCategoryId(categoryId);
            return subCategories;
        }

        public Category getCategoryByCategoryName(string name)
        {
            Category category = this.categoryRepository.getCategoryByName(name);
            return category;
        }

        public SubCategory getSubCategoryBySubCategoryName(string name)
        {
            SubCategory subCategory = this.subCategoryRepository.getSubCategoryByname(name);
            return subCategory;
        }
        public bool updateImportInfo(ImportInfo importInfo)
        {
            bool res = this.importInfoRepository.updateImportInfo(importInfo);
            return res;
        }

        public bool deleteImportInfo(string importInfoId)
        {
            bool res = this.importInfoRepository.deleteImportInfo(importInfoId);
            return res;
        }
    }
}