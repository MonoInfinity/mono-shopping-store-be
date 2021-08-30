using Microsoft.AspNetCore.Mvc;
using store.Src.OrderModule.DTO;
using store.Src.OrderModule.Entity;

namespace store.Src.OrderModule.Interface
{
    public interface IOrderController
    {
        public ObjectResult saveOrder(Order order);
        public ObjectResult saveItem(Item item);
        public ObjectResult getQuantityByProductId(Item item);
    }
}