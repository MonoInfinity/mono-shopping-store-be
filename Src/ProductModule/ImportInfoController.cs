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
{   [ApiController]
    [Route("/api/product")]
    [RoleGuardAttribute(new UserRole[] { UserRole.MANAGER })]
    [ServiceFilter(typeof(AuthGuard))]
    public class ImportInfoController: Controller, IImportInfoController
    {
        private readonly IProductService productService;
        private readonly IUserService userService;
        public ImportInfoController(IProductService productService, IUserService userService)
        {
            this.productService = productService;
            this.userService = userService;
        }

        [HttpPost("importInfo")]
        [ValidateFilterAttribute(typeof(AddImportInfoDto))]
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


            Product product = this.productService.getProductByProductId(body.productId);
            if (product == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "productId");
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
            importInfo.product = product;


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


        [HttpPut("importInfo")]
        [ValidateFilterAttribute(typeof(UpdateImportInfoDto))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult updateImportInfo([FromBody] UpdateImportInfoDto body)
        {
            ServerResponse<ImportInfo> res = new ServerResponse<ImportInfo>();
            var importInfo = this.productService.getImportInfoByImportInfoId(body.importInfoId);
            if (importInfo == null)
            {
                res.setErrorMessage(ErrorMessageKey.Error_NotFound, "importInfoId");
                return new BadRequestObjectResult(res.getResponse()) { StatusCode = 400 };
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