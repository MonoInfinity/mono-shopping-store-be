using System;
using Microsoft.AspNetCore.Mvc;
using store.Src.AuthModule;
using store.Src.ProductModule.DTO;
using store.Src.ProductModule.Entity;
using store.Src.ProductModule.Interface;
using store.Src.UserModule.Entity;
using store.Src.Utils.Common;
using store.Src.Utils.Validator;
using System.Collections.Generic;

namespace store.Src.ProductModule
{
    [ApiController]
    [Route("/api/product")]
    public class ProductController : Controller, IProductController
    {
        private readonly IProductService productService;
        private readonly AddCategoryDtoValidator addCategoryDtoValidator;
        private readonly AddSubCategoryDtoValidator addSubCategoryDtoValidator;
        private readonly AddProductDtoValidator addProductValidator;
        private readonly DeleteProductDtoValidator deleteProductDtoValidator;
        private readonly UpdateProductDtoValidator updateProductDtoValidator;

        public ProductController(
                                IProductService productService,
                                AddCategoryDtoValidator addCategoryDtoValidator,
                                AddSubCategoryDtoValidator addSubCategoryDtoValidator,
                                AddProductDtoValidator addProductValidator,
                                DeleteProductDtoValidator deleteProductDtoValidator,
                                UpdateProductDtoValidator updateProductDtoValidator
            )
        {
            this.productService = productService;
            this.addCategoryDtoValidator = addCategoryDtoValidator;
            this.addSubCategoryDtoValidator = addSubCategoryDtoValidator;
            this.addProductValidator = addProductValidator;
            this.deleteProductDtoValidator = deleteProductDtoValidator;
            this.updateProductDtoValidator = updateProductDtoValidator;
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

        [HttpPost("")]
        [ValidateFilterAttribute(typeof(AddProductDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult AddProduct(AddProductDto body)
        {
            Console.WriteLine("ahihi");
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

        [HttpPut("")]
        [ValidateFilterAttribute(typeof(UpdateProductDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult UpdateProduct(UpdateProductDto body)
        {
            ServerResponse<Product> res = new ServerResponse<Product>();

            SubCategory subCategory = this.productService.getSubCategoryBySubCategoryId(body.subCategoryId);
            if (subCategory == null)
            {
                res.setErrorMessage("The sub category with the given id was not found");
                return new BadRequestObjectResult(res.getResponse());
            }

            Product updateProduct = this.productService.getProductByProductId(body.productId);
            updateProduct.name = body.name;
            updateProduct.description = body.description;
            updateProduct.location = body.location;
            updateProduct.status = body.status;
            updateProduct.wholesalePrice = body.wholesalePrice;
            updateProduct.retailPrice = body.retailPrice;
            updateProduct.subCategory = subCategory;

            bool isInserted = this.productService.updateProduct(updateProduct);
            if (!isInserted)
            {
                res.setErrorMessage("Fail to update product");
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.data = updateProduct;
            return new ObjectResult(res.getResponse());
        }

        [HttpDelete("")]
        [ValidateFilterAttribute(typeof(DeleteProductDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult DeleteProduct(DeleteProductDto body)
        {
            ServerResponse<Product> res = new ServerResponse<Product>();
            Product product = this.productService.getProductByProductId(body.productId);
            if (product == null)
            {
                res.setErrorMessage("product with given productId not exist");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            bool isDelete = this.productService.deleteProduct(body.productId);
            if (!isDelete)
            {
                res.setErrorMessage("Fail to delete product");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }

            res.setMessage("Delete Product successfully");
            return new ObjectResult(res.getResponse());

        }

        [HttpGet("")]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult listAllProduct(int pageSize, int page, string name)
        {
            IDictionary<string, object> dataRes = new Dictionary<string, object>();
            ServerResponse<IDictionary<string, object>> res = new ServerResponse<IDictionary<string, object>>();
            var products = this.productService.getAllProduct(pageSize, page, name);
            var count = this.productService.getAllProductCount(name);
            dataRes.Add("products", products);
            dataRes.Add("count", count); res.data = dataRes;
            return new ObjectResult(res.getResponse());
        }
    }
}