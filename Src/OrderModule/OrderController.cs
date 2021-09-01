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
        private readonly IUserService userService;
        public OrderController(IOrderService orderService, IProductService productService, IUserService userService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.userService = userService;
        }

        [HttpPost("")]
        [ValidateFilterAttribute(typeof(CreateItemDto))]
        [ServiceFilter(typeof(ValidateFilter))]

        public ObjectResult createOrder([FromBody] List<CreateItemDto> body)
        {
            ServerResponse<Dictionary<string, string>> res = new ServerResponse<Dictionary<string, string>>();
            Dictionary<string, string> dataRes = new Dictionary<string, string>();
            new ServerResponse<User>();
            var user = this.ViewData["user"] as User;
            double totalPrice = 0;
            for (int i = 0; i < body.Count; i++)
            {
                Product product = this.productService.getProductByProductId(body[i].productId);

                if (product == null)
                {
                    res.setErrorMessage(ErrorMessageKey.Error_NotFound, "productId");
                    return new NotFoundObjectResult(res.getResponse());
                }
                if (product.quantity < body[i].quantity)
                {
                    res.setErrorMessage(ErrorMessageKey.Error_NotEnoughtQuantity, "product quantity");
                    return new NotFoundObjectResult(res.getResponse());
                }
                totalPrice += product.price;
            }



            Order createOrder = new Order();
            createOrder.orderId = Guid.NewGuid().ToString();
            createOrder.total = totalPrice;
            createOrder.createDate = DateTime.Now.ToShortDateString();
            createOrder.status = OrderStatus.NOT_PAID;
            createOrder.paymentMethod = OrderPaymentMethod.CASH;
            createOrder.customer.userId = user.userId;
            createOrder.casher.userId = user.userId;

            bool isOrderCreated = this.orderService.saveOrder(createOrder);
            if (!isOrderCreated)
            {
                res.setErrorMessage(ErrorMessageKey.Error_FailToSave);
                return new ObjectResult(res.getResponse()) { StatusCode = 500 };
            }

            for (int i = 0; i < body.Count; i++)
            {
                Product product = this.productService.getProductByProductId(body[i].productId);
                Item createItem = new Item();
                createItem.itemId = Guid.NewGuid().ToString();
                createItem.quantity = body[i].quantity;
                createItem.salePrice = product.price;
                createItem.createDate = DateTime.Now.ToShortDateString();
                createItem.product.productId = product.productId;
                createItem.order.orderId = createOrder.orderId;

                bool isCreated = this.orderService.saveItem(createItem);
                if (!isCreated)
                {
                    res.setErrorMessage(ErrorMessageKey.Error_FailToSave);
                    return new ObjectResult(res.getResponse()) { StatusCode = 500 };
                }
                bool isDecrease = this.orderService.decreaseQuantity(product.productId, body[i].quantity);
                if (!isCreated)
                {
                    res.setErrorMessage(ErrorMessageKey.Error_UpdateFail);
                    return new ObjectResult(res.getResponse()) { StatusCode = 500 };
                }
            }

            dataRes.Add("orderId", createOrder.orderId);
            res.data = dataRes;
            return new ObjectResult(res.getResponse());
        }

    }

}
