using System;
using store.AuthModule;
using store.AuthModule.Interface;
using store.ProductModule.DTO;
using store.ProductModule.Entity;
using store.ProductModule.Interface;
using store.UserModule;
using store.UserModule.Entity;
using store.UserModule.Interface;
using store.Utils;
using store.Utils.Interface;
using store.Utils.Test;
using Xunit;

namespace store.ProductModule.Test
{
    public class ProductControllerTest
    {
        private readonly IProductController productController;
        private readonly IProductService productService;
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ISubCategoryRepository subCategoryRepository;
        private readonly IUserRepository userRepository;
        private readonly IAuthService authService;
        private readonly User loginedUser;

        public ProductControllerTest()
        {
            ConfigTest config = new ConfigTest();
            IDBHelper dbHelper = new DBHelper(config);
            this.categoryRepository = new CategoryRepository(dbHelper);
            this.subCategoryRepository = new SubCategoryRepository(dbHelper, categoryRepository);
            this.userRepository = new UserRepository(dbHelper);
            AddCategoryDtoValidator addCategoryDtoValidator = new AddCategoryDtoValidator();
            AddSubCategoryDtoValidator addSubCategoryDtoValidator = new AddSubCategoryDtoValidator();
            AddProductDtoValidator addProductDtoValidator = new AddProductDtoValidator();

            this.authService = new AuthService();
            this.productService = new ProductService(categoryRepository, subCategoryRepository, productRepository);
            this.productController = new ProductController(productService, addCategoryDtoValidator, addSubCategoryDtoValidator, addProductDtoValidator);

            this.loginedUser = new User();
            this.loginedUser.userId = Guid.NewGuid().ToString();
            this.loginedUser.username = TestHelper.randomString(10, RamdomStringType.LETTER_LOWER_CASE);
            this.loginedUser.password = this.authService.hashingPassword("123456789");
            this.loginedUser.role = UserRole.MANAGER;

            this.userRepository.saveUser(loginedUser);
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
            Category category = new Category();
            category.categoryId = Guid.NewGuid().ToString();
            category.name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE);
            category.createDate = DateTime.Now.ToShortDateString();
            category.status = CategoryStatus.NOT_SALE;
            this.categoryRepository.saveCategory(category);

            AddSubCategoryDto input = new AddSubCategoryDto()
            {
                name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
                categoryId = category.categoryId
            };

            var res = this.productController.AddSubCategory(input);
            SubCategory subCategory = this.subCategoryRepository.getSubCategoryByname(input.name);

            Assert.NotNull(res);
            Assert.Equal(subCategory.name, input.name);
        }

        [Fact]
        public void passAddProduct()
        {
            SubCategory subCategory = new SubCategory();
            subCategory.subCategoryId = Guid.NewGuid().ToString();

            AddProductDto input = new AddProductDto()
            {
                name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
                description = TestHelper.randomString(100, RamdomStringType.LETTER),
                location = TestHelper.randomString(100, RamdomStringType.LETTER),
                expiryDate = DateTime.Now.ToShortDateString(),
                wholesalePrice = 1000,
                retailPrice = 1000,
                quantity = 100,
                subCategoryId = subCategory.subCategoryId,
            };

            var res = this.productController.AddProduct(input);
            Product product = this.productService.getProductByName(input.name);

            Assert.NotNull(res);
            Assert.Equal(product.name, input.name);
        }

        [Fact]
        public void passUpdateProduct()
        {
            SubCategory subCategory = new SubCategory();
            subCategory.subCategoryId = Guid.NewGuid().ToString();
            UpdateProductDto input = new UpdateProductDto()
            {
                name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
                description = TestHelper.randomString(100, RamdomStringType.LETTER),
                location = TestHelper.randomString(100, RamdomStringType.LETTER),
                status = ProductStatus.SOLD_OUT,
                wholesalePrice = 1000,
                retailPrice = 1000,
                quantity = 100,
                subCategoryId = subCategory.subCategoryId,
            };

            var res = this.productController.UpdateProduct(input);
            Product product = this.productService.getProductByName(input.name);
            Assert.NotNull(res);
            Assert.Equal(product.name, input.name);
        }
    }
}