using System;
using Microsoft.AspNetCore.Mvc;
using store.Src.AuthModule;
using store.Src.Utils.Interface;
using store.Src.UserModule.Interface;
using store.Src.OrderModule.Interface;
using store.Src.OrderModule.DTO;
using store.Src.OrderModule.Entity;
using store.Src.ProductModule.Entity;
using store.Src.ProductModule.Interface;
using store.Src.UserModule.Entity;
using store.Src.Utils.Common;
using store.Src.Utils.Validator;
using System.Collections.Generic;

using static store.Src.Utils.Locale.CustomLanguageValidator;

namespace store.Src.OrderModule
{
    [ApiController]
    [Route("/api/order")]
    [ServiceFilter(typeof(AuthGuard))]

    public class OrderController : Controller, IOrderController
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        public OrderController(IOrderService orderService, IProductService productService)
        {
            this.orderService = orderService;
        }

        [HttpPost("/item")]
        [ValidateFilterAttribute(typeof(CreateItemDto))]
        [ServiceFilter(typeof(ValidateFilter))]

        public ObjectResult createItem(CreateItemDto body)
        {
            ServerResponse<Dictionary<string, string>> res = new ServerResponse<Dictionary<string, string>>();
            Dictionary<string, string> dataRes = new Dictionary<string, string>();

            Product product = this.productService.getProductByProductId(body.productId);

            if (product == null)
            {
                res.setErrorMessage("This product no longer exist");
                return new NotFoundObjectResult(res.getResponse());
            }

            Item createItem = new Item();
            createItem.itemId = Guid.NewGuid().ToString();
            createItem.quantity = body.quantity;
            createItem.salePrice = body.salePrice;
            createItem.createDate = DateTime.Now.ToShortTimeString();
            createItem.product.productId = body.productId;
            createItem.order.orderId = body.orderId;

            bool isCreated = this.orderService.saveItem(createItem);
            if (!isCreated)
            {
                res.setErrorMessage(ErrorMessageKey.Error_FailToSave);
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            dataRes.Add("itemId", createItem.itemId);
            res.data = dataRes;
        }

        [HttpPost("")]
        [ValidateFilterAttribute(typeof(CreateOrderDto))]
        [ServiceFilter(typeof(ValidateFilter))]
        public ObjectResult createOrder(CreateOrderDto body)
        {
            ServerResponse<Dictionary<string, string>> res = new ServerResponse<Dictionary<string, string>>();
            Dictionary<string, string> dataRes = new Dictionary<string, string>();

            Order createOrder = new Order();
            createOrder.orderId = Guid.NewGuid().ToString();
            createOrder.total = body.total;
            createOrder.createDate = DateTime.Now.ToShortDateString();
            createOrder.status = OrderStatus.NOT_PAID;
            createOrder.paymentMethod = OrderPaymentMethod.CASH;
            createOrder.customer.userId = body.costumerId;
            createOrder.casher.userId = body.casherId;

            bool isCreated = this.orderService.saveOrder(createOrder);
            if (!isCreated)
            {
                res.setErrorMessage(ErrorMessageKey.Error_FailToSave);
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }
            dataRes.Add("orderId", createOrder.orderId);
            res.data = dataRes;
        }


    }

}
