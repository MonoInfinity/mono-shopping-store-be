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

namespace store.ProductModule.Test
{
    public class ProductControllerTest
    {
        //    private readonly IProductController productController;
        //     private readonly IProductService productService;
        //     private readonly IProductRepository productRepository;
        //     private readonly ICategoryRepository categoryRepository;
        //     private readonly ISubCategoryRepository subCategoryRepository;
        //     private readonly IUserRepository userRepository;
        //     private readonly IAuthService authService;
        //     private readonly User loginedUser;

        //     public ProductControllerTest()
        //     {
        //         ConfigTest config = new ConfigTest();
        //         IDBHelper dbHelper = new DBHelper(config);
        //         this.categoryRepository = new CategoryRepository(dbHelper);
        //         this.subCategoryRepository = new SubCategoryRepository(dbHelper, categoryRepository);
        //         this.userRepository = new UserRepository(dbHelper);
        //         AddCategoryDtoValidator addCategoryDtoValidator = new AddCategoryDtoValidator();
        //         AddSubCategoryDtoValidator addSubCategoryDtoValidator = new AddSubCategoryDtoValidator();
        //         AddProductDtoValidator addProductDtoValidator = new AddProductDtoValidator();

        //         this.authService = new AuthService();
        //         this.productService = new ProductService(categoryRepository, subCategoryRepository, productRepository);
        //         this.productController = new Product Controller(productService, addCategoryDtoValidator, addSubCategoryDtoValidator, addProductDtoValidator);

        //         this.loginedUser = new User();
        //         this.loginedUser.userId = Guid.NewGuid().ToString();
        //         this.loginedUser.username = TestHelper.randomString(10, RamdomStringType.LETTER_LOWER_CASE);
        //         this.loginedUser.password = this.authService.hashingPassword("123456789");
        //         this.loginedUser.role = UserRole.MANAGER;

        //         this.userRepository.saveUser(loginedUser);
        //     }

        //     [Fact]
        //     public void passAddCategory()
        //     {
        //         AddCategoryDto input = new AddCategoryDto()
        //         {
        //             name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE)
        //         };
        //         var res = this.productController.AddCategory(input);
        //         Category category = this.categoryRepository.getCategoryByName(input.name);

        //         Assert.NotNull(res);
        //         Assert.Equal(category.name, input.name);
        //     }

        //     [Fact]
        //     public void passAddSubCategory()
        //     {
        //         Category category = new Category();
        //         category.categoryId = Guid.NewGuid().ToString();
        //         category.name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE);
        //         category.createDate = DateTime.Now.ToShortDateString();
        //         category.status = CategoryStatus.NOT_SALE;
        //         this.categoryRepository.saveCategory(category);

        //         AddSubCategoryDto input = new AddSubCategoryDto()
        //         {
        //             name = TestHelper.randomString(8, RamdomStringType.LETTER_LOWER_CASE),
        //             categoryId = category.categoryId
        //         };

        //         var res = this.productController.AddSubCategory(input);
        //         SubCategory subCategory = this.subCategoryRepository.getSubCategoryByname(input.name);

        //         Assert.NotNull(res);
        //         Assert.Equal(subCategory.name, input.name);
        //     }
    }
}