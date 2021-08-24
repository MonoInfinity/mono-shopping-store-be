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
using FluentValidation.Results;
using store.Src.Utils;
using store.Src.Utils.Interface;
using store.Src.UserModule.Interface;

namespace store.Src.ProductModule
{
    [ApiController]
    [Route("/api/product")]
    public class ProductController : Controller, IProductController
    {
        private readonly IProductService productService;
        private readonly IUserService userService;
        private readonly AddCategoryDtoValidator addCategoryDtoValidator;
        private readonly AddSubCategoryDtoValidator addSubCategoryDtoValidator;
        private readonly AddProductDtoValidator addProductValidator;
        private readonly DeleteProductDtoValidator deleteProductDtoValidator;
        private readonly UpdateProductDtoValidator updateProductDtoValidator;
        private readonly IUploadFileService uploadFileService;
        private readonly UpdateCategoryDtoValidator updateCategoryDtoValidator;
        private readonly UpdateSubCategoryDtoValidator updateSubCategoryDtoValidator;

        public ProductController(
                                IProductService productService,
                                IUserService userService,
                                AddCategoryDtoValidator addCategoryDtoValidator,
                                AddSubCategoryDtoValidator addSubCategoryDtoValidator,
                                AddProductDtoValidator addProductValidator,
                                DeleteProductDtoValidator deleteProductDtoValidator,
                                UpdateProductDtoValidator updateProductDtoValidator,
                                IUploadFileService uploadFileService,
                                UpdateCategoryDtoValidator updateCategoryDtoValidator,
                                UpdateSubCategoryDtoValidator updateSubCategoryDtoValidator
            )
        {
            this.productService = productService;
            this.userService = userService;
            this.addCategoryDtoValidator = addCategoryDtoValidator;
            this.addSubCategoryDtoValidator = addSubCategoryDtoValidator;
            this.addProductValidator = addProductValidator;
            this.deleteProductDtoValidator = deleteProductDtoValidator;
            this.updateProductDtoValidator = updateProductDtoValidator;
            this.uploadFileService = uploadFileService;
            this.updateCategoryDtoValidator = updateCategoryDtoValidator;
            this.updateSubCategoryDtoValidator = updateSubCategoryDtoValidator;
        }

