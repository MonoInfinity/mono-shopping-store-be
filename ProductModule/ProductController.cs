using System;
using Microsoft.AspNetCore.Mvc;
using store.AuthModule;
using store.ProductModule.DTO;
using store.ProductModule.Entity;
using store.ProductModule.Interface;
using store.UserModule.Entity;
using store.Utils.Common;
using store.Utils.Validator;

namespace store.ProductModule
{
    [ApiController]
    [Route("/api/product")]
    public class ProductController : Controller, IProductController
    {
        private readonly IProductService productService;
        private readonly AddCategoryDtoValidator addCategoryDtoValidator;
        private readonly AddSubCategoryDtoValidator addSubCategoryDtoValidator;
        private readonly AddProductDtoValidator addProductValidator;
        public ProductController(
                                IProductService productService,
                                AddCategoryDtoValidator addCategoryDtoValidator,
                                AddSubCategoryDtoValidator addSubCategoryDtoValidator,
                                AddProductDtoValidator addProductValidator
            )
        {
            this.productService = productService;
            this.addCategoryDtoValidator = addCategoryDtoValidator;
            this.addSubCategoryDtoValidator = addSubCategoryDtoValidator;
            this.addProductValidator = addProductValidator;
        }

        [HttpPost("category")]
        [ValidateFilterAttribute(typeof(AddCategoryDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult AddCategory(AddCategoryDto body)
        {
            ServerResponse<Category> res = new ServerResponse<Category>();

            Category newCategory = new Category();
            newCategory.categoryId = Guid.NewGuid().ToString();
            newCategory.name = body.name;
            newCategory.createDate = DateTime.Now.ToShortDateString();
            newCategory.status = CategoryStatus.NOT_SALE;

            bool isInserted = this.productService.saveCategory(newCategory);
            if (!isInserted)
            {
                res.setErrorMessage("Fail to save new category");
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.data = newCategory;
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("subcategory")]
        [ValidateFilterAttribute(typeof(AddSubCategoryDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult AddSubCategory(AddSubCategoryDto body)
        {
            ServerResponse<SubCategory> res = new ServerResponse<SubCategory>();

            Category category = this.productService.getCategoryByCategoryId(body.categoryId);
            if (category == null)
            {
                res.setErrorMessage("The category with the given id was not found");
                return new BadRequestObjectResult(res.getResponse());
            }

            SubCategory newSubCategory = new SubCategory();
            newSubCategory.subCategoryId = Guid.NewGuid().ToString();
            newSubCategory.name = body.name;
            newSubCategory.createDate = DateTime.Now.ToShortDateString();
            newSubCategory.status = SubCategoryStatus.NOT_SALE;
            newSubCategory.category = category;

            bool isInserted = this.productService.saveSubCategory(newSubCategory);
            if (!isInserted)
            {
                res.setErrorMessage("Fail to save new category");
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.data = newSubCategory;
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("product")]
        [ValidateFilterAttribute(typeof(AddProductDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult AddProduct(AddProductDto body)
        {
            ServerResponse<Product> res = new ServerResponse<Product>();

            SubCategory subCategory = this.productService.getSubCategoryBySubCategoryId(body.subCategoryId);
            if (subCategory == null)
            {
                res.setErrorMessage("The sub category with the given id was not found");
                return new BadRequestObjectResult(res.getResponse());
            }

            Product newProduct = new Product();
            newProduct.productId = Guid.NewGuid().ToString();
            newProduct.name = body.name;
            newProduct.description = body.description;
            newProduct.location = body.location;
            newProduct.expiryDate = body.expiryDate;
            newProduct.wholesalePrice = body.wholesalePrice;
            newProduct.retailPrice = body.retailPrice;
            newProduct.subCategory = subCategory;

            bool isInserted = this.productService.saveProduct(newProduct);
            if (!isInserted)
            {
                res.setErrorMessage("Fail to save new product");
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.data = newProduct;
            return new ObjectResult(res.getResponse());
        }
    }
}