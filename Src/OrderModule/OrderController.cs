using System;
using Microsoft.AspNetCore.Mvc;
using store.Src.AuthModule;
using store.Src.OrderModule.DTO;
using store.Src.OrderModule.Entity;
using store.Src.OrderModule.Interface;
using store.Src.UserModule.Entity;
using store.Src.Utils.Common;
using store.Src.Utils.Validator;
using System.Collections.Generic;
using store.Src.Utils.Interface;
using store.Src.UserModule.Interface;
using static store.Src.Utils.Locale.CustomLanguageValidator;

namespace store.Src.OrderModule
{
    public class OrderController
    {
        [ApiController]
        [Route("/api/order")]
        [ServiceFilter(typeof(AuthGuard))]

        public class ProductController : Controller, IProductController
        {
            private readonly IProductService productService;
            private readonly IUserService userService;
            private readonly IUploadFileService uploadFileService;
            public ProductController(IProductService productService, IUserService userService, IUploadFileService uploadFileService)
            {
                this.productService = productService;
                this.userService = userService;
                this.uploadFileService = uploadFileService;
            }
        }

    }
}