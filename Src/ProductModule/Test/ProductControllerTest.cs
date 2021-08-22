using System;
using store.Src.AuthModule;
using store.Src.AuthModule.Interface;
using store.Src.ProductModule.DTO;
using store.Src.ProductModule.Entity;
using store.Src.ProductModule.Interface;
using store.Src.UserModule;
using store.Src.UserModule.Entity;
using store.Src.UserModule.Interface;
using store.Src.Utils;
using store.Src.Utils.Interface;
using store.Src.Utils.Test;
using Xunit;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace store.Src.ProductModule.Test
{
    public class ProductControllerTest
    {
        private readonly IProductController productController;
        private readonly IProductService productService;
        private readonly IUploadFileService uploadFileService;
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ISubCategoryRepository subCategoryRepository;
        private readonly IUserRepository userRepository;
        private readonly IAuthService authService;
        private readonly User loginedUser;
        private readonly Category categoryDB;
        private readonly SubCategory subCategoryDB;
        private readonly Product productDB;
        private readonly FileStream stream;
        private readonly FormFile file;

        public ProductControllerTest()
        {
            ConfigTest config = new ConfigTest();
            IDBHelper dbHelper = new DBHelper(config);
            this.categoryRepository = new CategoryRepository(dbHelper);
            this.subCategoryRepository = new SubCategoryRepository(dbHelper, categoryRepository);
            this.userRepository = new UserRepository(dbHelper);
            this.productRepository = new ProductRepository(dbHelper, subCategoryRepository);

            AddCategoryDtoValidator addCategoryDtoValidator = new AddCategoryDtoValidator();
            AddSubCategoryDtoValidator addSubCategoryDtoValidator = new AddSubCategoryDtoValidator();
            AddProductDtoValidator addProductDtoValidator = new AddProductDtoValidator();
            DeleteProductDtoValidator deleteProductDtoValidator = new DeleteProductDtoValidator();
            UpdateProductDtoValidator updateProductDtoValidator = new UpdateProductDtoValidator();

            this.authService = new AuthService();
            this.uploadFileService = new UploadFileService();
            this.productService = new ProductService(categoryRepository, subCategoryRepository, productRepository);
            this.productController = new ProductController(productService, addCategoryDtoValidator,
                                                        addSubCategoryDtoValidator, addProductDtoValidator,
                                                        deleteProductDtoValidator, updateProductDtoValidator, uploadFileService);

            this.loginedUser = new User();
            this.loginedUser.userId = Guid.NewGuid().ToString();
            this.loginedUser.username = TestHelper.randomString(10, RamdomStringType.LETTER_LOWER_CASE);
            this.loginedUser.password = this.authService.hashingPassword("123456789");
            this.loginedUser.role = UserRole.MANAGER;
            this.userRepository.saveUser(loginedUser);

            this.categoryDB = new Category();
            this.categoryDB.categoryId = Guid.NewGuid().ToString();
            this.categoryDB.name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE);
            this.categoryDB.createDate = DateTime.Now.ToShortDateString();
            this.categoryDB.status = CategoryStatus.NOT_SALE;
            this.categoryRepository.saveCategory(categoryDB);

            this.subCategoryDB = new SubCategory();
            this.subCategoryDB.subCategoryId = Guid.NewGuid().ToString();
            this.subCategoryDB.name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE);
            this.subCategoryDB.createDate = DateTime.Now.ToShortDateString();
            this.subCategoryDB.status = SubCategoryStatus.NOT_SALE;
            this.subCategoryDB.category = this.categoryDB;
            this.subCategoryRepository.saveSubCategory(subCategoryDB);

            this.stream = File.OpenRead("./../../../Src/Utils/Test/vit01.jpg");
            this.file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

            this.productDB = new Product();
            this.productDB.productId = Guid.NewGuid().ToString();
            this.productDB.name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE);
            this.productDB.description = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE);
            this.productDB.location = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE);
            this.productDB.expiryDate = DateTime.Now.ToShortDateString();
            this.productDB.wholesalePrice = 1000;
            this.productDB.retailPrice = 1000;
            this.productDB.imageUrl = this.uploadFileService.upload(file);
            this.productDB.subCategory = subCategoryDB;
            this.productRepository.saveProduct(productDB);
        }

        [Fact]
        public void passAddCategory()
        {
            AddCategoryDto input = new AddCategoryDto()
            {
                name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE)
            };
            var res = this.productController.AddCategory(input);
            Category category = this.categoryRepository.getCategoryByName(input.name);

            Assert.NotNull(res);
            Assert.Equal(category.name, input.name);
        }

        [Fact]
        public void passAddSubCategory()
        {
            AddSubCategoryDto input = new AddSubCategoryDto()
            {
                name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
                categoryId = this.categoryDB.categoryId
            };

            var res = this.productController.AddSubCategory(input);
            SubCategory subCategory = this.subCategoryRepository.getSubCategoryByname(input.name);
            Assert.NotNull(res);
            Assert.Equal(subCategory.name, input.name);
        }

        [Fact]
        public void passAddProduct()
        {
            AddProductDto input = new AddProductDto()
            {
                name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
                description = TestHelper.randomString(100, RamdomStringType.LETTER),
                location = TestHelper.randomString(100, RamdomStringType.LETTER),
                expiryDate = DateTime.Now.ToShortDateString(),
                wholesalePrice = 1000,
                retailPrice = 1000,
                quantity = 100,
                file = this.file,
                subCategoryId = this.subCategoryDB.subCategoryId,
            };

            var res = this.productController.AddProduct(input);
            Product product = this.productRepository.getProductByname(input.name);

            Assert.NotNull(res);
            Assert.Equal(product.name, input.name);
        }

        [Fact]
        public void faildAddProductWrongFileExtension()
        {
            SubCategory subCategory = new SubCategory();
            subCategory.subCategoryId = Guid.NewGuid().ToString();
            FileStream stream = File.OpenRead("./../../../Src/Utils/Test/vit01.txt");
            FormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

            AddProductDto input = new AddProductDto()
            {
                name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
                description = TestHelper.randomString(100, RamdomStringType.LETTER),
                location = TestHelper.randomString(100, RamdomStringType.LETTER),
                expiryDate = DateTime.Now.ToShortDateString(),
                wholesalePrice = 1000,
                retailPrice = 1000,
                quantity = 100,
                file = file,
                subCategoryId = subCategory.subCategoryId,
            };

            var res = this.productController.AddProduct(input);

            Assert.Equal(400, res.StatusCode);
        }

        [Fact]
        public void passUpdateProduct()
        {
            UpdateProductDto input = new UpdateProductDto()
            {
                productId = productDB.productId,
                name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
                description = TestHelper.randomString(100, RamdomStringType.LETTER),
                location = TestHelper.randomString(100, RamdomStringType.LETTER),
                status = ProductStatus.SOLD_OUT,
                wholesalePrice = 1000,
                retailPrice = 1000,
                quantity = 100,
                file = this.file,
                subCategoryId = subCategoryDB.subCategoryId,
            };

            var res = this.productController.UpdateProduct(input);
            Product product = this.productService.getProductByName(input.name);
            Assert.NotNull(res);
            Assert.Equal(product.name, input.name);
        }

        [Fact]
        public void passGetAProduct()
        {
            var result = this.productController.GetAProduct(productDB.productId);
            var res = (Dictionary<string, object>)result.Value;
            var product = res["data"] as Product;
            Assert.NotNull(result);
            Assert.Equal(product.productId, productDB.productId);
        }
    }
}