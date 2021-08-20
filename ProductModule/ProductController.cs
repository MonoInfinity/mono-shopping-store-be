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
    public class ProductController : IProductController
    {
        private readonly IProductService productService;
        private readonly AddCategoryDtoValidator addCategoryDtoValidator;
        public ProductController(IProductService productService, AddCategoryDtoValidator addCategoryDtoValidator){
            this.productService = productService;
            this.addCategoryDtoValidator = addCategoryDtoValidator;
        }

        [HttpPost("add")]
        // [ValidateFilterAttribute(typeof(AddCategoryDto))]
        // [RoleGuardAttribute(new UserRole[]{UserRole.MANAGER})]
        [ServiceFilter(typeof(AuthGuard))]
        // [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult AddCategory(AddCategoryDto body)
        {
            ServerResponse<Category> res = new ServerResponse<Category>();
            
            Category newCategory = new Category();
            newCategory.categoryId = Guid.NewGuid().ToString();
            newCategory.name = body.name;
            newCategory.createDate = DateTime.Now;
            newCategory.status = CategoryStatus.NOT_SALE;

            bool isInserted = this.productService.saveCategory(newCategory);
            if(!isInserted){
                res.setErrorMessage("Fail to save new category");
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            res.data = newCategory;
            return new ObjectResult(res.getResponse());
        }
    }
}