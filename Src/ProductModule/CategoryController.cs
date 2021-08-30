using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using store.Src.AuthModule;
using store.Src.ProductModule.DTO;
using store.Src.ProductModule.Entity;
using store.Src.ProductModule.Interface;
using store.Src.UserModule.Entity;
using store.Src.UserModule.Interface;
using store.Src.Utils.Common;
using store.Src.Utils.Validator;
using static store.Src.Utils.Locale.CustomLanguageValidator;

namespace store.Src.ProductModule
{
    [ApiController]
    [Route("/api/product")]
    [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
    [ServiceFilter(typeof(AuthGuard))]
    public class CategoryController: Controller, ICategoryController
    {
        private readonly IProductService productService;
        public CategoryController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost("category")]
        [ValidateFilterAttribute(typeof(AddCategoryDto))]
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
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult addSubCategory([FromBody] AddSubCategoryDto body)
        {
            ServerResponse<Dictionary<string, string>> res = new ServerResponse<Dictionary<string, string>>();
            Dictionary<string, string> dataRes = new Dictionary<string, string>();

            Category category = this.productService.getCategoryByCategoryId(body.categoryId);
            if (category == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "categoryId");
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

        [HttpPut("category")]
        [ValidateFilterAttribute(typeof(UpdateCategoryDto))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult updateCategory([FromBody] UpdateCategoryDto body)
        {
            ServerResponse<Category> res = new ServerResponse<Category>();
            var category = this.productService.getCategoryByCategoryId(body.categoryId);
            if (category == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "categoryId");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 400 };
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
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult updateSubCategory([FromBody] UpdateSubCategoryDto body)
        {
            ServerResponse<SubCategory> res = new ServerResponse<SubCategory>();
            var subCategory = this.productService.getSubCategoryBySubCategoryId(body.subCategoryId);
            if (subCategory == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "subCategory");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 400 };
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
        public ObjectResult listAllSubCategory(int pageSize, int page, string name)
        {
            IDictionary<string, object> dataRes = new Dictionary<string, object>();
            ServerResponse<IDictionary<string, object>> res = new ServerResponse<IDictionary<string, object>>();
            var subCategories = this.productService.getAllSubCategory(pageSize, page, name);
            var count = this.productService.getAllSubCategoryCount(name);
            dataRes.Add("subCategories", subCategories);
            dataRes.Add("count", count);
            res.data = dataRes;
            return new ObjectResult(res.getResponse());
        }

        [HttpGet("subcategory/category/")]
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
    }
}