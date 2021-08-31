using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using store.Src.OrderModule.DTO;
using store.Src.OrderModule.Entity;

namespace store.Src.OrderModule.Interface
{
    public interface IOrderController
    {
        public ObjectResult createOrder(CreateOrderDto body);
        public ObjectResult createItem(CreateItemDto body);
    }
}