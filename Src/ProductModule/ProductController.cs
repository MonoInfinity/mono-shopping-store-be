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
using store.Src.Utils.Interface;
using store.Src.UserModule.Interface;
using static store.Src.Utils.Locale.CustomLanguageValidator;

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
        private readonly UpdateImportInfoDtoValidator updateImportInfoDtoValidator;
        private readonly DeleteImportInfoDtoValidator deleteImportInfoDtoValidator;
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
                                UpdateSubCategoryDtoValidator updateSubCategoryDtoValidator,
                                UpdateImportInfoDtoValidator updateImportInfoDtoValidator,
                                DeleteImportInfoDtoValidator deleteImportInfoDtoValidator
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
            this.updateImportInfoDtoValidator = updateImportInfoDtoValidator;
            this.deleteImportInfoDtoValidator = deleteImportInfoDtoValidator;
        }

        [HttpPost("category")]
        [ValidateFilterAttribute(typeof(AddCategoryDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult addCategory([FromBody] AddCategoryDto body)
        {
            ServerResponse<Dictionary<string, string>> res = new ServerResponse<Dictionary<string, string>>();
            Dictionary<string, string> dataRes = new Dictionary<string, string>();
            Category category = this.productService.getCategoryByCategoryName(body.name);
            if (category != null)
            {
                dataRes.Add("categoryId", category.categoryId);
                res.data = dataRes;
                res.setErrorMessage(ErrorMessageKey.Error_Existed, "categoryName");
                return new BadRequestObjectResult(res.getResponse());
            }

            Category newCategory = new Category();
            newCategory.categoryId = Guid.NewGuid().ToString();
            newCategory.name = body.name;
            newCategory.createDate = DateTime.Now.ToShortDateString();
            newCategory.status = CategoryStatus.NOT_SALE;

            bool isInserted = this.productService.saveCategory(newCategory);
            if (!isInserted)
            {
                res.setErrorMessage(ErrorMessageKey.Error_FailToSave);
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }

            dataRes.Add("categoryId", newCategory.categoryId);
            res.data = dataRes;
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("subcategory")]
        [ValidateFilterAttribute(typeof(AddSubCategoryDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult addSubCategory([FromBody] AddSubCategoryDto body)
        {
            ServerResponse<Dictionary<string, string>> res = new ServerResponse<Dictionary<string, string>>();
            Dictionary<string, string> dataRes = new Dictionary<string, string>();

            Category category = this.productService.getCategoryByCategoryId(body.categoryId);
            if (category == null)
            {
                res.setErrorMessage("The category with the given id was not found");
                return new NotFoundObjectResult(res.getResponse());
            }

            SubCategory subCategory = this.productService.getSubCategoryBySubCategoryName(body.name);
            if (subCategory != null)
            {
                dataRes.Add("subCategoryId", subCategory.subCategoryId);
                res.setErrorMessage(ErrorMessageKey.Error_Existed, "subCategoryName");
                res.data = dataRes;
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
                res.setErrorMessage(ErrorMessageKey.Error_FailToSave);
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            dataRes.Add("subCategoryId", newSubCategory.subCategoryId);
            res.data = dataRes;
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("importInfo")]
        [ValidateFilterAttribute(typeof(AddImportInfoDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult addImportInfo([FromBody] AddImportInfoDto body)
        {
            ServerResponse<Dictionary<string, string>> res = new ServerResponse<Dictionary<string, string>>();

            User manager = this.userService.getUserById(body.managerId);
            if (manager == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "managerId");
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
                res.setErrorMessage(ErrorMessageKey.Error_FailToSave);
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }

            Dictionary<string, string> dataRes = new Dictionary<string, string>();
            dataRes.Add("importInfoId", importInfo.importInfoId);
            res.data = dataRes;
            res.setMessage(MessageKey.Message_AddSuccess);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("")]
        [ValidateFilterAttribute(typeof(AddProductDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(ValidateFilter))]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult addProduct([FromBody] AddProductDto body)
        {
            ServerResponse<Dictionary<string, string>> res = new ServerResponse<Dictionary<string, string>>();

            SubCategory subCategory = this.productService.getSubCategoryBySubCategoryId(body.subCategoryId);
            if (subCategory == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "subCategoryId");
                return new BadRequestObjectResult(res.getResponse());
            }

            ImportInfo importInfo = this.productService.getImportInfoByImportInfoId(body.importInfoId);
            if (importInfo == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "importInfoId");
                return new BadRequestObjectResult(res.getResponse());
            }

            Product newProduct = new Product();
            newProduct.productId = Guid.NewGuid().ToString();
            newProduct.name = body.name;
            newProduct.description = body.description;
            newProduct.location = body.location;
            newProduct.wholesalePrice = body.wholesalePrice;
            newProduct.retailPrice = body.retailPrice;
            newProduct.imageUrl = body.imageUrl;
            newProduct.subCategory = subCategory;
            newProduct.importInfo = importInfo;

            bool isInserted = this.productService.saveProduct(newProduct);
            if (!isInserted)
            {
                res.setErrorMessage(ErrorMessageKey.Error_FailToSave);
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            Dictionary<string, string> dataRes = new Dictionary<string, string>();
            dataRes.Add("productId", newProduct.productId);
            res.data = dataRes;
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
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "productId");
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
                res.setErrorMessage(ErrorMessageKey.Error_FailToSave);
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.data = updateProduct;
            res.setMessage(MessageKey.Message_UpdateSuccess);
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
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "productId");
                return new BadRequestObjectResult(res.getResponse());
            }
            bool isDelete = this.productService.deleteProduct(body.productId);
            if (!isDelete)
            {
                res.setErrorMessage(ErrorMessageKey.Error_DeleteFail);
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }

            res.setMessage(MessageKey.Message_DeleteSuccess);
            return new ObjectResult(res.getResponse());

        }

        [HttpGet("all")]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult listAllProduct(int pageSize, int page, string name)
        {
            IDictionary<string, object> dataRes = new Dictionary<string, object>();
            ServerResponse<IDictionary<string, object>> res = new ServerResponse<IDictionary<string, object>>();
            var count = this.productService.getAllProductCount(name);
            var products = this.productService.getAllProduct(pageSize, page, name);
            dataRes.Add("products", products);
            dataRes.Add("count", count);
            res.data = dataRes;
            return new ObjectResult(res.getResponse());
        }

        [HttpGet("")]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult getAProduct(string productId)
        {
            ServerResponse<Product> res = new ServerResponse<Product>();

            Product product = this.productService.getProductByProductId(productId);
            if (product == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "productId");
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
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "categoryId");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            category.name = body.name;
            category.status = body.status;
            bool isUpdate = this.productService.updateCategory(category);
            if (!isUpdate)
            {
                res.setErrorMessage(ErrorMessageKey.Error_UpdateFail);
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.setMessage(MessageKey.Message_UpdateSuccess);
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
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "subCategory");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            subCategory.name = body.name;
            subCategory.status = body.status;
            bool isUpdate = this.productService.updateSubCategory(subCategory);
            if (!isUpdate)
            {
                Console.WriteLine(subCategory);
                res.setErrorMessage(ErrorMessageKey.Error_UpdateFail);
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.setMessage(MessageKey.Message_UpdateSuccess);
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
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "categories");
                return new NotFoundObjectResult(res.getResponse());
            }
            res.data = categories;
            return new ObjectResult(res.getResponse());
        }

        [HttpGet("subcategory/all")]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult listAllSubCategory(int pageSize, int page, string name)
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

        [HttpGet("subcategory/category/")]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        public ObjectResult listSubCategoryByCategoryId(string categoryId)
        {
            ServerResponse<List<SubCategory>> res = new ServerResponse<List<SubCategory>>();
            var isValidCategoryId = productService.getCategoryByCategoryId(categoryId);
            if (isValidCategoryId == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "categoryId");
                return new NotFoundObjectResult(res.getResponse());
            }

            var subCategories = this.productService.getSubCategoryByCategoryId(categoryId);
            if (subCategories == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "subCategories");
                return new NotFoundObjectResult(res.getResponse());
            }
            res.data = subCategories;
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("importInfo")]
        [ValidateFilterAttribute(typeof(UpdateImportInfoDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult updateImportInfo([FromBody] UpdateImportInfoDto body)
        {
            ServerResponse<ImportInfo> res = new ServerResponse<ImportInfo>();
            var importInfo = this.productService.getImportInfoByImportInfoId(body.importInfoId);
            if (importInfo == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "importInfoId");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            importInfo.importDate = body.importDate;
            importInfo.importPrice = body.importPrice;
            importInfo.importQuantity = body.importQuantity;
            importInfo.expiryDate = body.expiryDate;
            importInfo.brand = body.brand;
            importInfo.note = body.note;
            bool isUpdate = this.productService.updateImportInfo(importInfo);
            if (!isUpdate)
            {
                res.setErrorMessage(ErrorMessageKey.Error_UpdateFail);
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.setMessage(MessageKey.Message_UpdateSuccess);
            return new ObjectResult(res.getResponse());
        }

        [HttpDelete("importInfo")]
        [ValidateFilterAttribute(typeof(DeleteImportInfoDto))]
        [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
        [ServiceFilter(typeof(AuthGuard))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult deleteImportInfo([FromBody] DeleteImportInfoDto body)
        {
            ServerResponse<ImportInfo> res = new ServerResponse<ImportInfo>();
            var importInfo = this.productService.getImportInfoByImportInfoId(body.importInfoId);
            if (importInfo == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "importInfoId");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            bool isDelete = this.productService.deleteImportInfo(importInfo.importInfoId);
            if (!isDelete)
            {
                res.setErrorMessage(ErrorMessageKey.Error_DeleteFail);
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.setMessage(MessageKey.Message_DeleteSuccess);
            return new ObjectResult(res.getResponse());
        }
    }
}