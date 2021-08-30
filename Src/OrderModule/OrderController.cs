using System;
using Microsoft.AspNetCore.Mvc;
using store.Src.AuthModule;
using store.Src.Utils.Interface;
using store.Src.UserModule.Interface;
using store.Src.OrderModule.Interface;

namespace store.Src.OrderModule
{
    [ApiController]
    [Route("/api/order")]
    [ServiceFilter(typeof(AuthGuard))]

    public class OrderController : Controller, IOrderController
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
    }

}
