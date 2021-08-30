using Microsoft.AspNetCore.Mvc;
using store.Src.OrderModule.DTO;
using store.Src.OrderModule.Entity;

namespace store.Src.OrderModule.Interface
{
    public interface IOrderController
    {
        public ObjectResult addOrder(Order order);
        public ObjectResult addItem(Item item);
        public ObjectResult getQuantityByProductId(Item item);
        public ObjectResult getLastOrderId(string customerId);
    }
}