        [HttpPost("category")]
        [ValidateFilterAttribute(typeof(AddCategoryDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult addCategory([FromBody] AddCategoryDto body)
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
        public ObjectResult addSubCategory([FromBody] AddSubCategoryDto body)
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
        [ServiceFilter(typeof(ValidateFilter))]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult addProduct([FromBody] AddProductDto body)
        {
            ServerResponse<Product> res = new ServerResponse<Product>();

            SubCategory subCategory = this.productService.getSubCategoryBySubCategoryId(body.subCategoryId);
            if (subCategory == null)
            {
                res.setErrorMessage("The sub category with the given id was not found");
                return new BadRequestObjectResult(res.getResponse());
            }

            ImportInfo importInfo = this.productService.getImportInfoByImportInfoId(body.importInfoId);
            if (importInfo == null)
            {
                res.setErrorMessage("The import infomation with the given id was not found");
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
            newProduct.imageUrl = body.imageUrl;
            newProduct.subCategory = subCategory;
            newProduct.importInfo = importInfo;

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
        [ServiceFilter(typeof(ValidateFilter))]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult updateProduct([FromBody] UpdateProductDto body)
        {
            ServerResponse<Product> res = new ServerResponse<Product>();

            Product updateProduct = this.productService.getProductByProductId(body.productId);
            if (updateProduct == null)
            {
                res.setErrorMessage("The product with the given id was not found");
                return new BadRequestObjectResult(res.getResponse());
            }

            updateProduct.name = body.name;
            updateProduct.description = body.description;
            updateProduct.location = body.location;
            updateProduct.status = body.status;
            updateProduct.wholesalePrice = body.wholesalePrice;
            updateProduct.retailPrice = body.retailPrice;
            updateProduct.quantity = body.quantity;
            if (body.imageUrl != null)
            {
                updateProduct.imageUrl = body.imageUrl;
            }
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
        public ObjectResult deleteProduct([FromBody] DeleteProductDto body)
        {
            ServerResponse<Product> res = new ServerResponse<Product>();
            Product product = this.productService.getProductByProductId(body.productId);
            if (product == null)
            {
                res.setErrorMessage("product with given productId not exist");
                return new BadRequestObjectResult(res.getResponse());
            }
            bool isDelete = this.productService.deleteProduct(body.productId);
            if (!isDelete)
            {
                res.setErrorMessage("Fail to delete product");
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }

            res.setMessage("Delete Product successfully");
            return new ObjectResult(res.getResponse());

        }

        [HttpGet("all/{pageSize}/{page}/{name}")]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult listAllProduct([FromRoute] int pageSize, [FromRoute] int page, [FromRoute] string name)
        {
            IDictionary<string, object> dataRes = new Dictionary<string, object>();
            ServerResponse<IDictionary<string, object>> res = new ServerResponse<IDictionary<string, object>>();
            var products = this.productService.getAllProduct(pageSize, page, name);
            var count = this.productService.getAllProductCount(name);
            dataRes.Add("products", products);
            dataRes.Add("count", count);
            res.data = dataRes;
            return new ObjectResult(res.getResponse());
        }

        [HttpGet("{productId}")]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult getAProduct([FromRoute] string productId)
        {
            ServerResponse<Product> res = new ServerResponse<Product>();

            Product product = this.productService.getProductByProductId(productId);
            if (product == null)
            {
                res.setErrorMessage("product with given productId not exist");
                return new BadRequestObjectResult(res.getResponse());
            }

            res.data = product;
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("category")]
        [ValidateFilterAttribute(typeof(UpdateCategoryDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult updateCategory([FromBody] UpdateCategoryDto body)
        {
            ServerResponse<Category> res = new ServerResponse<Category>();
            var category = this.productService.getCategoryByCategoryId(body.categoryId);
            if (category == null)
            {
                res.setErrorMessage("Category with given categoryId not exist");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            category.name = body.name;
            category.status = body.status;
            bool isUpdate = this.productService.updateCategory(category);
            if (!isUpdate)
            {
                res.setErrorMessage("Fail to update category");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.setMessage("Update category success!");
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("subCategory")]
        [ValidateFilterAttribute(typeof(UpdateSubCategoryDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult updateSubCategory([FromBody] UpdateSubCategoryDto body)
        {
            ServerResponse<SubCategory> res = new ServerResponse<SubCategory>();
            var subCategory = this.productService.getSubCategoryBySubCategoryId(body.subCategoryId);
            if (subCategory == null)
            {
                res.setErrorMessage("SubCategory with given subCategoryId not exist");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            subCategory.name = body.name;
            subCategory.status = body.status;
            bool isUpdate = this.productService.updateSubCategory(subCategory);
            if (!isUpdate)
            {
                Console.WriteLine(subCategory);
                res.setErrorMessage("Fail to update subCategory");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.setMessage("Update subcategory success!");
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("importInfo")]
        [ValidateFilterAttribute(typeof(AddImportInfoDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult addImportInfo([FromBody] AddImportInfoDto body)
        {
            ServerResponse<Product> res = new ServerResponse<Product>();

            User manager = this.userService.getUserById(body.managerId);
            if (manager == null)
            {
                res.setErrorMessage("The manager with the give id was not found");
                return new BadRequestObjectResult(res.getResponse());
            }

            ImportInfo importInfo = new ImportInfo();
            importInfo.importInfoId = Guid.NewGuid().ToString();
            importInfo.importDate = body.importDate;
            importInfo.importPrice = body.importPrice;
            importInfo.importQuantity = body.importQuantity;
            importInfo.expiryDate = body.expiryDate;
            importInfo.note = body.note;
            importInfo.brand = body.brand;
            importInfo.manager = manager;

            bool isInserted = this.productService.saveImportInfo(importInfo);
            if (!isInserted)
            {
                res.setErrorMessage("Fail to save import infomation");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }

            res.setMessage("Add import information success");
            return new ObjectResult(res.getResponse());
        }
        [HttpGet("category/all")]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult listAllCategory()
        {
            ServerResponse<List<Category>> res = new ServerResponse<List<Category>>();
            var categories = this.productService.getAllCategory();
            if (categories == null)
            {
                res.setErrorMessage("Empty Category");
                return new NotFoundObjectResult(res.getResponse());
            }
            res.data = categories;
            return new ObjectResult(res.getResponse());
        }

        [HttpGet("subcategory/all/{pageSize}/{page}/{name}")]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult listAllSubCategory([FromRoute] int pageSize, [FromRoute] int page, [FromRoute] string name)
        {
            IDictionary<string, object> dataRes = new Dictionary<string, object>();
            ServerResponse<IDictionary<string, object>> res = new ServerResponse<IDictionary<string, object>>();
            var subCategories = this.productService.getAllSubCategory(pageSize, page, name);
            var count = this.productService.getAllSubCategoryCount(name);
            dataRes.Add("users", subCategories);
            dataRes.Add("count", count);
            res.data = dataRes;
            return new ObjectResult(res.getResponse());
        }

        [HttpGet("subcategory/categoryId/{categoryId}")]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult listSubCategoryByCategoryId([FromRoute] string categoryId)
        {
            ServerResponse<List<SubCategory>> res = new ServerResponse<List<SubCategory>>();
            var isValidCategoryId = productService.getCategoryByCategoryId(categoryId);
            if (isValidCategoryId == null)
            {
                res.setErrorMessage("CategoryId not found");
                return new NotFoundObjectResult(res.getResponse());
            }

            var subCategories = this.productService.getSubCategoryByCategoryId(categoryId);
            if (subCategories == null)
            {
                res.setErrorMessage("Empty SubCategory");
                return new NotFoundObjectResult(res.getResponse());
            }
            res.data = subCategories;
            return new ObjectResult(res.getResponse());
        }
    }
}