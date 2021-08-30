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
using store.Src.UserModule.Interface;
using static store.Src.Utils.Locale.CustomLanguageValidator;

namespace store.Src.ProductModule
{
    [ApiController]
    [Route("/api/product")]
    [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
    [ServiceFilter(typeof(AuthGuard))]
    public class ProductController : Controller, IProductController
    {
        private readonly IProductService productService;
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;
        public ProductController(IProductService productService, IUserService userService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.userService = userService;
            this.categoryService = categoryService;
        }

        [HttpPost("")]
        [ValidateFilterAttribute(typeof(AddProductDto))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult addProduct([FromBody] AddProductDto body)
        {
            ServerResponse<Dictionary<string, string>> res = new ServerResponse<Dictionary<string, string>>();

            SubCategory subCategory = this.categoryService.getSubCategoryBySubCategoryId(body.subCategoryId);
            if (subCategory == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "subCategoryId");
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
        [ServiceFilter(typeof(ValidateFilter))]
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
    }
}